using JiangDuo.Core.Base;
using JiangDuo.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JiangDuo.Core.Models
{
    /// <summary>
    /// 有事好商量(服务)
    /// </summary>
    [Table("Reserve")]
    public partial class Reserve : BaseEntity
    {
        /// <summary>
        /// 主题
        /// </summary>
        [MaxLength(50)]
        public string Theme { get; set; } = null!;
        /// <summary>
        /// 场地设备
        /// </summary>
        public long VenueDeviceId { get; set; }
        /// <summary>
        /// 人数
        /// </summary>
        public int? Number { get; set; }
        /// <summary>
        /// 预约日期
        /// </summary>
        public DateTime ReserveDate { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [MaxLength(255)]
        public string Remarks { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public ReserveStatus Status { get; set; }
        /// <summary>
        /// 审核结果
        /// </summary>
        [MaxLength(255)]
        public string AuditFindings { get; set; }
        /// <summary>
        /// 会议结果
        /// </summary>
        [MaxLength(255)]
        public string MeetingResults { get; set; }
        /// <summary>
        /// 所属区域
        /// </summary>
        public long? SelectAreaId { get; set; }
        /// <summary>
        /// 关联工单Id
        /// </summary>
        public long? WorkOrderId { get; set; }
    }
}
