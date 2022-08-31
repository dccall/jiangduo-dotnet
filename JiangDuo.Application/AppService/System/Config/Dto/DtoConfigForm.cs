using JiangDuo.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace JiangDuo.Application.System.Config.Dto
{
    public class DtoConfigForm
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long? Id { get; set; }

        /// <summary>
        /// 参数名称
        /// </summary>
        [MaxLength(100)]
        public string ConfigName { get; set; }

        /// <summary>
        /// 参数键名
        /// </summary>
        [MaxLength(100)]
        public string ConfigKey { get; set; }

        /// <summary>
        /// 参数键值
        /// </summary>
        [MaxLength(500)]
        public string ConfigValue { get; set; }

        /// <summary>
        /// 系统内置0是 1.否
        /// </summary>
        public ConfigType Type { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        public string Remark { get; set; }
    }
}