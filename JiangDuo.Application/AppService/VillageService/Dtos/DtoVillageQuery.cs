using JiangDuo.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.VillageService.Dto
{
    public class DtoVillageQuery : BaseRequest
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}
