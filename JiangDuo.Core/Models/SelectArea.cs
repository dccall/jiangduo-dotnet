using Furion.DatabaseAccessor;
using JiangDuo.Core.Base;
using JiangDuo.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace JiangDuo.Core.Models
{
    /// <summary>
    /// 选区
    /// </summary>
    [Table("SelectArea")]
    [Index(nameof(SelectAreaName))]
    public partial class SelectArea: BaseEntity
    {
        /// <summary>
        /// 选区名称
        /// </summary>
        [MaxLength(50)]
        public string SelectAreaName { get; set; } = null!;

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
        public long? Area { get; set; }
        /// <summary>
        /// 区域类型
        /// </summary>
        public SelectAreaTypeEnum? SelectAreaType { get; set; }
        /// <summary>
        /// 父级Id
        /// </summary>
        public long? ParentId { get; set; }
        /// <summary>
        /// 选区范围(坐标集合)
        /// </summary>
        public string SelectAreaRange { get; set; }
    }
}
