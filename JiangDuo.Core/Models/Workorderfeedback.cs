using JiangDuo.Core.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JiangDuo.Core.Models
{
    /// <summary>
    /// 工单反馈
    /// </summary>
    [Table("Workorderfeedback")]
    public partial class Workorderfeedback:BaseEntity
    {
        /// <summary>
        ///工单id
        /// </summary>
        public long WordOrderId { get; set; }
        /// <summary>
        /// 处理人名称
        /// </summary>
        public string HandlerName { get; set; } = null!;
        /// <summary>
        /// 处理内容
        /// </summary>
        public string HandlerContent { get; set; }
        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTimeOffset? HandlerTime { get; set; }
        /// <summary>
        /// 发起人名称
        /// </summary>
        public string OriginatorName { get; set; }
        /// <summary>
        /// 发起人回复内容
        /// </summary>
        public string OriginatorContent { get; set; }
        /// <summary>
        /// 发起人回复时间
        /// </summary>
        public DateTimeOffset? OriginatorTime { get; set; }
    }
}
