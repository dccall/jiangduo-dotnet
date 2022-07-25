using JiangDuo.Core.Models;
using Furion.DatabaseAccessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.VenuedeviceService.Dto
{
    [Manual]
    public class DtoVenuedevice : Venuedevice
    {
        /// <summary>
        /// 所属建筑名称
        /// </summary>
        public string BuildingName{ get; set; }
    }
}
