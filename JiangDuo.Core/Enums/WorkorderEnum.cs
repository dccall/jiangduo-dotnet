﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Core.Enums
{
    public enum WorkorderTypeEnum
    {

        [Description("默认")]
        Normal = 0,
        [Description("有事好商量(预约设施)")]
        Reserve = 1,
        [Description("一老一小（活动/服务）")]
        Service = 2,
        [Description("码上说马上办")]
        OnlineLetters = 3
    }

    public enum WorkorderSourceEnum
    {
        [Description("系统")]
        System = 0,
        [Description("居民")]
        Resident = 1,
        [Description("人大")]
        Official = 2,
    }


    public enum WorkorderStatusEnum
    {
        /// <summary>
        /// 默认
        /// </summary>
        [Description("默认")]
        Normal = 0,
        [Description("待处理")]
        NotProcessed = 1,
        [Description("进行中")]
        InProgress = 2,
        [Description("已反馈")]
        FedBack = 3,
        [Description("已完结")]
        End =4,
        [Description("已同意")]
        Approve = 5,
        [Description("已拒绝")]
        Reject =6
    }
    
}
