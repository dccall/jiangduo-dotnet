using JiangDuo.Core.Base;
using JiangDuo.Core.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JiangDuo.Core.Models
{
    /// <summary>
    /// 工单
    /// </summary>
    [Table("Workorder")]
    [Index(nameof(WorkOrderNo))]
    public partial class Workorder : BaseEntity
    {
        /// <summary>
        /// 工单编号
        /// </summary>
        [MaxLength(50)]
        public string WorkOrderNo { get; set; }
        /// <summary>
        /// 发起人
        /// </summary>
        public long OriginatorId { get; set; }
        /// <summary>
        /// 发起人名称
        /// </summary>
        [MaxLength(50)]
        public string OriginatorName{ get; set; }
        /// <summary>
        /// 接收人
        /// </summary>
        public long? RecipientId { get; set; }
        /// <summary>
        /// 接收人名称
        /// </summary>
        [MaxLength(50)]
        public string RecipientName { get; set; }

        /// <summary>
        /// 协助人
        /// </summary>
        public long? AssistantId { get; set; }
        /// <summary>
        /// 协助人名称
        /// </summary>
        [MaxLength(50)]
        public string AssistantName { get; set; }


        /// <summary>
        /// 业务类型
        /// </summary>
        public long? BusinessId { get; set; }
        /// <summary>
        /// 工单类型
        /// </summary>
        public WorkorderTypeEnum WorkorderType { get; set; }
        /// <summary>
        /// 工单来源
        /// </summary>
        public WorkorderSourceEnum WorkorderSource { get; set; }
        /// <summary>
        /// 工单状态
        /// </summary>
        public WorkorderStatusEnum Status { get; set; }
        /// <summary>
        /// 工单内容
        /// </summary>
        [MaxLength(300)]
        public string Content { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 完结时间
        /// </summary>
        public DateTime? OverTime { get; set; }
        /// <summary>
        /// 工单分数
        /// </summary>
        public int? Score { get; set; }
        /// <summary>
        /// 选区id
        /// </summary>
        public long? SelectAreaId { get; set; }
        /// <summary>
        /// 引用公共需求Id
        /// </summary>
        public long? PublicSentimentId { get; set; }


        /// <summary>
        /// 附件
        /// </summary>
        public string Attachments { get; set; }
    }
}
