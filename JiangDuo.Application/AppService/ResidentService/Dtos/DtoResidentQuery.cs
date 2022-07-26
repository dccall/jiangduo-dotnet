using JiangDuo.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.ResidentService.Dto
{
    public class DtoResidentQuery : BaseRequest
    {
        /// <summary>
        /// 所属村Id
        /// </summary>
        public long? VillageId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; } = null!;
    }
}
