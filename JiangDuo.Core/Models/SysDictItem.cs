using JiangDuo.Core.Base;
using JiangDuo.Core.Enums;
using Furion.DatabaseAccessor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JiangDuo.Core.Models
{
    /// <summary>
    /// 字典数据表
    /// </summary>
    [Table("sys_dictitem")]
    public partial class SysDictItem: BaseEntity
    {
        public long SysDictId { get; set; }
        /// <summary>
        /// 字典排序
        /// </summary>
        public int Order { get; set; }
        /// <summary>
        /// 字典标签
        /// </summary>
        [MaxLength(100)]
        public string Label { get; set; }
        /// <summary>
        /// 字典键值
        /// </summary>
        [MaxLength(100)]
        public string Value { get; set; }
        /// <summary>
        /// 是否默认
        /// </summary>
        public bool? IsDefault { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public DictStatus Status { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        public string Remark { get; set; }


    }
}
