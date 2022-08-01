using JiangDuo.Core.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JiangDuo.Core.Models
{
    /// <summary>
    /// 参与人
    /// </summary>
    [Table("Participant")]
    public partial class Participant: BaseEntity
    {
        /// <summary>
        /// 服务Id
        /// </summary>
        public long? ServiceId { get; set; }
        /// <summary>
        /// 居民id
        /// </summary>
        public long? ResidentId { get; set; }

        /// <summary>
        /// 报名时间
        /// </summary>
        public DateTime? RegistTime { get; set; }

        /// <summary>
        /// 预约开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 预约结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
    }
}
