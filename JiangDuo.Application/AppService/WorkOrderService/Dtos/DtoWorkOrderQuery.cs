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
    public class DtoWorkOrderQuery : BaseRequest
    {
        /// <summary>
        /// 接收人名称
        /// </summary>
        public string RecipientName { get; set; }
        /// <summary>
        /// 工单编号
        /// </summary>
        [MaxLength(50)]
        public string WorkOrderNo { get; set; }
        /// <summary>
        /// 工单类型 1.有事好商量 2.一老一小 3.码上说马上办
        /// </summary>
        public WorkorderTypeEnum? WorkorderType { get; set; }
        /// <summary>
        /// 选区id
        /// </summary>
        public long? SelectAreaId { get; set; }
        /// <summary>
        /// 工单状态 1.未处理 2.进行中 3.已完成待审核 4.审核通过 5.已完结
        /// </summary>
        public WorkorderStatusEnum? Status { get; set; }
        /// <summary>
        /// 业务类型
        /// </summary>
        public long? BusinessId { get; set; }

        /// <summary>
        /// 页面来源0.待处理，1.待审核
        /// </summary>
        public int PageSource { get; set; } = 0;
        /// <summary>
        /// 工单来源 0系统1居民 2.人大
        /// </summary>
        public WorkorderSourceEnum? WorkorderSource { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
    }
}
