﻿using JiangDuo.Core.Models;
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
        /// 工单来源名称
        /// </summary>
        public string WorkorderSourceName => WorkorderSource.GetDescription();

        /// <summary>
        /// 工单状态名称
        /// </summary>
        public string StatusName=> Status.GetDescription();

        /// <summary>
        /// 所属选区
        /// </summary>
        public string SelectAreaName { get; set; }

        /// <summary>
        /// 工单日志
        /// </summary>
        public List<Workorderlog> Workorderlogs { get; set; }
        /// <summary>
        /// 工单反馈
        /// </summary>
        public List<Workorderfeedback> WorkorderfeedbackList { get; set; }

        /// <summary>
        /// 是否为协助工单
        /// </summary>
        public bool IsHelper { get; set; }

        /// <summary>
        /// 附件列表
        /// </summary>
        public List<SysUploadFile> AttachmentsList { get; set; } = new List<SysUploadFile>();
    }
}
