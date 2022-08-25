using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppletAppService.ResidentApplet.Dtos
{
    public class DtoSubscribeService
    {
        /// <summary>
        /// 服务id
        /// </summary>
        public long ServiceId { get; set; }
        /// <summary>
        /// 预约时间
        /// </summary>
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
