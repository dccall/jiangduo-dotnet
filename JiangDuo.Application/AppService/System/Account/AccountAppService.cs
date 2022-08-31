using Furion.DynamicApiController;
using Furion.FriendlyException;
using Furion.Logging;
using JiangDuo.Application.Account.Dtos;
using JiangDuo.Application.Account.Services;
using JiangDuo.Application.Menu.Services;
using JiangDuo.Application.User.Dtos;
using JiangDuo.Core.Filters;
using JiangDuo.Core.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        var jwtTokenResult = JwtHelper.GetJwtToken(accountModel);
        return jwtTokenResult;
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