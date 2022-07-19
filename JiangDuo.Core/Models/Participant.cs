﻿using JiangDuo.Core.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JiangDuo.Core.Models
{
    /// <summary>
    /// 参与人
    /// </summary>
    [Table("Participant")]
    public partial class Participant: BaseEntity
    {
        /// <summary>
        /// 服务Id
        /// </summary>
        public long? ServiceId { get; set; }
        /// <summary>
        /// 居民id
        /// </summary>
        public long? ResidentId { get; set; }
    }
}
