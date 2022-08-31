using JiangDuo.Core.Enums;

namespace JiangDuo.Application.AppService.NewsService.Dto
{
    public class DtoNewsUpdateStatus
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public NewsStatus Status { get; set; }
    }
}