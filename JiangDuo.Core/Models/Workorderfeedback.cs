using JiangDuo.Core.Base;
using JiangDuo.Core.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JiangDuo.Core.Models
{
    /// <summary>
    /// 工单反馈
    /// </summary>
    [Table("Workorderfeedback")]
    public partial class Workorderfeedback : BaseEntity
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
        /// 当前状态
        /// </summary>
        public WorkorderStatusEnum HandlerStatus { get; set; }
    }
}