using JiangDuo.Core.Base;
using JiangDuo.Core.Enums;
using Furion.DatabaseAccessor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JiangDuo.Core.Models
{
    /// <summary>
    /// 字典表
    /// </summary>
    [Table("sys_dict")]
    [Index(nameof(DictName))]
    public partial class SysDict: Base.BaseEntity
    {
        /// <summary>
        /// 字典名称
        /// </summary>
        [MaxLength(100)]
        public string DictName { get; set; } = null!;
        /// <summary>
        /// 字典类型
        /// </summary>
        [MaxLength(100)]
        public string DictType { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public DictStatus Status { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        public string Remark { get; set; }

        /// <summary>
        /// 一对多
        /// </summary>
        public virtual ICollection<SysDictItem> SysDictItem { get; set; }
    }
}
