using JiangDuo.Application.AppService.OnlineletterService.Dto;
using JiangDuo.Application.AppService.ReserveService.Dto;
using JiangDuo.Application.AppService.ServiceService.Dto;
using JiangDuo.Core.Base;
using JiangDuo.Core.Enums;
using JiangDuo.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.WorkOrderService.Dto
{
    public class DtoWorkOrderForm 
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long? Id { get;  set; }
        /// <summary>
        /// 工单编号
        /// </summary>
        [MaxLength(50)]
        public string WorkOrderNo { get; set; }
        /// <summary>
        /// 发起人
        /// </summary>
        public long OriginatorId { get; set; }
        /// <summary>
        /// 发起人名称
        /// </summary>
        public string OriginatorName { get; set; }
        /// <summary>
        /// 接收人
        /// </summary>
        public long? RecipientId { get; set; }
        /// <summary>
        /// 接收人名称
        /// </summary>
        public string RecipientName { get; set; }
        /// <summary>
        /// 业务类型
        /// </summary>
        public long BusinessId { get; set; }
        /// <summary>
        /// 工单类型
        /// </summary>
        public WorkorderTypeEnum WorkorderType { get; set; } = WorkorderTypeEnum.Normal;
        /// <summary>
        /// 工单来源
        /// </summary>
        public WorkorderSourceEnum WorkorderSource { get; set; } = WorkorderSourceEnum.System;
        /// <summary>
        /// 工单状态
        /// </summary>
        public WorkorderStatusEnum Status { get; set; } = WorkorderStatusEnum.NotProcessed;
        /// <summary>
        /// 工单内容
        /// </summary>
        [MaxLength(300)]
        public string Content { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 完结时间
        /// </summary>
        public DateTime? OverTime { get; set; }
        /// <summary>
        /// 工单分数
        /// </summary>
        public double? Score { get; set; }= 1;
        /// <summary>
        /// 选区id
        /// </summary>
        public long? SelectAreaId { get; set; }
        /// <summary>
        /// 引用公共需求Id
        /// </summary>
        public long? PublicSentimentId { get; set; }



        /// <summary>
        /// 附件列表
        /// </summary>
        public List<SysUploadFile> AttachmentsList { get; set; } = new List<SysUploadFile>();
    }
}
