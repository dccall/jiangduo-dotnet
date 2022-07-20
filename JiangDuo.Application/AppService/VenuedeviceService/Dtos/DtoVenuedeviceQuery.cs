using JiangDuo.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.VenuedeviceService.Dto
{
    public class DtoVenuedeviceQuery : BaseRequest
    {
        /// <summary>
        /// 建筑名称
        /// </summary>
        public string VenuedeviceName { get; set; }
    }
}
