using JiangDuo.Core.Base;
using JiangDuo.Core.Enums;
using System;
using System.Collections.Generic;

namespace JiangDuo.Core.Models
{
    /// <summary>
    /// 规章制度
    /// </summary>
    public partial class Regulation:BaseEntity
    {
        /// <summary>
        /// 类型
        /// </summary>
        public VenuedeviceEnum Type { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
    }
}
