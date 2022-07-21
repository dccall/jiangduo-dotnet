using JiangDuo.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.ServiceService.Dto
{
    public class DtoServiceQuery : BaseRequest
    {
        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName { get; set; }
    }
}
