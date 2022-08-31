using JiangDuo.Core.Models;
using System.Collections.Generic;

namespace JiangDuo.Application.AppService.OnlineletterService.Dto
{
    public class DtoOnlineletterForm
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long? Id { get; set; }

        /// <summary>
        /// 居民Id
        /// </summary>
        public long? ResidentId { get; set; }

        /// <summary>
        /// 业务类型
        /// </summary>
        public long? BusinessId { get; set; }

        /// <summary>
        /// 人大名单Id
        /// </summary>
        public long? OfficialsId { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 附件
        /// </summary>
        public string Attachments { get; set; }

        /// <summary>
        /// 附件文件对象
        /// </summary>
        public List<SysUploadFile> AttachmentsFiles { get; set; }

        /// <summary>
        /// 所属选区Id
        /// </summary>
        public long? SelectAreaId { get; set; }

        /// <summary>
        /// 关联工单Id
        /// </summary>
        public long? WorkOrderId { get; set; }
    }
}