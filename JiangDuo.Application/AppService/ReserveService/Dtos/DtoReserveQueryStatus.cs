using JiangDuo.Core.Base;
using JiangDuo.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.ReserveService.Dto
{
    public class DtoReserveQueryStatus : BaseRequest
    {

        public long ReserveId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public ReserveStatus Status { get; set; }
    }
}
