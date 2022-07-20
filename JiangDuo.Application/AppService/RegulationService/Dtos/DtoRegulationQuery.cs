using JiangDuo.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.RegulationService.Dto
{
    public class DtoRegulationQuery : BaseRequest
    {
        /// <summary>
        /// 规则制度名称
        /// </summary>
        public string Name { get; set; }
    }
}
