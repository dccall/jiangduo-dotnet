using Furion.DatabaseAccessor;
using JiangDuo.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.ServiceService.Dtos
{
    [Manual]
    public class DtoParticipant: Participant
    {
        /// <summary>
        /// 居民名称
        /// </summary>
        public string ResidentName { get; set; }
    }
}
