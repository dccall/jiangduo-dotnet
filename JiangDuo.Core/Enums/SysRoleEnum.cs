using System.ComponentModel;

namespace JiangDuo.Core.Enums
{
    /// <summary>
    /// 角色状态
    /// </summary>
    public enum RoleStatus
    {
        [Description("正常")]
        Normal = 0,

        [Description("停用")]
        Stop = 1
    }
}