using JiangDuo.Core.Base;
using JiangDuo.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.ServiceService.Dto
{
    public class DtoUpdateServiceStatus
    {
        /// <summary>
        /// 服务id
        /// </summary>
        public long ServiceId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public ServiceStatusEnum? Status { get; set; }
    }
}
