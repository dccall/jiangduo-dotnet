using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.QueryStatistics.Dtos
{
    public class DtoServiceTotal
    {
        /// <summary>
        /// 总数量
        /// </summary>
        public long TotalCount { get; set; }
        /// <summary>
        /// 待审核数量
        /// </summary>
        public long AuditCount { get; set; }
        /// <summary>
        /// 审核未通过数量
        /// </summary>
        public long AuditFailedCount { get; set; }
        /// <summary>
        /// 审核通过数量
        /// </summary>
        public long AuditedCount { get; set; }
        /// <summary>
        /// 已发布数量
        /// </summary>
        public long PublishedCount { get; set; }
        /// <summary>
        /// 已结束数量
        /// </summary>
        public long EndCount { get; set; }

       

    }
}
