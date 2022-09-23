using Npoi.Mapper.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.QueryStatistics.Dtos
{
    public class DtoOfficialOrderCount
    {
        /// <summary>
        /// 日期
        /// </summary>
        [Column("日期")]
        public string Date { get; set; }
        /// <summary>
        /// 人大名称
        /// </summary>
        [Column("人大名称")]
        public string Name { get; set; }
        /// <summary>
        /// 完结数量
        /// </summary>
        [Column("本月完结数量")]
        public int OverOrderCount { get; set; }

        /// <summary>
        /// 上月完结数量
        /// </summary>
        [Column("上月完结数量")]
        public int LastMonthOrderCount { get; set; }


        /// <summary>
        /// 月增长（环比）
        /// </summary>
        [Column("月增长（环比）")]
        public string MonthGrowth
        {
            get
            {
                var number = LastMonthOrderCount == 0 ? 1 : LastMonthOrderCount;

                var temp = ((OverOrderCount - LastMonthOrderCount) / number * 100);
                return temp.ToString("f0") + "%";
            }
        }
    }
}
