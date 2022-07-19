using JiangDuo.Core.Base;
using JiangDuo.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JiangDuo.Core.Models
{
    /// <summary>
    /// 场地设备
    /// </summary>
    [Table("Venuedevice")]
    public partial class Venuedevice:BaseEntity
    {
        /// <summary>
        /// 1.场地，2.设备
        /// </summary>
        public VenuedeviceEnum Type { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 所属建筑Id
        /// </summary>
        public long? BuildingId { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Remarks { get; set; }
    }
}
