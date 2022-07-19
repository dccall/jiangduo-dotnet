using Furion.DatabaseAccessor;
using JiangDuo.Core.Base;
using JiangDuo.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JiangDuo.Core.Models
{
    /// <summary>
    /// 选区
    /// </summary>
    [Table("Area")]
    public partial class Area: BaseEntity
    {
        /// <summary>
        /// 选区名称
        /// </summary>
        public string AreaName { get; set; } = null!;

        /// <summary>
        /// 省
        /// </summary>
        public long? Province { get; set; }
        /// <summary>
        /// 市区
        /// </summary>
        public long? City { get; set; }
        /// <summary>
        /// 区
        /// </summary>
        public long? Area1 { get; set; }
        /// <summary>
        /// 区域类型
        /// </summary>
        public AreaEnum? AreaType { get; set; }
        /// <summary>
        /// 父级Id
        /// </summary>
        public long? ParentId { get; set; }
        /// <summary>
        /// 选区范围
        /// </summary>
        public string AreaRange { get; set; }
    }
}
