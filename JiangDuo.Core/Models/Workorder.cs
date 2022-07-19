using JiangDuo.Core.Base;
using JiangDuo.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JiangDuo.Core.Models
{
    /// <summary>
    /// 工单
    /// </summary>
    [Table("Workorder")]
    public partial class Workorder : BaseEntity
    {
        /// <summary>
        /// 发起人
        /// </summary>
        public long OriginatorId { get; set; }
        /// <summary>
        /// 接收人
        /// </summary>
        public long ReceiverId { get; set; }
        /// <summary>
        /// 工单类型
        /// </summary>
        public WorkorderTypeEnum Type { get; set; }
        /// <summary>
        /// 引用的业务id
        /// </summary>
        public long? RelationId { get; set; }
        /// <summary>
        /// 选区id
        /// </summary>
        public long? AreaId { get; set; }
        /// <summary>
        /// 工单状态
        /// </summary>
        public WorkorderStatusEnum Status { get; set; }
        /// <summary>
        /// 工单内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTimeOffset StartTime { get; set; }
        /// <summary>
        /// 完结时间
        /// </summary>
        public DateTimeOffset? OverTime { get; set; }
    }
}
