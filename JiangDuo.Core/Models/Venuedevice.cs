using JiangDuo.Core.Base;
using JiangDuo.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [MaxLength(50)]
        public string Name { get; set; }
        /// <summary>
        /// 所属建筑Id
        /// </summary>
        public long? BuildingId { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [MaxLength(300)]
        public string Remarks { get; set; }

        /// <summary>
        /// 规则制度id
        /// </summary>
        public long? RegulationId { get; set; }
    }
}
