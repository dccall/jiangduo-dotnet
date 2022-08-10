using JiangDuo.Core.Base;
using JiangDuo.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.NewsService.Dto
{
    public class DtoNewsForm
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
        [MaxLength(300)]
        public string CoverPhoto { get; set; }
        /// <summary>
        /// 文件对象
        /// </summary>
        public List<SysUploadFile> CoverPhotoFiles { get; set; }
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
