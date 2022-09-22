using JiangDuo.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static JiangDuo.Application.AppService.QueryStatistics.Services.HomeService;

namespace JiangDuo.Application.AppService.QueryStatistics.Dtos
{
    public class DtoOrderTypeCount
    {
        /// <summary>
        /// 工单类型
        /// </summary>
        public WorkorderTypeEnum Type { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName =>Type.GetDescription();

        /// <summary>
        /// 数量
        /// </summary>
        public int OverOrderCount { get; set; }
    }
}
