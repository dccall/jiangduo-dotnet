using Furion.DatabaseAccessor;
using JiangDuo.Core.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JiangDuo.Core.Models
{
    /// <summary>
    /// 建筑
    /// </summary>
    [Table("Building")]
    [Index(nameof(BuildingName))]
    public partial class Building : BaseEntity
    {
        /// <summary>
        /// 建筑名称
        /// </summary>
        [MaxLength(50)]
        public string BuildingName { get; set; } = null!;
        /// <summary>
        /// 建筑地址
        /// </summary>
        [MaxLength(255)]
        public string Address { get; set; }
        /// <summary>
        /// 建筑位置
        /// </summary>
        [MaxLength(50)]
        public string Location { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [MaxLength(300)]
        public string Remarks { get; set; }
        /// <summary>
        /// 建筑图片
        /// </summary>
        //[MaxLength(255)]
        public string Images { get; set; }
        /// <summary>
        /// 所属选区
        /// </summary>
        public long? SelectAreaId { get; set; }
    }
}
