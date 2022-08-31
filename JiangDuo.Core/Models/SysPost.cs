using JiangDuo.Core.Base;
using JiangDuo.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JiangDuo.Core.Models
{
    /// <summary>
    /// 岗位信息表
    /// </summary>
    [Table("sys_post")]
    public partial class SysPost : BaseEntity
    {
        /// <summary>
        /// 岗位编码
        /// </summary>
        [MaxLength(64)]
        public string PostCode { get; set; } = null!;

        /// <summary>
        /// 岗位名称
        /// </summary>
        [MaxLength(50)]
        public string PostName { get; set; } = null!;

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public PostStatus Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        public string Remark { get; set; }
    }
}