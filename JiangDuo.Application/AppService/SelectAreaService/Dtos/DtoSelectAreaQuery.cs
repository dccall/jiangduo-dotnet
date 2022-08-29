using JiangDuo.Core.Base;
using JiangDuo.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.SelectAreaService.Dto
{
    public class DtoSelectAreaQuery : BaseRequest
    {
        /// <summary>
        /// 选区名称
        /// </summary>
        public string SelectAreaName { get; set; }

        /// <summary>
        /// 区域类型
        /// </summary>
        public SelectAreaTypeEnum? SelectAreaType { get; set; }
    }
}
