using JiangDuo.Core.Enums;
using System;

namespace JiangDuo.Application.AppService.ServiceService.Dto
{
    public class DtoServiceSubscribeQuery
    {
        /// <summary>
        /// 登记日期
        /// </summary>
        public DateTime? RegistTime { get; set; }

        /// <summary>
        /// 服务id
        /// </summary>
        public long? ServiceId { get; set; }

        /// <summary>
        /// 居民id
        /// </summary>
        public long? ResidentId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public ParticipantStatus? Status { get; set; }
    }
}