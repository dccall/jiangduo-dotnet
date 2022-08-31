using JiangDuo.Core.Enums;
using JiangDuo.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JiangDuo.Application.AppService.ServiceService.Dto
{
    public class DtoServiceForm
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long? Id { get; set; }

        /// <summary>
        /// 服务名称
        /// </summary>
        [MaxLength(100)]
        public string ServiceName { get; set; } = null!;

        /// <summary>
        /// 面向群体
        /// </summary>
        [MaxLength(50)]
        public string GroupOriented { get; set; }

        /// <summary>
        /// 场地设备Id
        /// </summary>
        public long? VenueDeviceId { get; set; }

        /// <summary>
        /// 服务分类
        /// </summary>
        public long? ServiceClassifyId { get; set; }

        /// <summary>
        /// 服务类型
        /// </summary>
        public ServiceTypeEnum ServiceType { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [MaxLength(300)]
        public string Remarks { get; set; }

        /// <summary>
        /// 计划人数
        /// </summary>
        public int? PlanNumber { get; set; }

        /// <summary>
        /// 计划开始时间
        /// </summary>
        public DateTime? PlanStartTime { get; set; }

        /// <summary>
        /// 计划结束时间
        /// </summary>
        public DateTime? PlanEndTime { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [MaxLength(300)]
        public string Address { get; set; }

        /// <summary>
        /// 选区id
        /// </summary>
        public long? SelectAreaId { get; set; }

        /// <summary>
        /// 村 范围
        /// </summary>
        public string VillagesRange { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public ServiceStatusEnum? Status { get; set; }

        /// <summary>
        /// 人大名单id
        /// </summary>
        public long? OfficialsId { get; set; }

        /// <summary>
        /// 附件
        /// </summary>
        public string Attachments { get; set; }

        /// <summary>
        /// 附件文件对象
        /// </summary>
        public List<SysUploadFile> AttachmentsFiles { get; set; }
    }
}