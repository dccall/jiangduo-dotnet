using System.ComponentModel;

namespace JiangDuo.Core.Enums
{
    /// <summary>
    /// 文件来源
    /// </summary>
    public enum UploadFileSource
    {
        [Description("默认")]
        Null = 0,

        [Description("业务1")]
        Normal = 1
    }
}