using JiangDuo.Core.Base;

namespace JiangDuo.Application.AppService.BusinessService.Dto
{
    public class DtoBusinessQuery : BaseRequest
    {
        /// <summary>
        /// 业务名称
        /// </summary>
        public string Name { get; set; }
    }
}