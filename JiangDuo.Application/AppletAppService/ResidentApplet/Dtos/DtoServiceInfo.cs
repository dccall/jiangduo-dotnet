using Furion.DatabaseAccessor;
using JiangDuo.Application.AppService.ServiceService.Dtos;
using JiangDuo.Core.Base;
using JiangDuo.Core.Enums;
using JiangDuo.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppletAppService.ResidentApplet.Dtos
{

    [Manual]
    public class DtoServiceInfo : Core.Models.Service
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
        /// 是否报名
        /// </summary>
        public bool IsSignUp { get; set; } = false;
        /// <summary>
        /// 报名时间
        /// </summary>
        public DateTime? RegistTime { get; set; }
        /// <summary>
        /// 预约开始时间
        /// </summary>

        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 预约结束时间
        /// </summary>

        public DateTime? EndTime { get; set; }
    }
}
