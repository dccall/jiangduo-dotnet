using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using JiangDuo.Application.Tools;
using JiangDuo.Application.Account.Services;
using JiangDuo.Core.Enums;
using Furion;
using Furion.DatabaseAccessor;
using Furion.DataEncryption;
using Furion.DynamicApiController;
using Furion.FriendlyException;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yitter.IdGenerator;
using JiangDuo.Application.Menu.Services;
using JiangDuo.Application.Account.Dtos;
using JiangDuo.Application.User.Dtos;
using JiangDuo.Core.Attributes;
using JiangDuo.Core.Utils;
using JiangDuo.Core.Filters;
using Furion.Logging;

namespace JiangDuo.Application.Account;

/// <summary>
/// 账号服务
/// </summary>
[OperLogActionFilter("账号服务")]

public class AccountAppService : IDynamicApiController
{
	private readonly IAccountService _accountService;
	private readonly IMenuService _menuService;
	public AccountAppService(IAccountService accountService, IMenuService menuService)
	{
		_accountService = accountService;
		_menuService = menuService;
	}
	/// <summary>
	/// 用户登录
	/// </summary>
	/// <param name="model"></param>
	/// <returns></returns>
	[AllowAnonymous]
    public async Task<JwtTokenResult> Login([FromBody] DtoLoginRequest model)
	{
	    var user = await _accountService.GetUserByUserNameAndPwd(model);
		if (user == null)
		{
			Log.Error($"用户不存在或密码错误");
			throw Oops.Oh($"用户不存在或密码错误");
		}
		user.PassWord = null;
		
		AccountModel accountModel = new AccountModel();
		accountModel.Id = user.Id;
		accountModel.Name = user.NickName;
		accountModel.Type = AccountType.System;
		var jwtTokenResult= JwtHelper.GetJwtToken(accountModel);
		return  jwtTokenResult;
	}
	/// <summary>
	/// 获取登陆人用户信息
	/// </summary>
	/// <returns></returns>
	[ApiDescriptionSettings(KeepVerb = true)]
	public async Task<DtoUser> GetUserInfo()
	{
		var userId = JwtHelper.GetAccountId();
		return await _accountService.GetById(userId);
	}
	/// <summary>
	/// 获取登录用户路由
	/// </summary>
	/// <returns></returns>
	[ApiDescriptionSettings(KeepVerb = true)]
	public async Task<DtoUserRoutes> GetUserRoutes()
	{
		var userId = JwtHelper.GetAccountId();
		return await _accountService.GetUserRoutes(userId);
	}
	/// <summary>
	/// 根据用户id获取路由
	/// </summary>
	/// <returns></returns>
	[ApiDescriptionSettings(KeepVerb = true)]
	public async Task<DtoUserRoutes> GetUserRoutes(long userId)
	{
		return await _accountService.GetUserRoutes(userId);
	}

	/// <summary>
	/// 修改密码
	/// </summary>
	/// <param name="model"></param>
	/// <returns></returns>
	[ApiDescriptionSettings(KeepVerb = true)]
	public async Task<int> UpdatePassword(DtoUpdatePassword model)
	{
		return await _accountService.UpdatePassword(model);
	}
}