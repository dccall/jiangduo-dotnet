using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
