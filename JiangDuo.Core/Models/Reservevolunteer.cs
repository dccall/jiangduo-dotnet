using JiangDuo.Core.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JiangDuo.Core.Models
{
    /// <summary>
    /// 有事好商量志愿者
    /// </summary>
    [Table("Reservevolunteer")]
    public partial class Reservevolunteer : BaseEntity
    {
        /// <summary>
        /// 有事好商量预约Id
        /// </summary>
        public long ReserveId { get; set; }
        /// <summary>
        /// 志愿者Id
        /// </summary>
        public long VolunteerId { get; set; }
    }
}
