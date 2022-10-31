using JiangDuo.Core.Enums;
using JiangDuo.Core.Models;
using System;

namespace JiangDuo.Application.AppService.ServiceService.Dtos
{
    public class DtoJoinServiceResident
    {
        /// <summary>
        /// 居民
        /// </summary>
        public Resident Resident { get; set; }

        public DateTime? RegistTime { get; set; }

        /// <summary>
        /// 预约开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 预约结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 自己预约
        /// </summary>
        public bool Self { get; set; } = false;
        /// <summary>
        /// 状态
        /// </summary>
        public ParticipantStatus Status { get; set; }
    }
}