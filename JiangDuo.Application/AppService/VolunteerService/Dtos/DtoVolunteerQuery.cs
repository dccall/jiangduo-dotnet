using JiangDuo.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.VolunteerService.Dto
{
    public class DtoVolunteerQuery : BaseRequest
    {
        /// <summary>
        /// 所属选区Id
        /// </summary>
        public long? SelectAreaId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
    }
}
