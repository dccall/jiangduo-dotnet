using JiangDuo.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.System.Config.Dto
{
    public class ConfigRequest: BaseRequest
    {
        /// <summary>
        /// 参数名称
        /// </summary>
        public string ConfigName { get; set; }
    }
}
