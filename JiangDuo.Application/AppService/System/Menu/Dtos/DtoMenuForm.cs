using JiangDuo.Core.Base;
using JiangDuo.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.Menu.Dtos
{
    public class DtoMenuForm 
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long? Id { get;  set; }
        /// <summary>
        /// 菜单/按钮标题
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "菜单名称")]
        public  string Title { get; set; }

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
        public long? ParentId { get; set; }
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


    }
}
