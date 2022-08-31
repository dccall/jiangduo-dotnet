using JiangDuo.Core.Base;

namespace JiangDuo.Application.AppService.VenuedeviceService.Dto
{
    public class DtoVenuedeviceQuery : BaseRequest
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string VenuedeviceName { get; set; }

        /// <summary>
        /// 所属选区
        /// </summary>
        public long? SelectAreaId { get; set; }
    }
}