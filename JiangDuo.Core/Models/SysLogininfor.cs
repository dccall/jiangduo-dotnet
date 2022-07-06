using JiangDuo.Core.Enums;
using Furion.DatabaseAccessor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JiangDuo.Core.Models
{
    /// <summary>
    /// 系统访问记录
    /// </summary>
    [Table("sys_logininfor")]
    public partial class SysLogininfor : IEntity
    {
        [Key]
        public long Id { get; set; }
        /// <summary>
        /// 用户账号
        /// </summary>
        [MaxLength(50)]
        public string UserName { get; set; }
        /// <summary>
        /// 登录IP地址
        /// </summary>
        [MaxLength(128)]
        public string Ipaddr { get; set; }
        /// <summary>
        /// 登录地点
        /// </summary>
        [MaxLength(255)]
        public string LoginLocation { get; set; }
        /// <summary>
        /// 浏览器类型
        /// </summary>
        [MaxLength(50)]
        public string Browser { get; set; }
        /// <summary>
        /// 操作系统
        /// </summary>
        [MaxLength(50)]
        public string Os { get; set; }
        /// <summary>
        /// 登录状态
        /// </summary>
        public LogininforStatus Status { get; set; }
        /// <summary>
        /// 提示消息
        /// </summary>
        [MaxLength(255)]
        public string Msg { get; set; }
        /// <summary>
        /// 访问时间
        /// </summary>
        public DateTime? LoginTime { get; set; }
    }
}
