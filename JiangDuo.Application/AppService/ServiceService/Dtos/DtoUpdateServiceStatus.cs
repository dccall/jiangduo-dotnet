using JiangDuo.Core.Enums;

namespace JiangDuo.Application.AppService.ServiceService.Dto
{
    public class DtoUpdateServiceStatus
    {
        /// <summary>
        /// 服务id
        /// </summary>
        public long ServiceId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public ServiceStatusEnum? Status { get; set; }
    }
}