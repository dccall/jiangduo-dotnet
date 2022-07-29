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
    public class DtoWorkOrderEndHandel
    {
        /// <summary>
        /// 工单id
        /// </summary>
        public long WordOrderId { get;  set; }
        /// <summary>
        /// 反馈内容
        /// </summary>
        public string FeedbackContent { get; set; }
    }
}
