using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Core.Enums
{
    public enum ServiceStatusEnum
    {

        [Description("默认")]
        Normal = 0,
        [Description("审核中")]
        Approval = 1,
        [Description("审核通过")]
        Approved = 2,
        [Description("已发布")]
        Published = 3,
        [Description("已结束")]
        Ended =4
    }
}
