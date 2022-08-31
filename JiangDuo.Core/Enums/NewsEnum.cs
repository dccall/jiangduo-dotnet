using System.ComponentModel;

namespace JiangDuo.Core.Enums
{
    public enum NewsStatus
    {
        [Description("默认")]
        Normal = 0,

        [Description("草稿")]
        Draft = 1,

        [Description("未发布")]
        NoPublish = 2,

        [Description("已发布")]
        Publish = 3
    }
}