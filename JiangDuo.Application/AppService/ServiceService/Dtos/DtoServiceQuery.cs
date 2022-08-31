using JiangDuo.Core.Base;
using JiangDuo.Core.Enums;

namespace JiangDuo.Application.AppService.ServiceService.Dto
{
    public class DtoServiceQuery : BaseRequest
    {
        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public ServiceStatusEnum? Status { get; set; }

        /// <summary>
        /// 服务类型
        /// </summary>
        public ServiceTypeEnum? ServiceType { get; set; }

        /// <summary>
        /// 服务分类
        /// </summary>
        public long? ServiceClassifyId { get; set; }

        /// <summary>
        /// 所属区域
        /// </summary>
        public long? SelectAreaId { get; set; }

        /// <summary>
        /// 创建人id
        /// </summary>
        public long? Creator { get; set; }

        /// <summary>
        /// 页面来源0.服务申请，1服务发布
        /// </summary>
        public int PageSource { get; set; } = 0;
    }
}