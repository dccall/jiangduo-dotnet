using System.ComponentModel;

namespace JiangDuo.Core.Enums
{
    public enum ReserveStatus
    {
        [Description("默认")]
        Normal = 0,

        [Description("待受理")]
        Audit = 1,

        [Description("已受理")]
        Audited = 2,

        [Description("已拒绝")]
        AuditFailed = -1,

        [Description("已完成")]
        Completed = 3,

        [Description("已总结")]
        Over = 4,
    }
}