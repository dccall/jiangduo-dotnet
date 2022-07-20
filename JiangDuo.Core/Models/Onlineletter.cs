﻿using Furion.DatabaseAccessor;
using JiangDuo.Core.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JiangDuo.Core.Models
{
    /// <summary>
    /// 码上说马上办
    /// </summary>
    [Table("Onlineletter")]
    public partial class Onlineletter : BaseEntity
    {
        /// <summary>
        /// 居民Id
        /// </summary>
        public long? ResidentId { get; set; }
        /// <summary>
        /// 业务类型
        /// </summary>
        public long? BusinessId { get; set; }
        /// <summary>
        /// 人大名单Id
        /// </summary>
        public long? OfficialsId { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 附件
        /// </summary>
        public string Attachments { get; set; }
        /// <summary>
        /// 所属选区Id
        /// </summary>
        public long? SelectAreaId { get; set; }
    }
}
