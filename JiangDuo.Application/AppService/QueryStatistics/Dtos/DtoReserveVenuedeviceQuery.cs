using JiangDuo.Core.Base;
using System;

namespace JiangDuo.Application.AppService.QueryStatistics.Dtos;

public class DtoReserveVenuedeviceQuery : BaseRequest
{
    /// <summary>
    /// 预约日期
    /// </summary>
    public DateTime? ReserveDate { get; set; }
}