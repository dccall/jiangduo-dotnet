using JiangDuo.Core.Base;

namespace JiangDuo.Application.AppletAppService.ResidentApplet.Dtos
{
    public class DtoMyServiceQuery : BaseRequest
    {
        /// <summary>
        /// 未开始
        /// </summary>
        public bool NotStarted { get; set; }
    }
}