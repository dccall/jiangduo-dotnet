using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppletAppService.OfficialApplet.Dtos
{
    public class DtoOfficialLogin
    {
        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string Code { get; set; }


        /// <summary>
        /// 小程序登录code
        /// </summary>
        public string JsCode { get; set; }
    }
}
