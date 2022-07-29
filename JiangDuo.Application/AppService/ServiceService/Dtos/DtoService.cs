using JiangDuo.Core.Models;
using Furion.DatabaseAccessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JiangDuo.Core.Enums;
using JiangDuo.Application.AppService.WorkOrderService.Dto;
using JiangDuo.Application.AppService.WorkorderService.Dto;

namespace JiangDuo.Application.AppService.ServiceService.Dto
{
    [Manual]
    public class DtoService : Core.Models.Service
    {
        /// <summary>
        /// 状态
        /// </summary>
        public string StatusName => Status.GetDescription();

        /// <summary>
        /// 服务参加人员名单
        /// </summary>
        public List<Resident> JoinList { get; set; }
    }
}
