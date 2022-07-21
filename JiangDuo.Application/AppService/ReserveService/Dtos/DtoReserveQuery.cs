using JiangDuo.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.ReserveService.Dto
{
    public class DtoReserveQuery : BaseRequest
    {
        /// <summary>
        /// 主题
        /// </summary>
        public string Theme { get; set; }
    }
}
