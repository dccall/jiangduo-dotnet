using JiangDuo.Core.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JiangDuo.Core.Models
{
    /// <summary>
    /// 村落
    /// </summary>
    [Table("Village")]
    public partial class Village : BaseEntity
    {
        /// <summary>
        /// 村名称
        /// </summary>
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        /// <summary>
        /// 选区id
        /// </summary>
        public long? SelectAreaId { get; set; }
    }
}
