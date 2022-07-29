using Furion.DatabaseAccessor;
using JiangDuo.Core.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JiangDuo.Core.Models
{
    /// <summary>
    /// 业务类型
    /// </summary>
    [Table("Business")]
    public partial class Business : BaseEntity
    {
        /// <summary>
        /// 业务名称
        /// </summary>
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
