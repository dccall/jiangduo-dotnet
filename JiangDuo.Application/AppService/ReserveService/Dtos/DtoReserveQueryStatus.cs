using JiangDuo.Core.Base;
using JiangDuo.Core.Enums;

namespace JiangDuo.Application.AppService.ReserveService.Dto
{
    public class DtoReserveQueryStatus : BaseRequest
    {
        public long ReserveId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public ReserveStatus Status { get; set; }
    }
}