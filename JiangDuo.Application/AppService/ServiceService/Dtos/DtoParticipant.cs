using Furion.DatabaseAccessor;
using JiangDuo.Core.Models;

namespace JiangDuo.Application.AppService.ServiceService.Dtos
{
    [Manual]
    public class DtoParticipant : Participant
    {
        /// <summary>
        /// 居民名称
        /// </summary>
        public string ResidentName { get; set; }
    }
}