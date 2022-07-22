using Furion.DependencyInjection;
using Microsoft.Extensions.Caching.Distributed;
using MsgPack.Serialization;
using System;
using System.IO;
using System.Threading.Tasks;

namespace JiangDuo.Core.Utils
{
	/// <summary>
	/// Redis缓存帮助接口
	/// </summary>
	public interface IRedisCache : ITransient
	{
		/// <summary>
		/// 设置缓存
		/// </summary>
		/// <typeparam name="T">类型</typeparam>
		/// <param name="key">键</param>
		/// <param name="t">值</param>
		/// <param name="expireTime">过期时间(绝对)</param>
		Task SetAsync<T>(string key, T t, DateTime? expireTime = null);
		/// <summary>
		/// 设置缓存
		/// </summary>
		/// <typeparam name="T">类型</typeparam>
		/// <param name="key">键</param>
		/// <param name="t">值</param>
		/// <param name="expireAt">过期时间(相对)</param>
		Task SetAsync<T>(string key, T t, TimeSpan? expireAt = null);
		/// <summary>
		/// 设置缓存
		/// </summary>
		/// <typeparam name="T">类型</typeparam>
		/// <param name="key">键</param>
		/// <param name="bytes">数据</param>
		/// <param name="options">选项</param>
		/// <returns></returns>
		Task SetAsync<T>(string key, byte[] bytes, DistributedCacheEntryOptions options);
		/// <summary>
		/// 获取缓存
		/// </summary>
		/// <typeparam name="T">类型</typeparam>
		/// <param name="key">键</param>
		/// <param name="defaultValue">默认数据</param>
		/// <returns></returns>
		Task<T> GetAsync<T>(string key, T defaultValue = default(T));
	}

	/// <summary>
	/// Redis缓存帮助类
	/// </summary>
	public class RedisCache : IRedisCache
	{
		private readonly IDistributedCache _cache;
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="cache">缓存</param>
		public RedisCache(IDistributedCache cache)
		{
			_cache = cache;
		}
		/// <summary>
		/// 设置缓存
		/// </summary>
		/// <typeparam name="T">类型</typeparam>
		/// <param name="key">键</param>
		/// <param name="t">值</param>
		/// <param name="expireTime">过期时间(绝对)</param>
		public async Task SetAsync<T>(string key, T t, DateTime? expireTime = null)
		{
			await using var stream = new MemoryStream();
			// Creates serializer.
			var serializer = MessagePackSerializer.Get<T>();
			// Pack obj to stream.
			await serializer.PackAsync(stream, t);
			stream.Seek(0, SeekOrigin.Begin);
			var bytes = stream.ToArray();
			var options = new DistributedCacheEntryOptions();
			if (expireTime.HasValue)
			{
				options.AbsoluteExpiration = new DateTimeOffset(expireTime.Value.ToLocalTime());
			}
			await SetAsync<T>(key, bytes, options);
		}
		/// <summary>
		/// 设置缓存
		/// </summary>
		/// <typeparam name="T">类型</typeparam>
		/// <param name="key">键</param>
		/// <param name="t">值</param>
		/// <param name="expireAt">过期时间(相对)</param>
		public async Task SetAsync<T>(string key, T t, TimeSpan? expireAt = null)
		{
			await using var stream = new MemoryStream();
			// Creates serializer.
			var serializer = MessagePackSerializer.Get<T>();
			// Pack obj to stream.
			await serializer.PackAsync(stream, t);
			stream.Seek(0, SeekOrigin.Begin);
			var bytes = stream.ToArray();
			var options = new DistributedCacheEntryOptions();
			if (expireAt.HasValue)
			{
				options.SlidingExpiration = expireAt;
			}
			await SetAsync<T>(key, bytes, options);
		}
		/// <summary>
		/// 设置缓存
		/// </summary>
		/// <typeparam name="T">类型</typeparam>
		/// <param name="key">键</param>
		/// <param name="bytes">数据</param>
		/// <param name="options">选项</param>
		/// <returns></returns>
		public async Task SetAsync<T>(string key, byte[] bytes, DistributedCacheEntryOptions options)
		{
			await _cache.SetAsync(key, bytes, options);
		}
		/// <summary>
		/// 获取缓存
		/// </summary>
		/// <typeparam name="T">类型</typeparam>
		/// <param name="key">键</param>
		/// <param name="defaultValue">默认类型</param>
		/// <returns></returns>
		public async Task<T> GetAsync<T>(string key, T defaultValue = default(T))
		{
			// Creates serializer.
			try
			{

				var serializer = MessagePackSerializer.Get<T>();
				var bytes = await _cache.GetAsync(key);
				using var stream = new MemoryStream(bytes);
				return await serializer.UnpackAsync(stream);
			}
			catch (Exception)
			{
				return default;
			}
		}
	}
}
