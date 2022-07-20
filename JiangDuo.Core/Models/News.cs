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
    /// 新闻
    /// </summary>
    [Table("News")]
    [Index(nameof(Title))]
    public partial class News : BaseEntity
    {
        /// <summary>
        /// 新闻标题
        /// </summary>
        [MaxLength(50)]
        public string Title { get; set; } = null!;
        /// <summary>
        /// 子标题
        /// </summary>
        [MaxLength(50)]
        public string Subtitle { get; set; }
        /// <summary>
        /// 封面
        /// </summary>
        [MaxLength(255)]
        public string CoverPhoto { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        [MaxLength(50)]
        public string Author { get; set; }
        /// <summary>
        /// 新闻分类
        /// </summary>
        public long? NewsClassifyId { get; set; }
        /// <summary>
        /// 引用的id
        /// </summary>
        public long? RelationId { get; set; }
    }
}
