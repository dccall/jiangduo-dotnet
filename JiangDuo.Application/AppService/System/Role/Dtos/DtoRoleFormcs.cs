using JiangDuo.Core.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JiangDuo.Application.Role.Dtos
{
    public class DtoRoleFormcs
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long? Id { get; set; }

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

        /// <summary>
        /// 菜单id集合
        /// </summary>
        public List<long> MenuIdList { get; set; }
    }
}