using JiangDuo.Core.Base;
using JiangDuo.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.System.Dict.Dto
{
    public class DtoDictForm
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long? Id { get;  set; }
        /// 字典名称
        /// </summary>
        [MaxLength(100)]
        public string DictName { get; set; } = null!;
        /// <summary>
        /// 字典类型
        /// </summary>
        [MaxLength(100)]
        public string DictType { get; set; }
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
