using Furion.DatabaseAccessor;
using JiangDuo.Application.AppService.VenuedeviceService.Dto;
using JiangDuo.Application.AppService.VolunteerService.Dto;
using JiangDuo.Core.Enums;
using JiangDuo.Core.Models;
using System.Collections.Generic;

namespace JiangDuo.Application.AppService.ReserveService.Dto
{
    [Manual]
    public class DtoReserve : Reserve
    {
        /// <summary>
        /// 状态名称
        /// </summary>
        public string StatusName => Status.GetDescription();

        /// <summary>
        /// 场地/设备名称
        /// </summary>
        public string VenueDeviceName { get; set; }

        /// <summary>
        /// 志愿者列表
        /// </summary>
        public List<DtoVolunteer> VolunteerList { get; set; }

        /// <summary>
        /// 场地/设备
        /// </summary>
        public DtoVenuedevice Venuedevice { get; set; }
    }
}