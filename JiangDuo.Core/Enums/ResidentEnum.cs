using System.ComponentModel;

namespace JiangDuo.Core.Enums
{
    public enum ResidentStatus
    {
        [Description("未实名")]
        Normal = 0,

        [Description("已认证")]
        Certified = 1
    }
}