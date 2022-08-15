using Furion.DynamicApiController;
using JiangDuo.Application.AppService.QueryStatistics.Dtos;
using JiangDuo.Application.AppService.QueryStatistics.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.QueryStatistics
{
    /// <summary>
    /// 查询统计
    /// </summary>
    [Route("api/[controller]")]
    [ApiDescriptionSettings("Default", "查询统计")]
    public class QueryStatisticsAppService : IDynamicApiController
    {
        private readonly IQueryStatisticsService _queryStatisticsService;
        public QueryStatisticsAppService(IQueryStatisticsService queryStatisticsService)
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
        [HttpGet("GetBusinessStatistics")]
        public List<DtoBusinessStatistics> GetBusinessStatistics()
        {
            return _queryStatisticsService.GetBusinessStatistics();
        }
        /// <summary>
        /// 获取用户需求数量统计
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetPublicSentimentStatistics")]
        public DtoPublicSentimentStatistics GetPublicSentimentStatistics()
        {
            return _queryStatisticsService.GetPublicSentimentStatistics();
        }
        /// <summary>
        /// 获取用户需求数量统计(日)
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetPublicSentimentDayStatistics")]
        public List<DtoYearMonthDayStatistics> GetPublicSentimentDayStatistics([FromQuery] DtoYearMonthDayStatisticsQuery model)
        {
            return _queryStatisticsService.GetPublicSentimentDayStatistics(model);
        }
        /// <summary>
        /// 获取用户需求数量统计(月)
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetPublicSentimentMonthStatistics")]
        public List<DtoYearMonthDayStatistics> GetPublicSentimentMonthStatistics([FromQuery] DtoYearMonthDayStatisticsQuery model)
        {
            return _queryStatisticsService.GetPublicSentimentMonthStatistics(model);
        }
        /// <summary>
        /// 获取用户需求数量统计(年)
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetPublicSentimentYearStatistics")]
        public List<DtoYearMonthDayStatistics> GetPublicSentimentYearStatistics([FromQuery] DtoYearMonthDayStatisticsQuery model)
        {
            return _queryStatisticsService.GetPublicSentimentYearStatistics(model);
        }

        /// <summary>
        /// 获取服务数量统计
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetServiceTotalStatistics")]
        public DtoServiceTotal GetServiceTotalStatistics()
        {
            return _queryStatisticsService.GetServiceTotalStatistics();
        }

        /// <summary>
        /// 获取预约服务数量统计
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetReserveTotalStatistics")]
        public DtoReserveTotal GetReserveTotalStatistics()
        {
            return _queryStatisticsService.GetReserveTotalStatistics();
        }
    }
}
