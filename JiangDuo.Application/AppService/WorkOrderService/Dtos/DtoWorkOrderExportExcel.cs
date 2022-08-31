using JiangDuo.Core.Enums;
using Npoi.Mapper.Attributes;
using System;

namespace JiangDuo.Application.AppService.WorkorderService.Dto
{
    public class DtoWorkOrderExportExcel
    {
        /// <summary>
        /// 主键
        /// </summary>
        //[Column("工单id")]
        [Ignore]
        public string Id { get; set; }

        /// <summary>
        /// 工单编号
        /// </summary>
        [Column("工单编号")]
        public string WorkOrderNo { get; set; }

        /// <summary>
        /// 发起人
        /// </summary>
        //[Column("发起人id")]
        [Ignore]
        public string OriginatorId { get; set; }

        /// <summary>
        /// 发起人名称
        /// </summary>
        [Column("发起人")]
        public string OriginatorName { get; set; }

        /// <summary>
        /// 接收人
        /// </summary>
        //[Column("接收人id")]
        [Ignore]
        public string RecipientId { get; set; }

        /// <summary>
        /// 接收人名称
        /// </summary>
        [Column("接收人")]
        public string RecipientName { get; set; }

        /// <summary>
        /// 业务类型
        /// </summary>
        //[Column("业务类型id")]
        [Ignore]
        public string BusinessId { get; set; }

        /// <summary>
        /// 业务类型名称
        /// </summary>
        [Column("业务类型")]
        public string BusinessName { get; set; }

        /// <summary>
        /// 工单类型
        /// </summary>
        //[Column("工单类型值")]
        [Ignore]
        public WorkorderTypeEnum WorkorderType { get; set; } = WorkorderTypeEnum.Normal;

        /// <summary>
        /// 工单类型名称
        /// </summary>
        [Column("工单类型")]
        public string WorkorderTypeName => WorkorderType.GetDescription();

        /// <summary>
        /// 工单来源
        /// </summary>
        //[Column("工单来源值")]
        [Ignore]
        public WorkorderSourceEnum WorkorderSource { get; set; } = WorkorderSourceEnum.System;

        /// <summary>
        /// 工单来源名称
        /// </summary>
        [Column("工单来源")]
        public string WorkorderSourceName => WorkorderSource.GetDescription();

        /// <summary>
        /// 工单状态
        /// </summary>
        //[Column("工单状态值")]
        [Ignore]
        public WorkorderStatusEnum Status { get; set; } = WorkorderStatusEnum.NotProcessed;

        /// <summary>
        /// 工单状态名称
        /// </summary>
        [Column("工单状态")]
        public string StatusName => Status.GetDescription();

        /// <summary>
        /// 工单内容
        /// </summary>
        [Column("工单内容")]
        public string Content { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("创建时间", CustomFormat = "yyyy-MM-dd", DefaultValue = "")]
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [Column("开始时间", CustomFormat = "yyyy-MM-dd", DefaultValue = "")]
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 完结时间
        /// </summary>
        [Column("完结时间", CustomFormat = "yyyy-MM-dd", DefaultValue = "")]
        public DateTime? OverTime { get; set; }

        /// <summary>
        /// 工单分数
        /// </summary>
        [Ignore]
        public string Score { get; set; }

        /// <summary>
        /// 选区id
        /// </summary>
        [Ignore]
        public string SelectAreaId { get; set; }

        /// <summary>
        /// 所属选区
        /// </summary>
        [Column("所属选区")]
        public string SelectAreaName { get; set; }
    }
}