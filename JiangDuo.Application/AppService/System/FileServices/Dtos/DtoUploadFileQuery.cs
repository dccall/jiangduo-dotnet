using Furion.DatabaseAccessor;
using JiangDuo.Core.Models;
using JiangDuo.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JiangDuo.Core.Base;

namespace JiangDuo.Application.AppService.System.FileServices.Dtos
{
    [Manual]
    public class DtoUploadFileQuery : BaseRequest
    {
        /// <summary>
        /// 来源
        /// </summary>
        public UploadFileSource? FileSource;
    }
}
