using JiangDuo.Core.Base;
using JiangDuo.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JiangDuo.Core.Models
{
    /// <summary>
    /// 参数配置表
    /// </summary>
    [Table("sys_config")]
    public partial class SysConfig: EntityBase
    {
        /// <summary>
        /// 参数名称
        /// </summary>
        [MaxLength(100)]
        public string ConfigName { get; set; }
        /// <summary>
        /// 参数键名
        /// </summary>
        [MaxLength(100)]
        public string ConfigKey { get; set; }
        /// <summary>
        /// 参数键值
        /// </summary>
        [MaxLength(500)]
        public string ConfigValue { get; set; }
        /// <summary>
        /// 系统内置0是 1.否
        /// </summary>
        public ConfigType Type { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        public string Remark { get; set; }
    }
}
