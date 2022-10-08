using System.ComponentModel;

namespace JiangDuo.Core.Enums;

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

/// <summary>
/// 人大类型
/// </summary>
public enum OfficialType
{
    [Description("默认值")]
    Normal = 0,

    [Description("区代表")]
    Area = 1,

    [Description("镇代表")]
    Town = 2,
}
