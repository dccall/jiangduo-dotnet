using JiangDuo.Core.Base;

namespace JiangDuo.Application.AppService.NewsclassifyService.Dto
{
    public class DtoNewsclassifyQuery : BaseRequest
    {
        /// <summary>
        /// 分类名称
        /// </summary>
        public string ClassifyName { get; set; } = null!;
    }
}