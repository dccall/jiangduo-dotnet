using JiangDuo.Core.Base;
using JiangDuo.Core.Enums;
using JiangDuo.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.NewsService.Dto
{
    public class DtoNewsUpdateStatus
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get;  set; }
        /// <summary>
        /// 状态
        /// </summary>
        public NewsStatus Status { get; set; }
    }
}
