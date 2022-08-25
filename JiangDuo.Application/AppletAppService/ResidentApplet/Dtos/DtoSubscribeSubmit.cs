using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppletAppService.ResidentApplet.Dtos
{
    public class DtoSubscribeSubmit
    {
        /// <summary>
        /// 服务id
        /// </summary>
        public long ServiceId { get; set; }

        /// <summary>
        /// 预约时间段
        /// </summary>
        public List<DtoSubscribeService> List { get; set; }
    }
}
