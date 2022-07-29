using JiangDuo.Core.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.BusinessService.Dto
{
    public class DtoBusinessForm
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long? Id { get;  set; }
        /// <summary>
        /// 业务名称
        /// </summary>
        public string Name { get; set; }
    }
}
