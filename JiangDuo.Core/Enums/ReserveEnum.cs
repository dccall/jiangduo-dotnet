using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Core.Enums
{
    public enum ReserveStatus
    {

        [Description("默认")]
        Normal = 0,
        [Description("审核中")]
        Audit = 1,
        [Description("审核通过")]
        Audited = 2,
        [Description("审核未通过")]
        AuditFailed = -1,
        [Description("已完成")]
        Completed = 3,
    }
}
