using JiangDuo.Core.Base;
using JiangDuo.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.System.Dept.Dtos
{
    public class DtoDeptForm
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long? Id { get;  set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        [MaxLength(50)]
        public string DeptName { get; set; }
        /// <summary>
        /// 部门编号
        /// </summary>
        [MaxLength(50)]
        public string DeptCode { get; set; }
        /// <summary>
        /// 父部门id
        /// </summary>
        public long? ParentId { get; set; }
        /// <summary>
        /// 祖级列表
        /// </summary>
        [MaxLength(500)]
        public string Ancestors { get; set; }
        /// <summary>
        /// 显示顺序
        /// </summary>
        public int Order { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        [MaxLength(50)]
        public string Leader { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        [MaxLength(11)]
        public string Phone { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [MaxLength(100)]
        public string Email { get; set; }
        /// <summary>
        /// 部门状态（0正常 1停用）
        /// </summary>
        public DeptStatus Status { get; set; }

    }
}
