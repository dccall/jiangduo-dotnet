using JiangDuo.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.ServiceService.Dtos
{
    public class DtoJoinServiceResident
    {
        /// <summary>
        /// 居民
        /// </summary>
        public Resident Resident { get; set; }

        public DateTime? RegistTime { get; set; }

        /// <summary>
        /// 预约开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 预约结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
    }
}
