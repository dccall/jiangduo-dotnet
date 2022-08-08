using JiangDuo.Core.Models;
using Furion.DatabaseAccessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.NewsService.Dto
{
    [Manual]
    public class DtoNews : News
    {
        /// <summary>
        /// 文件对象
        /// </summary>
        public List<SysUploadFile> CoverPhotoFiles { get; set; }
    }
}
