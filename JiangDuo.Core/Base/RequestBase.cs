﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Core.Base
{
    public class RequestBase
    {


        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
