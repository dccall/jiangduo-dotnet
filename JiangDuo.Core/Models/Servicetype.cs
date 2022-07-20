using JiangDuo.Core.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JiangDuo.Core.Models
{
    /// <summary>
    /// 服务类型
    /// </summary>
    [Table("Servicetype")]
    public partial class Servicetype:BaseEntity
    {
        /// <summary>
        /// 类型名称
        /// </summary>
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
