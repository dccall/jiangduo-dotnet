using Furion.DatabaseAccessor;
using JiangDuo.Core.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JiangDuo.Core.Models
{
    /// <summary>
    /// 角色信息表
    /// </summary>
    [Table("sys_role")]
    public partial class SysRole : Base.BaseEntity, IEntitySeedData<SysRole>
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        [MaxLength(50)]
        public string RoleName { get; set; } = null!;

        /// <summary>
        /// 角色状态（0正常 1停用）
        /// </summary>
        public RoleStatus Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        public string Remark { get; set; }

        public IEnumerable<SysRole> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new List<SysRole>
            {
                new SysRole {Id=1, RoleName="管理员",CreatedTime=DateTime.UtcNow }
            };
        }
    }
}