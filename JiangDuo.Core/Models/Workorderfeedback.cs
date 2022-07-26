using JiangDuo.Core.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [MaxLength(50)]
        public string HandlerName { get; set; } = null!;
        /// <summary>
        /// 处理内容
        /// </summary>
        [MaxLength(300)]
        public string HandlerContent { get; set; }
        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime? HandlerTime { get; set; }
        /// <summary>
        /// 发起人名称
        /// </summary>
        [MaxLength(50)]
        public string OriginatorName { get; set; }
        /// <summary>
        /// 发起人回复内容
        /// </summary>
        [MaxLength(300)]
        public string OriginatorContent { get; set; }
        /// <summary>
        /// 发起人回复时间
        /// </summary>
        public DateTime? OriginatorTime { get; set; }
    }
}
