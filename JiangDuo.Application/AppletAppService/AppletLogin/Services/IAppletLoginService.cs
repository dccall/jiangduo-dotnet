using JiangDuo.Application.AppletAppService.AppletLogin.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppletAppService.AppletLogin.Services
{
    public interface IAppletLoginService
    {
        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public  Task<bool> GetVerifyCode(string phone);
        /// <summary>
        /// 小程序登录(手机号登录)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public  Task<DtoAppletLoginResult> LoginByPhone(DtoAppletLogin model);
    }
}
