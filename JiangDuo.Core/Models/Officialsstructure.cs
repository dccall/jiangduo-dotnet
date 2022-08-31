using JiangDuo.Core.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JiangDuo.Core.Models
{
    /// <summary>
    /// 人大结构
    /// </summary>
    [Table("Officialsstruct")]
    public partial class Officialsstruct : BaseEntity
    {
        /// <summary>
        /// 人大结构名称
        /// </summary>
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        /// <summary>
        /// 父级
        /// </summary>
        public long? ParentId { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [MaxLength(50)]
        public string Remarks { get; set; }
    }
}