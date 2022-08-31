namespace JiangDuo.Application.AppService.WorkOrderService.Dto
{
    public class DtoWorkOrderAssign
    {
        /// <summary>
        /// 工单id
        /// </summary>
        public long WorkOrderId { get; set; }

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