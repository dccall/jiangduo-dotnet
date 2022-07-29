using JiangDuo.Core.Base;
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
    }
}
