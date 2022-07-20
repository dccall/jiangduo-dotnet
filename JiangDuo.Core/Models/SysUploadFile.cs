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
    public class SysUploadFile : BaseEntity
    {
        /// <summary>
        /// 原文件名
        /// </summary>
        [MaxLength(100)]
        public string OldName { get; set; }
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


    }
}
