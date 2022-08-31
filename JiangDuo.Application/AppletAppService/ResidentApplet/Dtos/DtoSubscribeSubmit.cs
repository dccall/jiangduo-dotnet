using System.Collections.Generic;

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