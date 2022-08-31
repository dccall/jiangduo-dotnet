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
    /// 菜单权限表
    /// </summary>
    [Table("sys_menu")]
    public partial class SysMenu : Base.BaseEntity, IEntitySeedData<SysMenu>
    {
        /// <summary>
        /// 菜单标题
        /// </summary>
        [MaxLength(50)]
        public string Title { get; set; } = null!;

        /// <summary>
        /// 权限编号
        /// </summary>
        [MaxLength(50)]
        public string Code { get; set; } = null!;

        /// <summary>
        /// 路由名称
        /// </summary>
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        /// <summary>
        /// 路由地址
        /// </summary>
        [MaxLength(200)]
        public string Path { get; set; } = null!;

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 父菜单ID
        /// </summary>
        public long? ParentId { get; set; } = -1;

        /// <summary>
        /// 是否为外链
        /// </summary>
        public int? IsFrame { get; set; }

        /// <summary>
        /// 外链地址
        /// </summary>
        [MaxLength(500)]
        public string Href { get; set; }

        /// <summary>
        /// 是否缓存
        /// </summary>
        public int? KeepAlive { get; set; }

        /// <summary>
        /// 是否隐藏
        /// </summary>
        public int? Hide { get; set; }

        /// <summary>
        /// 菜单类型
        /// </summary>
        public MenuType Type { get; set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        [MaxLength(100)]
        public string Icon { get; set; }

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
        public IEnumerable<SysMenu> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new List<SysMenu>
            {
                new SysMenu {
                    Id=1,
                    Title="首页",
                    Name="dashboard",
                    Path="/dashboard",
                    Icon="carbon:dashboard",
                    Order=0,
                    CreatedTime=DateTime.UtcNow,
                    Creator=0
                },
                new SysMenu {
                    Id=2,
                    Title="系统管理",
                    Name="system",
                    Path="/system",
                    Icon="carbon:dashboard",
                    Order=1,
                    CreatedTime=DateTime.UtcNow,
                    Creator=0
                },
                new SysMenu {
                    Id=3,
                    ParentId=2,
                    Title="用户管理",
                    Name="system_peopleConfig_user",
                    Path="/system/peopleConfig/user",
                    Icon="carbon:dashboard",
                    Order=1,
                    CreatedTime=DateTime.UtcNow,
                    Creator=0
                },
                new SysMenu {
                    Id=4,
                    ParentId=2,
                    Title="角色管理",
                    Name="system_peopleConfig_role",
                    Path="/system/peopleConfig/role",
                    Icon="carbon:dashboard",
                    Order=2,
                    CreatedTime=DateTime.UtcNow,
                    Creator=0
                },
                new SysMenu {
                    Id=5,
                    ParentId=2,
                    Title="菜单管理",
                    Name="system_menu",
                    Path="/system/menu",
                    Icon="carbon:dashboard",
                    Order=3,
                    CreatedTime=DateTime.UtcNow,
                    Creator=0
                },
                new SysMenu {
                    Id=6,
                    ParentId=2,
                    Title="部门管理",
                    Name="system_dept",
                    Path="/system/dept",
                    Icon="carbon:dashboard",
                    Order=5,
                    CreatedTime=DateTime.UtcNow,
                    Creator=0
                },
                new SysMenu {
                    Id=7,
                    ParentId=2,
                    Title="岗位管理",
                    Name="system_post",
                    Path="/system/post",
                    Icon="carbon:dashboard",
                    Order=6,
                    CreatedTime=DateTime.UtcNow,
                    Creator=0
                },
                new SysMenu {
                    Id=8,
                    ParentId=2,
                    Title="字典管理",
                    Name="system_dict",
                    Path="/system/dict",
                    Icon="carbon:dashboard",
                    Order=7,
                    CreatedTime=DateTime.UtcNow,
                    Creator=0
                }
            };
        }
    }
}