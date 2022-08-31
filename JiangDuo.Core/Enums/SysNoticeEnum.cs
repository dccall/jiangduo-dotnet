using System.ComponentModel;

namespace JiangDuo.Core.Enums
{
    /// <summary>
    /// 公告状态
    /// </summary>
    public enum NoticeStatus
    {
        [Description("正常")]
        Normal = 0,

        [Description("关闭")]
        Stop = 1
    }

    /// <summary>
    /// 公告类型
    /// </summary>
    public enum NoticeType
    {
        [Description("通知")]
        Notice = 0,

        [Description("公告")]
        ANotice = 1
    }
}