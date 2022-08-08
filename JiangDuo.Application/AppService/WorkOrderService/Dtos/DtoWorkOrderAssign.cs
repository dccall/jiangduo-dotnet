using JiangDuo.Application.AppService.OnlineletterService.Dto;
using JiangDuo.Application.AppService.ReserveService.Dto;
using JiangDuo.Application.AppService.ServiceService.Dto;
using JiangDuo.Core.Base;
using JiangDuo.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.WorkOrderService.Dto
{
    public class DtoWorkOrderAssign
    {
        /// <summary>
        /// 工单id
        /// </summary>
        public long WorkOrderId { get;  set; }
        /// <summary>
        /// 指派接收人
        /// </summary>
        public long? RecipientId { get; set; }
        /// <summary>
        /// 指派协助人
        /// </summary>
        public long? AssistantId { get; set; }

    }
}
