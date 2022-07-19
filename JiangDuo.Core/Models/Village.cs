using JiangDuo.Core.Base;
using System;
using System.Collections.Generic;
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
        public string Name { get; set; } = null!;
        /// <summary>
        /// 选区id
        /// </summary>
        public long? AreaId { get; set; }
    }
}
