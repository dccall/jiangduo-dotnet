using JiangDuo.Core.Base;

namespace JiangDuo.Application.AppService.OnlineletterService.Dto
{
    public class DtoOnlineletterQuery : BaseRequest
    {
        /// <summary>
        /// 业务类型
        /// </summary>
        public long? BusinessId { get; set; }

        /// <summary>
        /// 所属选区Id
        /// </summary>
        public long? SelectAreaId { get; set; }

        /// <summary>
        /// 关联工单Id
        /// </summary>
        public long? WorkOrderId { get; set; }
    }
}