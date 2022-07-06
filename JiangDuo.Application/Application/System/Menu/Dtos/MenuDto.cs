using JiangDuo.Core;
using JiangDuo.Core.Enums;
using JiangDuo.Core.Models;
using Furion.DatabaseAccessor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.Menu.Dtos
{
    [Manual]
    public class MenuDto:SysMenu
    {
        /// <summary>
        /// 菜单/按钮标题
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "菜单名称")]
        public new string Title { get; set; }
       
    }
}
