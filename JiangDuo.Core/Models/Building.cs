using Furion.DatabaseAccessor;
using JiangDuo.Core.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JiangDuo.Core.Models
{
    /// <summary>
    /// 建筑
    /// </summary>
    [Table("Building")]
    public partial class Building : BaseEntity
    {
        /// <summary>
        /// 建筑名称
        /// </summary>
        public string BuildingName { get; set; } = null!;
        /// <summary>
        /// 建筑地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 建筑位置
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Remarks { get; set; }
        /// <summary>
        /// 建筑图片
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// 所属选区
        /// </summary>
        public long? AreaId { get; set; }
    }
}
