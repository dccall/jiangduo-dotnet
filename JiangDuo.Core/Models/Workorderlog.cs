﻿using JiangDuo.Core.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JiangDuo.Core.Models
{
    /// <summary>
    /// 工单日志
    /// </summary>
    [Table("Workorderlog")]
    public partial class Workorderlog:BaseEntity
    {
        /// <summary>
        /// 工单id
        /// </summary>
        public long? WordOrderId { get; set; }
        /// <summary>
        /// 日志内容
        /// </summary>
        [MaxLength(300)]
        public string LogContent { get; set; }
        /// <summary>
        /// 日志时间
        /// </summary>
        public DateTimeOffset LogTime { get; set; }
    }
}
