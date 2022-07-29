using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Core.Enums
{
    public enum PublicSentimentStatus
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
        [Description("完结已反馈")]
        FedBack = 3
    }
}
