using Furion.DynamicApiController;
using JiangDuo.Application.AppService.OfficialService.Dto;
using JiangDuo.Application.AppService.QueryStatistics.Dtos;
using JiangDuo.Application.AppService.QueryStatistics.Services;
using JiangDuo.Application.AppService.ReserveService.Dto;
using JiangDuo.Application.AppService.ServiceService.Dto;
using JiangDuo.Core.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.QueryStatistics
{
    /// <summary>
    /// 查询统计
    /// </summary>
    [Route("api/[controller]")]
    [ApiDescriptionSettings("Default", "查询统计")]
    public class HomeAppService : IDynamicApiController
    {
        private readonly IHomeService _queryStatisticsService;

        public HomeAppService(IHomeService queryStatisticsService)
        {
            _queryStatisticsService = queryStatisticsService;
        }

        /// <summary>
        /// 获取选区信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetSelectAreaInfoList")]
        public List<DtoSelectAreaInfo> GetSelectAreaInfoList()
        {
            return _queryStatisticsService.GetSelectAreaInfoList();
        }

        /// <summary>
        /// 获取基本数据总数统计
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetTotalCount")]
        public DtoTotal GetBaseTotalCount()
        {
            return _queryStatisticsService.GetBaseTotalCount();
        }

        /// <summary>
        /// 获取业务分类统计
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetBusiness")]
        public List<DtoBusinessStatistics> GetBusiness()
        {
            return _queryStatisticsService.GetBusiness();
        }

        /// <summary>
        /// 获取用户需求数量统计
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetPublicSentiment")]
        public DtoPublicSentimentStatisticsQuery GetPublicSentiment()
        {
            return _queryStatisticsService.GetPublicSentiment();
        }

        /// <summary>
        /// 获取用户需求数量统计(日)
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetPublicSentimentDay")]
        public List<DtoYearMonthDayStatistics> GetPublicSentimentDay([FromQuery] DtoYearMonthDayStatisticsQuery model)
        {
            return _queryStatisticsService.GetPublicSentimentDay(model);
        }

        /// <summary>
        /// 获取用户需求数量统计(月)
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetPublicSentimentMonth")]
        public List<DtoYearMonthDayStatistics> GetPublicSentimentMonth([FromQuery] DtoYearMonthDayStatisticsQuery model)
        {
            return _queryStatisticsService.GetPublicSentimentMonth(model);
        }

        /// <summary>
        /// 获取用户需求数量统计(年)
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetPublicSentimentYear")]
        public List<DtoYearMonthDayStatistics> GetPublicSentimentYear([FromQuery] DtoYearMonthDayStatisticsQuery model)
        {
            return _queryStatisticsService.GetPublicSentimentYear(model);
        }

        /// <summary>
        /// 获取服务数量统计
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetServiceTotal")]
        public DtoServiceTotal GetServiceTotal()
        {
            return _queryStatisticsService.GetServiceTotal();
        }

        /// <summary>
        /// 获取预约服务数量统计
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetReserveTotal")]
        public DtoReserveTotal GetReserveTotal()
        {
            return _queryStatisticsService.GetReserveTotal();
        }

        /// <summary>
        /// 获取有事好商量预约数量（30天）
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetReserveCount")]
        public List<DtoReserveCount> GetReserveCount()
        {
            return _queryStatisticsService.GetReserveCount();
        }

        /// <summary>
        /// 获取有事好商量预约场地列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetReserveVenuedevice")]
        public PagedList<DtoReserve> GetReserveVenuedevice([FromQuery] DtoReserveVenuedeviceQuery model)
        {
            return _queryStatisticsService.GetReserveVenuedevice(model);
        }

        /// <summary>
        /// 获取活动预约数量（30天）
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetServiceCount")]
        public List<DtoServiceCount> GetServiceCount()
        {
            return _queryStatisticsService.GetServiceCount();
        }

        /// <summary>
        /// 获取活动预约场地列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetServiceVenuedevice")]
        public PagedList<DtoService> GetServiceVenuedevice([FromQuery] DtoReserveVenuedeviceQuery model)
        {
            return _queryStatisticsService.GetServiceVenuedevice(model);
        }


        /// <summary>
        /// 人大完结工单排名
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("GetOfficialRanking")]
        public async Task<PagedList<DtoOfficial>> GetOfficialRanking([FromQuery] BaseRequest model)
        {
            return await _queryStatisticsService.OfficialRanking(model);
        }

        /// <summary>
        /// 人大每月工单数量
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("GetOfficialOrderCount")]
        public async Task<PagedList<DtoOfficialOrderCount>> OfficialOrderCount([FromQuery] DtoOfficialOrderCountQuery model)
        {
            return await _queryStatisticsService.OfficialOrderCount(model);
        }

        /// <summary>
        /// 导出人大每月工单数量
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("ExportOfficialOrderCount")]
        public async Task<IActionResult> ExportOfficialOrderCount([FromQuery] DtoOfficialOrderCountQuery model)
        {
            return await _queryStatisticsService.ExportOfficialOrderCount(model);
        }

        /// <summary>
        /// 工单类型 总数量
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        [HttpGet("GetOrderTypeCount")]
        public Task<List<DtoOrderTypeCount>> OrderTypeCount([FromQuery] DateTime? month)
        {
            return  _queryStatisticsService.OrderTypeCount(month);
        }

        /// <summary>
        /// 导出工单类型 总数量
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        [HttpGet("ExportOrderTypeCount")]
        public async Task<IActionResult> ExportOrderTypeCount([FromQuery] DateTime? month)
        {
            return await _queryStatisticsService.ExportOrderTypeCount(month);
        }
    }
}