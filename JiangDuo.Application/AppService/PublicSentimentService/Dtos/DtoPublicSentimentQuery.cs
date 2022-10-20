using JiangDuo.Core.Base;
using JiangDuo.Core.Enums;
using System;

namespace JiangDuo.Application.AppService.PublicSentimentService.Dto
{
    public class DtoPublicSentimentQuery : BaseRequest
    {
        /// <summary>
        /// 业务类型
        /// </summary>
        public long? BusinessId { get; set; }

        /// <summary>
        /// 居民Id
        /// </summary>
        public long? ResidentId { get; set; }

        /// <summary>
        /// 工单类型
        /// </summary>
        public WorkorderTypeEnum? WorkorderType { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public PublicSentimentStatus? Status { get; set; }

        /// <summary>
        /// 所属选区Id
        /// </summary>
        public long? SelectAreaId { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string PhoneNumber { get; set; }
    }
}