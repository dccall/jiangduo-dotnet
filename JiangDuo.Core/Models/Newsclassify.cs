using Furion.DatabaseAccessor;
using JiangDuo.Core.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JiangDuo.Core.Models
{
    /// <summary>
    /// 新闻分类
    /// </summary>
    [Table("Newsclassify")]
    public partial class Newsclassify : BaseEntity
    {
        /// <summary>
        /// 分类名称
        /// </summary>
        public string ClassifyName { get; set; } = null!;
    }
}
