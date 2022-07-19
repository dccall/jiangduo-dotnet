using JiangDuo.Core.Base;
using JiangDuo.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JiangDuo.Core.Models
{
    /// <summary>
    /// 通知公告表
    /// </summary>
    [Table("sys_notice")]
    public partial class SysNotice: BaseEntity
    {
        /// <summary>
        /// 公告标题
        /// </summary>
        [MaxLength(50)]
        public string NoticeTitle { get; set; } = null!;
        /// <summary>
        /// 公告类型
        /// </summary>
        public NoticeType Type { get; set; }
        /// <summary>
        /// 公告内容
        /// </summary>
        public string NoticeContent { get; set; }
        /// <summary>
        /// 公告状态
        /// </summary>
        public NoticeStatus Status { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        public string Remark { get; set; }
    }
}
