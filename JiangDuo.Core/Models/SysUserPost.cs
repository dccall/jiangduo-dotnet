using Furion.DatabaseAccessor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JiangDuo.Core.Models
{
    /// <summary>
    /// 用户与岗位关联表
    /// </summary>
    [Table("sys_user_post")]
    public partial class SysUserPost : IEntity
    {
        [Key]
        public long Id { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// 岗位ID
        /// </summary>
        public long PostId { get; set; }
    }
}
