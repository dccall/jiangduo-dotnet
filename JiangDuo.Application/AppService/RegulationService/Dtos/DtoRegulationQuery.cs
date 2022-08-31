using JiangDuo.Core.Base;

namespace JiangDuo.Application.AppService.RegulationService.Dto
{
    public class DtoRegulationQuery : BaseRequest
    {
        /// <summary>
        /// 规则制度名称
        /// </summary>
        public string Name { get; set; }
    }
}