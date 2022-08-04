using JiangDuo.Application.AppService.VolunteerService.Dto;
using JiangDuo.Core.Base;
using JiangDuo.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.ReserveService.Dto
{
    public class DtoReserveForm
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long? Id { get;  set; }
        /// <summary>
        /// 主题
        /// </summary>
        [MaxLength(50)]
        public string Theme { get; set; } = null!;
        /// <summary>
        /// 场地设备
        /// </summary>
        public long VenueDeviceId { get; set; }
        /// <summary>
        /// 人数
        /// </summary>
        public int? Number { get; set; }
        /// <summary>
        /// 预约日期
        /// </summary>
        public DateTime ReserveDate { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [MaxLength(255)]
        public string Remarks { get; set; }
        /// <summary>
        /// 会议结果
        /// </summary>
        [MaxLength(255)]
        public string MeetingResults { get; set; }
        /// <summary>
        /// 所属区域
        /// </summary>
        public long? SelectAreaId { get; set; }

        /// <summary>
        /// 关联工单Id
        /// </summary>
        public long? WorkOrderId { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public ReserveStatus Status { get; set; }
        /// <summary>
        /// 志愿者列表
        /// </summary>
        public List<DtoVolunteer> VolunteerList { get; set; }
    }
}
