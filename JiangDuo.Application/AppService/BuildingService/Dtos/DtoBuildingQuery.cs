using JiangDuo.Core.Base;

namespace JiangDuo.Application.AppService.BuildingService.Dto
{
    public class DtoBuildingQuery : BaseRequest
    {
        /// <summary>
        /// 所属选区
        /// </summary>
        public long? SelectAreaId { get; set; }

        /// <summary>
        /// 建筑名称
        /// </summary>
        public string BuildingName { get; set; }
    }
}