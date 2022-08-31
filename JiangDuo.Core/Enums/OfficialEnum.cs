using System.ComponentModel;

namespace JiangDuo.Core.Enums
{
    /// <summary>
    /// 人大角色类型
    /// </summary>
    public enum OfficialRoleEnum
    {
        [Description("人大")]
        Normal = 0,

        [Description("主席")]
        Chairman = 1
    }
}