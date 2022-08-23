using JiangDuo.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.QrcodeService.Dto
{
    public class DtoQrcodeQuery : BaseRequest
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}
