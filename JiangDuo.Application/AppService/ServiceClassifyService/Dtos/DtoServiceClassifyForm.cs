using JiangDuo.Core.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.ServiceClassifyService.Dto
{
    public class DtoServiceClassifyForm
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long? Id { get;  set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        [MaxLength(50)]
        public string ClassifyName { get; set; }
    }
}
