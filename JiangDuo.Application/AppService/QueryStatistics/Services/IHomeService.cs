using JiangDuo.Application.AppService.OfficialService.Dto;
using JiangDuo.Application.AppService.QueryStatistics.Dtos;
using JiangDuo.Application.AppService.ReserveService.Dto;
using JiangDuo.Application.AppService.ServiceService.Dto;
using JiangDuo.Core.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.QueryStatistics.Services
{
    public interface IHomeService
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
        public List<DtoBusinessStatistics> GetBusiness();

        /// <summary>
        /// 获取用户需求数量统计
        /// </summary>
        /// <returns></returns>
        public DtoPublicSentimentStatisticsQuery GetPublicSentiment();

        /// <summary>
        /// 获取用户需求数量统计(日)
        /// </summary>
        /// <returns></returns>
        public List<DtoYearMonthDayStatistics> GetPublicSentimentDay(DtoYearMonthDayStatisticsQuery model);

        /// <summary>
        /// 获取用户需求数量统计(日)
        /// </summary>
        /// <returns></returns>
        public List<DtoYearMonthDayStatistics> GetPublicSentimentMonth(DtoYearMonthDayStatisticsQuery model);

        /// <summary>
        /// 获取用户需求数量统计(日)
        /// </summary>
        /// <returns></returns>
        public List<DtoYearMonthDayStatistics> GetPublicSentimentYear(DtoYearMonthDayStatisticsQuery model);

        /// <summary>
        /// 获取服务数量统计
        /// </summary>
        /// <returns></returns>
        public DtoServiceTotal GetServiceTotal();

        /// <summary>
        /// 获取预约服务数量统计
        /// </summary>
        /// <returns></returns>
        public DtoReserveTotal GetReserveTotal();

        /// <summary>
        /// 获取有事好商量预约数量（30天）
        /// </summary>
        /// <returns></returns>
        public List<DtoReserveCount> GetReserveCount();

        /// <summary>
        /// 获取有事好商量预约场地列表
        /// </summary>
        /// <returns></returns>
        public PagedList<DtoReserve> GetReserveVenuedevice(DtoReserveVenuedeviceQuery model);

        /// <summary>
        /// 获取活动预约数量（30天）
        /// </summary>
        /// <returns></returns>
        public List<DtoServiceCount> GetServiceCount();

        /// <summary>
        /// 获取活动预约场地列表
        /// </summary>
        /// <returns></returns>
        public PagedList<DtoService> GetServiceVenuedevice(DtoReserveVenuedeviceQuery model);


        /// <summary>
        /// 人大完结工单排名
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public  Task<PagedList<DtoOfficial>> OfficialRanking(BaseRequest model);


        /// <summary>
        /// 人大每月工单数量
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<PagedList<DtoOfficialOrderCount>> OfficialOrderCount(DtoOfficialOrderCountQuery model);

        /// <summary>
        /// 人大每月工单数量
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<IActionResult> ExportOfficialOrderCount(DtoOfficialOrderCountQuery model);


        /// <summary>
        /// 工单类型 总数量
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public Task<List<DtoOrderTypeCount>> OrderTypeCount(DateTime? month);

        /// <summary>
        /// 导出工单类型 总数量
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public Task<IActionResult> ExportOrderTypeCount(DateTime? month);
    }
}