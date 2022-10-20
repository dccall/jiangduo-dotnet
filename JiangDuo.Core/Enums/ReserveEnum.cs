using System.ComponentModel;

namespace JiangDuo.Core.Enums
{
    public enum ReserveStatus
    {
        [Description("默认")]
        Normal = 0,

        [Description("审核中")]
        Audit = 1,

        [Description("审核通过")]
        Audited = 2,

        [Description("审核未通过")]
        AuditFailed = -1,

        [Description("已完成")]
        Completed = 3,

        [Description("已总结")]
        Over = 4,
    }
}