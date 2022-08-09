﻿using JiangDuo.Core.Models;
using Furion.DatabaseAccessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JiangDuo.Core.Enums;
using JiangDuo.Application.AppService.WorkOrderService.Dto;
using JiangDuo.Application.AppService.WorkorderService.Dto;
using JiangDuo.Application.AppService.ServiceService.Dtos;
using JiangDuo.Application.AppService.VenuedeviceService.Dto;

namespace JiangDuo.Application.AppService.ServiceService.Dto
{
    [Manual]
    public class DtoService : Service
    {
        /// <summary>
        /// 状态
        /// </summary>
        public string StatusName => Status.GetDescription();

        /// <summary>
        /// 服务类型名称
        /// </summary>
        public string ServiceTypeName => ServiceType.GetDescription();

        /// <summary>
        /// 场地名称
        /// </summary>
        public string VenueDeviceName { get; set; }

        /// <summary>
        /// 人大人员名称
        /// </summary>
        public string OfficialsName { get; set; }

        /// <summary>
        /// 服务参加人员名单
        /// </summary>
        public List<DtoJoinServiceResident> JoinServiceResident { get; set; }

        /// <summary>
        /// 附件文件对象
        /// </summary>
        public List<SysUploadFile> AttachmentsFiles { get; set; }

        /// <summary>
        /// 场地/设备
        /// </summary>
        public DtoVenuedevice Venuedevice { get; set; }
    }
}
