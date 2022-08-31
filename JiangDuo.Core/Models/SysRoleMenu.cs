using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JiangDuo.Core.Models
{
    /// <summary>
    /// 角色和菜单关联表
    /// </summary>
    [Table("sys_role_menu")]
    public partial class SysRoleMenu : IEntity, IEntitySeedData<SysRoleMenu>
    {
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public long RoleId { get; set; }

        /// <summary>
        /// 菜单ID
        /// </summary>
        public long MenuId { get; set; }

        public IEnumerable<SysRoleMenu> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new List<SysRoleMenu>
            {
                new SysRoleMenu {Id=1,RoleId=1,MenuId=1},
                new SysRoleMenu {Id=2,RoleId=1,MenuId=2},
                new SysRoleMenu {Id=3,RoleId=1,MenuId=3},
                new SysRoleMenu {Id=4,RoleId=1,MenuId=4},
                new SysRoleMenu {Id=5,RoleId=1,MenuId=5},
                new SysRoleMenu {Id=6,RoleId=1,MenuId=6},
                new SysRoleMenu {Id=7,RoleId=1,MenuId=7},
                new SysRoleMenu {Id=8,RoleId=1,MenuId=8},
            };
        }
    }
}