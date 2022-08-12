using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.QueryStatistics.Dtos
{
    public class DtoTotal
    {
        /// <summary>
        /// 选区总数量
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 区选区总数量
        /// </summary>
        public int AreaTotalCount { get; set; }
        /// <summary>
        /// 镇选区总数量
        /// </summary>
        public int TownTotalCount { get; set; }
        /// <summary>
        /// 村总数量
        /// </summary>
        public int VillageTotalCount { get; set; }
        /// <summary>
        /// 居民总数量
        /// </summary>
        public int ResidentTotalCount { get; set; }
        

        /// <summary>
        /// 人大总数量
        /// </summary>
        public int OfficialTotalCount { get; set; }
        /// <summary>
        /// 区人大数量
        /// </summary>
        public int AreaOfficialCount { get; set; }
        /// <summary>
        /// 镇人大数量
        /// </summary>
        public int TownOfficialCount { get; set; }





    }
}
