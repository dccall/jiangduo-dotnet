using JiangDuo.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace JiangDuo.Application.System.DictItem.Dto
{
    public class DtoDictItemForm
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long? Id { get; set; }

        public long SysDictId { get; set; }

        /// <summary>
        /// 字典排序
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 字典标签
        /// </summary>
        [MaxLength(100)]
        public string Label { get; set; }

        /// <summary>
        /// 字典键值
        /// </summary>
        [MaxLength(100)]
        public string Value { get; set; }

        /// <summary>
        /// 是否默认
        /// </summary>
        public bool? IsDefault { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public DictStatus Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        public string Remark { get; set; }
    }
}