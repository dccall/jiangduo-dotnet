using JiangDuo.Core.Base;
using JiangDuo.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.ReserveService.Dto
{
    public class DtoReserveQuery : BaseRequest
    {
        /// <summary>
        /// 主题
        /// </summary>
        public string Theme { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public ReserveStatus? Status { get; set; }

        /// <summary>
        /// 所属区域
        /// </summary>
        public long? SelectAreaId { get; set; }
        /// <summary>
        /// 创建人id
        /// </summary>
        public long? Creator { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
    }
}
