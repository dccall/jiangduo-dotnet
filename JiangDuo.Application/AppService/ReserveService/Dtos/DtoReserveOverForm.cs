using JiangDuo.Core.Base;
using JiangDuo.Core.Enums;

namespace JiangDuo.Application.AppService.ReserveService.Dto
{
    public class DtoReserveOverForm 
    {
        public long ReserveId { get; set; }
        /// <summary>
        /// 会议结果
        /// </summary>
        public string MeetingResults { get; set; }
    }
}