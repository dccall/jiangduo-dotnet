using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Core.Enums
{
    /// <summary>
    /// 人大角色类型
    /// </summary>
    public enum OfficialRoleEnum
    {
        [Description("人大")]
        Normal = 0,
        [Description("主席")]
        Chairman = 1
    }
}
