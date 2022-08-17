using Furion.DatabaseAccessor;
using JiangDuo.Core.Models;
using JiangDuo.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.System.FileServices.Dtos
{
    [Manual]
    public class DtoUploadFile:SysUploadFile
    {
        /// <summary>
        /// 来源
        /// </summary>
        public string FileSourceName => FileSource.GetDescription();
    }
}
