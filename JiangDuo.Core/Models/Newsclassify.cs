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
    /// 新闻分类
    /// </summary>
    [Table("Newsclassify")]
    [Index(nameof(ClassifyName))]
    public partial class Newsclassify : BaseEntity
    {
        /// <summary>
        /// 分类名称
        /// </summary>
        [MaxLength(50)]
        public string ClassifyName { get; set; } = null!;
    }
}
