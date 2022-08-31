using Furion.DependencyInjection;
using JiangDuo.Core.Utils;
using System;
using System.Threading.Tasks;

namespace JiangDuo.Core.Services
{
    public class VerifyCodeService : IVerifyCodeService, ITransient
    {
        private readonly IRedisCache _redisCache;

        /// <summary>
        /// 随机数
        /// </summary>
        private static readonly Random _rnd = new Random();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="redisCache">redis缓存</param>
        public VerifyCodeService(IRedisCache redisCache)
        {
            _redisCache = redisCache;
        }

        /// <summary>
        /// 根据手机号生成验证码
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <returns></returns>
        public async Task<string> GenerateVerificationCodeAsync(string phone)
        {
            //生成4位数随机数
            var result = _rnd.Next(1000, 9999);
            var strResult = $"{result:0000}";
            var cacheKey = $"{phone}-{strResult}";
            await _redisCache.SetAsync(cacheKey, true, new TimeSpan(0, 5, 0));
            return $"{result:0000}";
        }

        /// <summary>
        /// 验证验证码是否正确
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <param name="code">代码</param>
        /// <returns></returns>
        public async Task<bool> CheckVerificationCodeAsync(string phone, string code)
        {
            var cacheKey = $"{phone}-{code}";
            return await _redisCache.GetAsync(cacheKey, false);
        }
    }
}