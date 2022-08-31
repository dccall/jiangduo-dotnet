using Furion.DatabaseAccessor;
using JiangDuo.Application.AppService.WorkorderService.Dto;
using JiangDuo.Core.Models;

namespace JiangDuo.Application.AppService.OnlineletterService.Dto
{
    [Manual]
    public class DtoOnlineletter : OnlineLetters
    {
        /// <summary>
        /// 关联的工单
        /// </summary>
        public DtoWorkOrder WorkOrder { get; set; }
    }
}