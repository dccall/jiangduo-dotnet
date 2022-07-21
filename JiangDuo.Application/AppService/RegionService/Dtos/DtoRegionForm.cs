using JiangDuo.Core.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.RegionService.Dto
{
    public class DtoRegionForm
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long? Id { get;  set; }
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
        public long? RegionClassifyId { get; set; }
        /// <summary>
        /// 引用的id
        /// </summary>
        public long? RelationId { get; set; }
    }
}
