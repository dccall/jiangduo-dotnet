using JiangDuo.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.NewsclassifyService.Dto
{
    public class DtoNewsclassifyQuery : BaseRequest
    {
        /// <summary>
        /// 分类名称
        /// </summary>
        public string ClassifyName { get; set; } = null!;
    }
}
