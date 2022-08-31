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