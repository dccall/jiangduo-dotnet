using JiangDuo.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppletAppService.OfficialApplet.Dtos
{
    public class DtoMyWorkOrderQuery:BaseRequest
    {

        /// <summary>
        /// 状态:1.待处理2.待审核3.已完成
        /// </summary>
        public int? Status { get; set; }

    }
}
