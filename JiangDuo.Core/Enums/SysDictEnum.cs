using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Core.Enums
{
    
    /// <summary>
    /// 字典状态
    /// </summary>
    public enum DictStatus
    {
        [Description("正常")]
        Normal = 0,
        [Description("停用")]
        Stop = 1
    }
}
