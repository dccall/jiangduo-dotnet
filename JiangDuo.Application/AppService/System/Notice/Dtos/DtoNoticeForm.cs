using JiangDuo.Core.Base;
using JiangDuo.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.System.Notice.Dtos
{
    public class DtoNoticeForm
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long? Id { get;  set; }
        /// <summary>
        /// 公告标题
        /// </summary>
        [MaxLength(50)]
        public string NoticeTitle { get; set; } = null!;
        /// <summary>
        /// 公告类型
        /// </summary>
        public NoticeType Type { get; set; }
        /// <summary>
        /// 公告内容
        /// </summary>
        public string NoticeContent { get; set; }
        /// <summary>
        /// 公告状态
        /// </summary>
        public NoticeStatus Status { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        public string Remark { get; set; }
    }
}
