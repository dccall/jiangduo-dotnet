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
        /// 所属选区Id
        /// </summary>
        public long? SelectAreaId { get; set; }
        /// <summary>
        /// 人大结构Id
        /// </summary>
        public long? OfficialsstructId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; } = null!;
    }
}
