using JiangDuo.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.OfficialService.Dto
{
    public class DtoOfficialQuery : BaseRequest
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; } = null!;
    }
}
