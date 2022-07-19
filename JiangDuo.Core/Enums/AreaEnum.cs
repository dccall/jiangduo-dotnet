﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Core.Enums
{
    public enum AreaEnum
    {
        [Description("默认")]
        Normal = 0,
        [Description("区选区")]
        Area = 1,
        [Description("镇选区")]
        Town = 2
    }
}
