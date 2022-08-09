using Furion.DynamicApiController;
using JiangDuo.Application.AppletAppService.AppletLogin.Dtos;
using JiangDuo.Application.AppletAppService.AppletLogin.Services;
using JiangDuo.Core.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppletAppService.AppletLogin
{
    /// <summary>
    ///小程序端统一登录
    /// </summary>
    [Route("api/[controller]")]
    [ApiDescriptionSettings("Default", "小程序端统一登录")]
    public class AppletLoginAppService : IDynamicApiController
    {

        private readonly IAppletLoginService _appletLoginService;
        public AppletLoginAppService(IAppletLoginService appletLoginService)
        {
            _appletLoginService = appletLoginService;
        }
        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("GetVerifyCode")]
        public async Task<bool> GetVerifyCode([FromQuery] string phone)
        {
            return await _appletLoginService.GetVerifyCode(phone);
        }
        /// <summary>
        /// 登录(手机号登录)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<DtoAppletLoginResult> LoginByPhone(DtoAppletLogin model)
        {
            return await _appletLoginService.LoginByPhone(model);
        }

        /// <summary>
        /// 获取登录信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("GetLoginInfo")]
        public async Task<AccountModel> GetLoginInfo()
        {
            return JwtHelper.GetAccountInfo();
        }
    }
}
