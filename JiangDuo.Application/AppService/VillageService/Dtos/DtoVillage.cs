using Furion.DatabaseAccessor;
using JiangDuo.Core.Models;

namespace JiangDuo.Application.AppService.VillageService.Dto
{
    [Manual]
    public class DtoVillage : Village
    {

        /// <summary>
        /// 人大代表数量
        /// </summary>
        public int OfficialCount { get; set; }
    }
}