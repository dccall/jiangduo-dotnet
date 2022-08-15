using JiangDuo.Core.Base;
using JiangDuo.Core.Enums;
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
        /// 状态
        /// </summary>
        public NewsStatus? Status { get; set; }
        /// <summary>
        /// 新闻标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 是否推荐
        /// </summary>
        public int? IsRecommend { get; set; }
    }
}
