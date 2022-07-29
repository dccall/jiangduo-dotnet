using JiangDuo.Core.Base;
using JiangDuo.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppletAppService.OfficialApplet.Dtos
{
    public class DtoMyWorkOrderQuery:BaseRequest
    {
        /// <summary>
        /// 工单状态
        /// </summary>
        public WorkorderStatusEnum? Status { get; set; }

    }
}
