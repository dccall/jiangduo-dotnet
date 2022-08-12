using JiangDuo.Application.AppService.QueryStatistics.Dtos;
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
        public DtoPublicSentimentStatistics GetPublicSentimentStatistics();


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
    }
}
