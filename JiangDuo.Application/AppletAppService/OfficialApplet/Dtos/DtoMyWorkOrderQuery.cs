using JiangDuo.Core.Base;
using JiangDuo.Core.Enums;

namespace JiangDuo.Application.AppletAppService.OfficialApplet.Dtos
{
    public class DtoMyWorkOrderQuery : BaseRequest
    {
        /// <summary>
        /// 工单状态
        /// </summary>
        public WorkorderStatusEnum? Status { get; set; }
    }
}