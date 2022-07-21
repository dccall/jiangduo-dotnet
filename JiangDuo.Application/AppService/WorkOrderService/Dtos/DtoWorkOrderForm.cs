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
    public class DtoWorkOrderForm 
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long? Id { get;  set; }

        /// <summary>
        /// 发起人
        /// </summary>
        public long OriginatorId { get; set; }
        /// <summary>
        /// 接收人
        /// </summary>
        public long ReceiverId { get; set; }
        /// <summary>
        /// 工单类型
        /// </summary>
        public WorkorderTypeEnum Type { get; set; }
        /// <summary>
        /// 引用的业务id
        /// </summary>
        public long? RelationId { get; set; }
        /// <summary>
        /// 选区id
        /// </summary>
        public long? SelectAreaId { get; set; }
        /// <summary>
        /// 工单状态
        /// </summary>
        public WorkorderStatusEnum Status { get; set; }
        /// <summary>
        /// 工单内容
        /// </summary>
        [MaxLength(300)]
        public string Content { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTimeOffset StartTime { get; set; }
        /// <summary>
        /// 完结时间
        /// </summary>
        public DateTimeOffset? OverTime { get; set; }

         /// <summary>
         /// 有事好商量
         /// </summary>
        public DtoReserveForm Reserve { get; set; }
        /// <summary>
        /// 一老一小(服务)
        /// </summary>
        public DtoServiceForm Service { get; set; }
        /// <summary>
        /// 码上说马上办
        /// </summary>
        public DtoOnlineletterForm OnlineLetters { get; set; }

    }
}
