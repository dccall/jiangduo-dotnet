using JiangDuo.Core.Base;
using JiangDuo.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [MaxLength(100)]
        public string ServiceName { get; set; } = null!;
        /// <summary>
        /// 面向群体
        /// </summary>
        [MaxLength(50)]
        public string GroupOriented { get; set; }
        
        /// <summary>
        /// 服务类型
        /// </summary>
        public long ServiceTypeId { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [MaxLength(255)]
        public string Remarks { get; set; }
        /// <summary>
        /// 计划人数
        /// </summary>
        public int? PlanNumber { get; set; }
        /// <summary>
        /// 计划开始时间
        /// </summary>
        public DateTime? PlanStartTime { get; set; }
        /// <summary>
        /// 计划结束时间
        /// </summary>
        public DateTime? PlanEndTime { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [MaxLength(255)]
        public string Address { get; set; }
        /// <summary>
        /// 选区id
        /// </summary>
        public long? SelectAreaId { get; set; }
        /// <summary>
        /// 村 范围
        /// </summary>
        public string VillagesRange { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public ServiceStatusEnum? Status { get; set; }
        /// <summary>
        /// 人大名单id
        /// </summary>
        public long? OfficialsId { get; set; }

        /// <summary>
        /// 关联工单Id
        /// </summary>
        public long? WorkOrderId { get; set; }

        /// <summary>
        /// 附件
        /// </summary>
        public string Attachments { get; set; }
    }
}
