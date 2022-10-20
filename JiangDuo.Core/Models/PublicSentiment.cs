using JiangDuo.Core.Base;
using JiangDuo.Core.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace JiangDuo.Core.Models
{
    /// <summary>
    /// 公共需求
    /// </summary>
    [Table("Publicsentiment")]
    public partial class PublicSentiment : BaseEntity
    {
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
        /// 附件
        /// </summary>
        public string Attachments { get; set; }

        /// <summary>
        /// 工单类型
        /// </summary>
        public WorkorderTypeEnum WorkorderType { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public PublicSentimentStatus Status { get; set; }

        /// <summary>
        /// 所属选区Id
        /// </summary>
        public long? SelectAreaId { get; set; }

        /// <summary>
        /// 反馈人Id
        /// </summary>
        public long? FeedbackPersonId { get; set; }

        /// <summary>
        /// 反馈人
        /// </summary>
        public string FeedbackPerson { get; set; }

        /// <summary>
        /// 反馈内容
        /// </summary>
        public string FeedbackContent { get; set; }

        /// <summary>
        /// 反馈时间
        /// </summary>
        public DateTime? FeedbackTime { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string PhoneNumber { get; set; }
    }
}