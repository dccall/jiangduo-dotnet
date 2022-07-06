using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Core.Enums
{
    /// <summary>
    /// 操作状态
    /// </summary>
    public enum OperLogStatus
    {
        [Description("成功")]
        Success = 0,
        [Description("失败")]
        Fail = 1
    }
    /// <summary>
    ///  操作类别
    /// </summary>
    public enum OperatorType
    {
        [Description("其他")]
        Normal = 0,
        [Description("后台")]
        Backstage = 1,
        [Description("手机")]
        Phone = 2
    }


}
