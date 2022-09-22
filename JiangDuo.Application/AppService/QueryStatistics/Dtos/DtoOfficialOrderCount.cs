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
        public string Date { get; set; }
        /// <summary>
        /// 人大名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 完结数量
        /// </summary>
        public int OverOrderCount { get; set; }
    }
}
