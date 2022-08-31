using JiangDuo.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace JiangDuo.Application.System.Post.Dto
{
    public class DtoPostForm
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long? Id { get; set; }

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