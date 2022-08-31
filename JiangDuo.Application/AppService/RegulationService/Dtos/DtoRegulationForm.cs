using JiangDuo.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace JiangDuo.Application.AppService.RegulationService.Dto
{
    public class DtoRegulationForm
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long? Id { get; set; }

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