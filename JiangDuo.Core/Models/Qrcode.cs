using JiangDuo.Core.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JiangDuo.Core.Models
{
    /// <summary>
    /// 二维码
    /// </summary>
    [Table("Qrcode")]
    public class Qrcode : BaseEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        [MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [MaxLength(500)]
        public string Content { get; set; }

        /// <summary>
        /// 附件
        /// </summary>
        [MaxLength(500)]
        public string Attachments { get; set; }
    }
}