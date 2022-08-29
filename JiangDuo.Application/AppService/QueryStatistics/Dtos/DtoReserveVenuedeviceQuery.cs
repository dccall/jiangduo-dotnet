using JiangDuo.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.QueryStatistics.Dtos;
public class DtoReserveVenuedeviceQuery : BaseRequest
{
    /// <summary>
    /// 预约日期
    /// </summary>
    public DateTime? ReserveDate { get; set; }
}
