using JiangDuo.Core.Base;
using System;
using System.Collections.Generic;

namespace JiangDuo.Core.Models
{
    /// <summary>
    /// 服务类型
    /// </summary>
    public partial class Servicetype:BaseEntity
    {
        /// <summary>
        /// 类型名称
        /// </summary>
        public string Name { get; set; }
    }
}
