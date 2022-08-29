using Furion.DatabaseAccessor;
using JiangDuo.Core.Attributes;
using JiangDuo.Core.Base;
using JiangDuo.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Core.Models
{
    /// <summary>
    /// 文件上传表
    /// </summary>
    [Table("sys_uploadfile")]
    public class SysUploadFile : IEntity
    {
        [Key]
        public long FileId { get; set; }
        /// <summary>
        /// 原文件名
        /// </summary>
        [MaxLength(100)]
        public string Name { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        [MaxLength(100)]
        public string FileName { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        [MaxLength(300)]
        public string FilePath { get; set; }
        /// <summary>
        /// Url
        /// </summary>
        [MaxLength(300)]
        public string Url { get; set; }
        /// <summary>
        /// 文件来源
        /// </summary>
        public UploadFileSource FileSource { get; set; }
        /// <summary>
        /// 文件后缀
        /// </summary>
        [MaxLength(20)]
        public string FileExt { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        public long FileLength { get; set; }


        /// <summary>
		/// 创建者
		/// </summary>
		public long Creator { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdatedTime { get; set; }
        /// <summary>
        /// 更新者
        /// </summary>
        public long? Updater { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }

    }
}
