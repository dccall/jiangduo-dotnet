using System.Threading.Tasks;
using JiangDuo.Application.Account.Services;
using JiangDuo.Application.Tools;
using JiangDuo.Core.Attributes;
using Furion;
using Furion.Authorization;
using Furion.DatabaseAccessor;
using Furion.DataEncryption;
using Furion.FriendlyException;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace JiangDuo.Web.Core.Handlers;

public class JwtHandler : AppAuthorizeHandler
{
	
	/// <summary>
	/// 重写 Handler 添加自动刷新收取逻辑
	/// </summary>
	/// <param name="context"></param>
	/// <returns></returns>
	public override async Task HandleAsync(AuthorizationHandlerContext context)
	{
		// 自动刷新 token
		if (JWTEncryption.AutoRefreshToken(context, context.GetCurrentHttpContext()))
		{
			await AuthorizeHandleAsync(context);
		}
		else context.Fail(); // 授权失败
	}
	/// <summary>
	/// 请求管道
	/// </summary>
	/// <param name="context"></param>
	/// <param name="httpContext"></param>
	/// <returns></returns>
	public override Task<bool> PipelineAsync(AuthorizationHandlerContext context, DefaultHttpContext httpContext)
	{
		// 此处已经自动验证 Jwt token的有效性了，无需手动验证

		// 检查权限，如果方法是异步的就不用 Task.FromResult 包裹，直接使用 async/await 即可
		return Task.FromResult(CheckAuthorzie(httpContext));
	}
	/// <summary>
	/// 检查权限
	/// </summary>
	/// <param name="httpContext"></param>
	/// <returns></returns>
	private static bool CheckAuthorzie(DefaultHttpContext httpContext)
	{
		// 获取权限特性
		var securityDefineAttribute = httpContext.GetMetadata<SecurityDefineAttribute>();
		if (securityDefineAttribute == null)
		{
			return true;
		}
		return true;
	}
}