using JiangDuo.Core.Enums;
using Npoi.Mapper.Attributes;
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
        /// 日期
        /// </summary>
        [Column("日期")]
        public string Date { get; set; }
        /// <summary>
        /// 工单类型
        /// </summary>
        [Ignore]
        public WorkorderTypeEnum Type { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>
        [Column("类型")]
        public string TypeName =>Type.GetDescription();

        /// <summary>
        /// 数量
        /// </summary>
        [Column("数量")]
        public int OverOrderCount { get; set; }
    }
}
