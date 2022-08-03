using JiangDuo.Core.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JiangDuo.Core.Models
{
    /// <summary>
    /// 服务分类
    /// </summary>
    [Table("serviceclassify")]
    public partial class ServiceClassify:BaseEntity
    {
        /// <summary>
        /// 分类名称
        /// </summary>
        [MaxLength(50)]
        public string ClassifyName { get; set; }
    }
}
