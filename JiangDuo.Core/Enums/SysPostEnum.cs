using System.ComponentModel;

namespace JiangDuo.Core.Enums
{
    /// <summary>
    /// 职位状态
    /// </summary>
    public enum PostStatus
    {
        [Description("正常")]
        Normal = 0,

        [Description("停用")]
        Stop = 1
    }
}