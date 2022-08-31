using JiangDuo.Core.Base;
using JiangDuo.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JiangDuo.Core.Models
{
    /// <summary>
    /// 规章制度
    /// </summary>
    [Table("Regulation")]
    public partial class Regulation : BaseEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        [MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public VenuedeviceEnum Type { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
    }
}