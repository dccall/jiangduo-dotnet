using JiangDuo.Core.Models;

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