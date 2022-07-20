using JiangDuo.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.NewsService.Dto
{
    public class DtoNewsQuery : BaseRequest
    {
        /// <summary>
        /// 新闻标题
        /// </summary>
        public string Title { get; set; }
    }
}
