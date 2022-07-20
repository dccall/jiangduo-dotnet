using JiangDuo.Core.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.NewsclassifyService.Dto
{
    public class DtoNewsclassifyForm 
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long? Id { get;  set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        [MaxLength(50)]
        public string ClassifyName { get; set; } = null!;
    }
}
