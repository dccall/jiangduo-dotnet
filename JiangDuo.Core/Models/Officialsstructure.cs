using Furion.DatabaseAccessor;
using JiangDuo.Core.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JiangDuo.Core.Models
{
    /// <summary>
    /// 人大结构
    /// </summary>
    [Table("Officialsstructure")]
    public partial class Officialsstructure : BaseEntity
    {
        /// <summary>
        /// 人大结构名称
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// 父级
        /// </summary>
        public long? ParentId { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Remarks { get; set; }
    }
}
