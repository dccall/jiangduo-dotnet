using JiangDuo.Core.Base;

namespace JiangDuo.Application.AppService.RegionService.Dto
{
    public class DtoRegionQuery : BaseRequest
    {
        /// <summary>
        /// 地区父id
        /// </summary>
        public long? RegionParentId { get; set; }

        /// <summary>
        /// 地区级别 1-省、自治区、直辖市 2-地级市、地区、自治州、盟 3-市辖区、县级市、县
        /// </summary>
        public int? RegionLevel { get; set; }
    }
}