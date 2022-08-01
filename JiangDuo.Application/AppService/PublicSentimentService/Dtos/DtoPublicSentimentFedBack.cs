using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.PublicSentimentService.Dtos
{
    public class DtoPublicSentimentFedBack
    {

        public long PublicSentimentId { get; set; }

        /// <summary>
        /// 反馈内容
        /// </summary>
        public string FeedbackContent { get; set; }
    }
}
