using JiangDuo.Core.Models;
using Furion.DatabaseAccessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JiangDuo.Core.Enums;
namespace JiangDuo.Application.AppService.NewsService.Dto
{
    [Manual]
    public class DtoNews : News
    {
        /// <summary>
        /// 状态名称
        /// </summary>
        public string StatusName => Status.GetDescription();

        /// <summary>
        /// 分类名称
        /// </summary>
        public string NewsClassifyName { get; set; }
        /// <summary>
        /// 文件对象
        /// </summary>
        public List<SysUploadFile> CoverPhotoFiles { get; set; }
    }
}
