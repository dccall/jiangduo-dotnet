using JiangDuo.Core.Base;

namespace JiangDuo.Application.AppService.VillageService.Dto
{
    public class DtoVillageQuery : BaseRequest
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 选区
        /// </summary>
        public long? SelectAreaId { get; set; }
    }
}