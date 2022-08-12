using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.QueryStatistics.Dtos
{
    public class DtoReserveTotal
    {
        /// <summary>
        /// 总数量
        /// </summary>
        public long TotalCount { get; set; }

        /// <summary>
        /// 审核中总数量
        /// </summary>
        public long AuditCount { get; set; }
        /// <summary>
        /// 审核通过总数量
        /// </summary>
        public long AuditedCount { get; set; }
        /// <summary>
        /// 审核未通过总数量
        /// </summary>
        public long AuditFailedCount { get; set; }
        /// <summary>
        /// 已完成总数量
        /// </summary>
        public long CompletedCount { get; set; }
    }
}
