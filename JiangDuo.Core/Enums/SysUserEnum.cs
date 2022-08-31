using System.ComponentModel;

namespace JiangDuo.Core.Enums
{
    /// <summary>
    /// 用户类型
    /// </summary>
    public enum UserType
    {
        [Description("默认")]
        Normal = 0,

        [Description("系统用户")]
        System = 1
    }

    /// <summary>
    /// 用户性别
    /// </summary>
    public enum UserSex
    {
        [Description("未知")]
        Normal = 0,

        [Description("男")]
        Male = 1,

        [Description("女")]
        Female = 2
    }

    /// <summary>
    /// 用户状态
    /// </summary>
    public enum UserStatus
    {
        [Description("正常")]
        Normal = 0,

        [Description("停用")]
        Stop = 1
    }
}