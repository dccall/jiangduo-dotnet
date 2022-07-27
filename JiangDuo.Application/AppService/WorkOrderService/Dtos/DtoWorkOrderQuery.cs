using JiangDuo.Core.Base;
using JiangDuo.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.WorkOrderService.Dto
{
    public class DtoWorkOrderQuery : BaseRequest
    {
        /// <summary>
        /// 工单类型 1.有事好商量 2.一老一小 3.码上说马上办
        /// </summary>
        public WorkorderTypeEnum? WorkorderType { get; set; }
        /// <summary>
        /// 选区id
        /// </summary>
        public long? SelectAreaId { get; set; }
        /// <summary>
        /// 工单状态 1.未处理 2.进行中 3.已反馈 4.已完结 5.已同意 6已拒绝
        /// </summary>
        public WorkorderStatusEnum? Status { get; set; }
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
