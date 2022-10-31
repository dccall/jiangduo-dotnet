using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppletAppService.ResidentApplet.Dtos
{
    public class DtoGetParticipant
    {
        /// <summary>
        /// 服务id
        /// </summary>

        public long ServiceId { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public DateTime Date { get; set; }= DateTime.Now;
    }
}
