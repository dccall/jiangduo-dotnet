using JiangDuo.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.OfficialsstructService.Dto
{
    public class DtoOfficialsstructQuery : BaseRequest
    {
        /// <summary>
        /// 人大结构名称
        /// </summary>
        public string Name { get; set; }
    }
}
