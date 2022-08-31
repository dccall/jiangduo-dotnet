using System.ComponentModel;

namespace JiangDuo.Core.Enums
{
    /// <summary>
    /// 系统配置类型
    /// </summary>
    public enum ConfigType
    {
        [Description("默认")]
        Normal = 0,

        [Description("追加")]
        Append = 1
    }
}