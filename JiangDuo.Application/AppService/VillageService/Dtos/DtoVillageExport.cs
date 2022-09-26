using Npoi.Mapper.Attributes;
using System.ComponentModel.DataAnnotations;

namespace JiangDuo.Application.AppService.VillageService.Dto
{
    public class DtoVillageExport
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Column("Id")]
        public string Id { get; set; }

        /// <summary>
        /// 村名称
        /// </summary>
        [Column("村名称")]
        public string Name { get; set; }

        /// <summary>
        /// 选区id
        /// </summary>
        [Column("选区Id")]
        public string SelectAreaId { get; set; }

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