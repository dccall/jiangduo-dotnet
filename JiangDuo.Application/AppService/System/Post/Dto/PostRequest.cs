using JiangDuo.Core.Base;

namespace JiangDuo.Application.System.Post.Dto
{
    public class PostRequest : BaseRequest
    {
        /// <summary>
        ///  职位名称
        /// </summary>
        public string PostName { get; set; }
    }
}