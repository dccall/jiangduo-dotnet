using JiangDuo.Core.Enums;
using Npoi.Mapper.Attributes;
using System.ComponentModel.DataAnnotations;

namespace JiangDuo.Application.AppService.SelectAreaService.Dto
{
    public class DtoSelectAreaExport
    {
        [Column("Id")]
        public string Id { get; set; }
        /// <summary>
        /// 选区名称
        /// </summary>
        [Column("选区名称")]
        public string SelectAreaName { get; set; } = null!;

        /// <summary>
        /// 区域类型1.区2.镇
        /// </summary>
        [Column("区域类型1.区2.镇")]
        public int? SelectAreaType { get; set; }

        /// <summary>
        /// 选区范围(坐标集合)
        /// </summary>
        [Column("选区范围")]
        public string SelectAreaRange { get; set; }

        /// <summary>
        /// 人口数
        /// </summary>
        [Column("人口数")]
        public string Population { get; set; }

        /// <summary>
        /// 总面积
        /// </summary>
        [Column("总面积")]
        public string GrossArea { get; set; }
    }
}