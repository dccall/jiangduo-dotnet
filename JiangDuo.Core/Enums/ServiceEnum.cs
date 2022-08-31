using System.ComponentModel;

namespace JiangDuo.Core.Enums
{
    public enum ServiceStatusEnum
    {
        [Description("默认")]
        Normal = 0,

        [Description("审核中")]
        Audit = 1,

        [Description("审核通过")]
        Audited = 2,

        [Description("审核未通过")]
        AuditFailed = -1,

        [Description("已发布")]
        Published = 3,

        [Description("已结束")]
        Ended = 4
    }

    public enum ServiceTypeEnum
    {
        [Description("默认")]
        Normal = 0,

        [Description("常规")]
        General = 1,

        [Description("长期")]
        LongTerm = 2,
    }

    public enum ParticipantStatus
    {
        [Description("默认")]
        Normal = 0,

        [Description("占位")]
        Occupancy = 1
    }
}