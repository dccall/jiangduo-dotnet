using JiangDuo.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.BuildingService.Dto
{
    public class DtoBuildingQuery : BaseRequest
    {
        /// <summary>
        /// 所属选区
        /// </summary>
        public long? SelectAreaId { get; set; }
        /// <summary>
        /// 建筑名称
        /// </summary>
        public string BuildingName { get; set; }
    }
}
