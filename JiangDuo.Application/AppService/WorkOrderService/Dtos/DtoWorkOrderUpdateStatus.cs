using JiangDuo.Core.Enums;

namespace JiangDuo.Application.AppService.WorkOrderService.Dto
{
    public class DtoWorkOrderUpdateStatus
    {
        /// <summary>
        /// 工单id
        /// </summary>
        public long WordOrderId { get; set; }

        /// <summary>
        /// 处理内容
        /// </summary>

        public string HandelContent { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public WorkorderStatusEnum Status { get; set; }
    }
}