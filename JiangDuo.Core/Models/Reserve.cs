using JiangDuo.Core.Base;
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
        public DateTimeOffset ReserveDate { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTimeOffset StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTimeOffset? EndTime { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [MaxLength(255)]
        public string Remarks { get; set; }
        /// <summary>
        /// 会议结果
        /// </summary>
        [MaxLength(255)]
        public string MeetingResults { get; set; }
        /// <summary>
        /// 所属区域
        /// </summary>
        public long? SelectAreaId { get; set; }
    }
}
