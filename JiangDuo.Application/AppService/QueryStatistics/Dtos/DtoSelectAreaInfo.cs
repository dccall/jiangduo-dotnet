using JiangDuo.Application.AppService.SelectAreaService.Dto;
using JiangDuo.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.QueryStatistics.Dtos
{
    public class DtoSelectAreaInfo
    {
        /// <summary>
        /// 选区信息
        /// </summary>
        public SelectArea SelectArea { get; set; }
        /// <summary>
        /// 村总数量
        /// </summary>
        public int VillageCount { get; set; }
        /// <summary>
        /// 人大数量
        /// </summary>
        public int OfficialCount { get; set; }
        /// <summary>
        /// 居民数量
        /// </summary>
        public int ResidentCount { get; set; }
    }
}
