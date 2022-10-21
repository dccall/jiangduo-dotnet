using System.ComponentModel.DataAnnotations;

namespace JiangDuo.Application.AppService.VillageService.Dto
{
    public class DtoVillageForm
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long? Id { get; set; }

        /// <summary>
        /// 村名称
        /// </summary>
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        /// <summary>
        /// 选区id
        /// </summary>
        public long? SelectAreaId { get; set; }

        /// <summary>
        /// 人口数
        /// </summary>
        public long Population { get; set; }

        /// <summary>
        /// 总面积
        /// </summary>
        public double GrossArea { get; set; }

        /// <summary>
        /// 分类归属
        /// </summary>
        public string Classify { get; set; }
    }
}