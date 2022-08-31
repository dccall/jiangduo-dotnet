using JiangDuo.Core.Base;

namespace JiangDuo.Application.AppService.QrcodeService.Dto
{
    public class DtoQrcodeQuery : BaseRequest
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}