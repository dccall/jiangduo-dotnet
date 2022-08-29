using JiangDuo.Application.AppService.QueryStatistics.Dtos;
using JiangDuo.Application.AppService.ReserveService.Dto;
using JiangDuo.Application.AppService.ServiceService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.QueryStatistics.Services
{
    public interface IQueryStatisticsService
    {
        /// <summary>
        /// 获取选区信息
        /// </summary>
        /// <returns></returns>
        public List<DtoSelectAreaInfo> GetSelectAreaInfoList();
        /// <summary>
        /// 获取基本数据总数统计
        /// </summary>
        /// <returns></returns>
        public DtoTotal GetBaseTotalCount();
        /// <summary>
        /// 获取用户需求业务分类统计
        /// </summary>
        /// <returns></returns>
        public List<DtoBusinessStatistics> GetBusinessStatistics();

        /// <summary>
        /// 获取用户需求数量统计
        /// </summary>
        /// <returns></returns>
        public DtoPublicSentimentStatisticsQuery GetPublicSentimentStatistics();
        /// <summary>
        /// 获取用户需求数量统计(日)
        /// </summary>
        /// <returns></returns>
        public List<DtoYearMonthDayStatistics> GetPublicSentimentDayStatistics(DtoYearMonthDayStatisticsQuery model);
        /// <summary>
        /// 获取用户需求数量统计(日)
        /// </summary>
        /// <returns></returns>
        public List<DtoYearMonthDayStatistics> GetPublicSentimentMonthStatistics(DtoYearMonthDayStatisticsQuery model);
        /// <summary>
        /// 获取用户需求数量统计(日)
        /// </summary>
        /// <returns></returns>
        public List<DtoYearMonthDayStatistics> GetPublicSentimentYearStatistics(DtoYearMonthDayStatisticsQuery model);
        /// <summary>
        /// 获取服务数量统计
        /// </summary>
        /// <returns></returns>
        public DtoServiceTotal GetServiceTotalStatistics();

        /// <summary>
        /// 获取预约服务数量统计
        /// </summary>
        /// <returns></returns>
        public DtoReserveTotal GetReserveTotalStatistics();

        /// <summary>
        /// 获取有事好商量预约场地列表
        /// </summary>
        /// <returns></returns>
        public PagedList<DtoReserve> GetReserveVenuedevice(DtoReserveVenuedeviceQuery model);

        /// <summary>
        /// 获取活动预约场地列表
        /// </summary>
        /// <returns></returns>
        public PagedList<DtoService> GetServiceVenuedevice(DtoReserveVenuedeviceQuery model);
    }
}
