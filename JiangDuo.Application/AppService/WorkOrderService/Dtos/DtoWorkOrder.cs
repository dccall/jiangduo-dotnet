using JiangDuo.Core.Models;
using Furion.DatabaseAccessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JiangDuo.Core.Enums;
using JiangDuo.Application.AppService.ReserveService.Dto;
using JiangDuo.Application.AppService.ServiceService.Dto;
using JiangDuo.Application.AppService.OnlineletterService.Dto;
using JiangDuo.Application.AppService.VolunteerService.Dto;

namespace JiangDuo.Application.AppService.WorkorderService.Dto
{
    [Manual]
    public class DtoWorkOrder : Workorder
    {
        /// <summary>
        /// 工单类型名称
        /// </summary>
        public string WorkorderTypeName => WorkorderType.GetDescription();
        /// <summary>
        /// 工单状态名称
        /// </summary>
        public string StatusName=> Status.GetDescription();

        /// <summary>
        /// 志愿者列表
        /// </summary>
        public List<DtoVolunteer> Volunteers { get; set; }

        /// <summary>
        /// 有事好商量
        /// </summary>
        public DtoReserveForm Reserve { get; set; }
        /// <summary>
        /// 一老一小(服务)
        /// </summary>
        public DtoServiceForm Service { get; set; }
        /// <summary>
        /// 码上说马上办
        /// </summary>
        public DtoOnlineletterForm OnlineLetters { get; set; }
    }
}
