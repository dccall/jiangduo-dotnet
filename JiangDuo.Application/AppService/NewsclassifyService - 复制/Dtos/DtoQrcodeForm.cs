using JiangDuo.Core.Base;
using JiangDuo.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.QrcodeService.Dto
{
    public class DtoQrcodeForm 
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long? Id { get;  set; }
        /// <summary>
        /// 名称
        /// </summary>
        [MaxLength(50)]
        public string Name { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        [MaxLength(500)]
        public string Content { get; set; }
        /// <summary>
        /// 附件
        /// </summary>
        [MaxLength(500)]
        public string Attachments { get; set; }

        /// <summary>
        /// 附件文件对象
        /// </summary>
        public List<SysUploadFile> AttachmentsFiles { get; set; } = new List<SysUploadFile>();
    }
}
