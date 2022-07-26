using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Core.Enums
{
    public enum SexEnum
    {
        [Description("未知")]
        Normal = 0,
        [Description("男")]
        Male = 1,
        [Description("女")]
        Female = 2
    }
}
