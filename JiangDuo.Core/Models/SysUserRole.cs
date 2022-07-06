using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JiangDuo.Core.Models
{
    /// <summary>
    /// 用户和角色关联表
    /// </summary>
    [Table("sys_user_role")]
    public partial class SysUserRole: IEntity, IEntitySeedData<SysUserRole>
    {
        [Key]
        public long Id { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        public long RoleId { get; set; }

        public IEnumerable<SysUserRole> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new List<SysUserRole>
            {
                new SysUserRole {
                    Id=1,
                    RoleId=1,
                    UserId=1,
                }
            };
        }
    }
}
