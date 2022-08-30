using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.QueryStatistics.Dtos
{
    public class DtoReserveCount
    {
        /// <summary>
        /// 日期
        /// </summary>
        public string Date{ get; set; }
        /// <summary>
        /// 数量
        /// </summary>

        public int Count { get; set; }
    }
}
