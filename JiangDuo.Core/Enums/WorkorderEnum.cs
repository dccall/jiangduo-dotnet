using System;
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
        [Description("有事好商量")]
        Reserve = 1,
        [Description("一老一小")]
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
        [Description("已完成待审核")]
        Completed =3,
        [Description("审核通过")]
        Audited = 4,
        [Description("已完结")]
        End =5,
       
    }
    
}
