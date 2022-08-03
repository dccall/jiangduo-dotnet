using JiangDuo.Core.Base;
using JiangDuo.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.PublicSentimentService.Dto
{
    public class DtoPublicSentimentQuery : BaseRequest
    {
        /// <summary>
        /// 业务类型
        /// </summary>
        public long? BusinessId { get; set; }

        /// <summary>
        /// 居民Id
        /// </summary>
        public long? ResidentId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public PublicSentimentStatus? Status { get; set; }

        /// <summary>
        /// 所属选区Id
        /// </summary>
        public long? SelectAreaId { get; set; }
    }
}
