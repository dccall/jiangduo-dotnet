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
        public WorkorderTypeEnum? Type { get; set; }
        /// <summary>
        /// 选区id
        /// </summary>
        public long? SelectAreaId { get; set; }
        /// <summary>
        /// 工单状态 1.未处理 2.进行中 3.已反馈 4.已完结
        /// </summary>
        public WorkorderStatusEnum? Status { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTimeOffset? StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTimeOffset? EndTime { get; set; }
    }
}
