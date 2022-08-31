using Furion.DatabaseAccessor;
using JiangDuo.Core.Enums;
using JiangDuo.Core.Models;
using System.Collections.Generic;

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
        public List<SysUploadFile> CoverPhotoFiles { get; set; } = new List<SysUploadFile>();
    }
}