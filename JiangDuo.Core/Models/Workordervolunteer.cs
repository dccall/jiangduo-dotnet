using JiangDuo.Core.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JiangDuo.Core.Models
{
    /// <summary>
    /// 工单志愿者
    /// </summary>
    [Table("Workordervolunteer")]
    public partial class Workordervolunteer : BaseEntity
    {
        /// <summary>
        /// 工单Id
        /// </summary>
        public long WordOrderId { get; set; }
        /// <summary>
        /// 志愿者Id
        /// </summary>
        public long VolunteerId { get; set; }
    }
}
