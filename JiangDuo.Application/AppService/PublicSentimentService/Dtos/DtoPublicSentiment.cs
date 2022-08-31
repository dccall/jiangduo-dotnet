using Furion.DatabaseAccessor;
using JiangDuo.Core.Enums;
using JiangDuo.Core.Models;
using System.Collections.Generic;

namespace JiangDuo.Application.AppService.PublicSentimentService.Dto
{
    [Manual]
    public class DtoPublicSentiment : PublicSentiment
    {
        //状态名称
        public string StatusName => Status.GetDescription();

        //工单类型名称
        public string WorkorderTypeName => WorkorderType.GetDescription();

        /// <summary>
        /// 居民名称
        /// </summary>
        public string ResidentName { get; set; }

        /// <summary>
        /// 业务类型名称
        /// </summary>
        public string BusinessName { get; set; }

        /// <summary>
        /// 所属选区名称
        /// </summary>
        public string SelectAreaName { get; set; }

        /// <summary>
        /// 附件列表
        /// </summary>
        public List<SysUploadFile> AttachmentsList { get; set; } = new List<SysUploadFile>();
    }
}