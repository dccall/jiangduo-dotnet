using System.ComponentModel;

namespace JiangDuo.Core.Enums
{
    /// <summary>
    /// 部门状态
    /// </summary>
    public enum DeptStatus
    {
        [Description("正常")]
        Normal = 0,

        [Description("停用")]
        Stop = 1
    }
}