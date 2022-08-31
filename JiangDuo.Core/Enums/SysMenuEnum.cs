using System.ComponentModel;

namespace JiangDuo.Core.Enums
{
    /// <summary>
    /// 菜单类型
    /// </summary>
    public enum MenuType
    {
        [Description("菜单")]
        Menu = 0,

        [Description("按钮")]
        Button = 1
    }

    /// <summary>
    /// 菜单状态
    /// </summary>
    public enum MenuStatus
    {
        [Description("正常")]
        Normal = 0,

        [Description("停用")]
        Stop = 1
    }

    /// <summary>
    /// 菜单显示状态
    /// </summary>
    public enum MenuVisible
    {
        [Description("显示")]
        Show = 0,

        [Description("隐藏")]
        Hide = 0,
    }
}