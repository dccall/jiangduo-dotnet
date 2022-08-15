using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.QueryStatistics.Dtos
{
    public class DtoPublicSentimentStatistics
    {
        /// <summary>
        /// 总数量
        /// </summary>
        public long TotalCount { get; set; }
        /// <summary>
        /// 待处理数量
        /// </summary>
        public long NotProcessedCount { get; set; }
        /// <summary>
        /// 进行中数量
        /// </summary>
        public long InProgressCount { get; set; }
        /// <summary>
        /// 已完结数量
        /// </summary>
        public long FeedbackCount { get; set; }

    }

    public class DtoBusinessStatistics
    {
        /// <summary>
        /// 业务分类名称
        /// </summary>
        public string BusinessName { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 百分比
        /// </summary>

        public double Percentage { get; set; }
        /// <summary>
        /// 百分比
        /// </summary>
        public string PercentageStr
        {
            get
            {
                return Percentage.ToString("0.00")+"%";
            }
        }
    }


    public class DtoYearMonthDayStatisticsQuery
    {
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }

    public class DtoYearMonthDayStatistics
    {
        /// <summary>
        /// 日期
        /// </summary>

        public string Date { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }
    }
}
