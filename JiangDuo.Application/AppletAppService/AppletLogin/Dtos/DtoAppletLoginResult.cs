using JiangDuo.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppletAppService.AppletLogin.Dtos
{
    public class DtoAppletLoginResult
    {
        /// <summary>
        /// 账号token
        /// </summary>
        public string AccessToken { get; set; }
        /// <summary>
        /// 刷新token
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public AccountType Type { get; set; }

    }
}
