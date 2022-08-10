using JiangDuo.Core.Base;
using JiangDuo.Core.Enums;
using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JiangDuo.Core.Models
{
    /// <summary>
    /// 用户信息表
    /// </summary>
    [Table("sys_user")]
    public partial class SysUser: Base.BaseEntity, IEntitySeedData<SysUser>
    {
        /// <summary>
        /// 部门ID
        /// </summary>
        public long DeptId { get; set; }
        /// <summary>
        /// 用户账号
        /// </summary>
        [MaxLength(50)]
        public string UserName { get; set; } = null!;
        /// <summary>
        /// 用户昵称
        /// </summary>
        [MaxLength(50)]
        public string NickName { get; set; } = null!;
        /// <summary>
        /// 用户类型
        /// </summary>
        public UserType Type { get; set; }
        /// <summary>
        /// 用户邮箱
        /// </summary>
        [MaxLength(100)]
        public string Email { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        [MaxLength(11)]
        public string Phonenumber { get; set; }
        /// <summary>
        /// 用户性别（ 0未知 1男 2女）
        /// </summary>
        public UserSex Sex { get; set; } 
        /// <summary>
        /// 头像地址
        /// </summary>
        [MaxLength(300)]
        public string Avatar { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [MaxLength(100)]
        public string PassWord { get; set; }
        /// <summary>
        /// 帐号状态（0正常 1停用）
        /// </summary>
        public UserStatus Status { get; set; }
        /// <summary>
        /// 最后登录IP
        /// </summary>
        [MaxLength(128)]
        public string LoginIp { get; set; }
        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime? LoginDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        public string Remark { get; set; }


        /// <summary>
        /// 种子数据
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        public IEnumerable<SysUser> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new List<SysUser>
            {
                new SysUser {
                   Id=1,
                   NickName="管理员",
                   UserName="admin",
                   PassWord="9B6F539B39186126518612B27FB8504A",//admin123
                   CreatedTime=DateTime.UtcNow,
                }
            };
        }
    }
}
