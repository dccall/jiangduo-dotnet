using JiangDuo.Core.Models;
using Furion.DatabaseAccessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JiangDuo.Application.AppService.WorkorderService.Dto;
using JiangDuo.Core.Enums;
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
    }
}
