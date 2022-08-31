using Furion.DatabaseAccessor;
using JiangDuo.Core.Enums;
using JiangDuo.Core.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JiangDuo.Application.Role.Dtos
{
    [Manual]
    public class RoleDto : SysRole
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "角色名称")]
        public new string RoleName { get; set; }

        /// <summary>
        /// 菜单id集合
        /// </summary>
        public List<long> MenuIdList { get; set; }

        /// <summary>
        /// 状态名称
        /// </summary>
        public string RoleStatusName => Status.GetDescription();
    }
}