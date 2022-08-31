using JiangDuo.Core.Base;

namespace JiangDuo.Application.AppService.OfficialsstructService.Dto
{
    public class DtoOfficialsstructQuery : BaseRequest
    {
        /// <summary>
        /// 人大结构名称
        /// </summary>
        public string Name { get; set; }
    }
}