using JiangDuo.Core.Base;
using JiangDuo.Core.Enums;
using JiangDuo.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.PublicSentimentService.Dto
{
    public class DtoPublicSentimentForm
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long? Id { get;  set; }
        /// <summary>
        /// 居民Id
        /// </summary>
        public long ResidentId { get; set; }
        /// <summary>
        /// 业务类型
        /// </summary>
        public long BusinessId { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 需求状态
        /// </summary>
        public PublicSentimentStatus Status { get; set; }
        /// <summary>
        /// 附件
        /// </summary>
        public string Attachments { get; set; }

        /// <summary>
        /// 附件列表
        /// </summary>
        public List<SysUploadFile> AttachmentsList { get; set; } = new List<SysUploadFile>();
        /// <summary>
        /// 所属选区Id
        /// </summary>
        public long? SelectAreaId { get; set; }
    }
}
