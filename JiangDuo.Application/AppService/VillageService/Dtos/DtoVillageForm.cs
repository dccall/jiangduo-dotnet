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
    }
}