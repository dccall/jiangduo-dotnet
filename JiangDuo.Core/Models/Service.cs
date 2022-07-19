using JiangDuo.Core.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JiangDuo.Core.Models
{
    /// <summary>
    /// 服务
    /// </summary>
    [Table("Service")]
    public partial class Service:BaseEntity
    {
        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName { get; set; } = null!;
        /// <summary>
        /// 服务类型
        /// </summary>
        public long ServiceTypeId { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Remarks { get; set; }
        /// <summary>
        /// 计划人数
        /// </summary>
        public int? PlanNumber { get; set; }
        /// <summary>
        /// 计划开始时间
        /// </summary>
        public DateOnly? PlanStartTime { get; set; }
        /// <summary>
        /// 计划结束时间
        /// </summary>
        public DateOnly? PlanEndTime { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 选区id
        /// </summary>
        public long? AreaId { get; set; }
        /// <summary>
        /// 村 范围
        /// </summary>
        public string VillagesRange { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int? Status { get; set; }
        /// <summary>
        /// 人大名单id
        /// </summary>
        public long? OfficialsId { get; set; }
    }
}
