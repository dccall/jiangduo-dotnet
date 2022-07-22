using JiangDuo.Core.Models;
using Furion.DatabaseAccessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JiangDuo.Application.AppService.WorkorderService.Dto;

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
