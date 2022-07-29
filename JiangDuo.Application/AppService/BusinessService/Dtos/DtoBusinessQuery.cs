using JiangDuo.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.BusinessService.Dto
{
    public class DtoBusinessQuery : BaseRequest
    {
        /// <summary>
        /// 业务名称
        /// </summary>
        public string Name { get; set; }
    }
}
