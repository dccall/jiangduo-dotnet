using Furion.DatabaseAccessor;
using JiangDuo.Core.Enums;
using JiangDuo.Core.Models;

namespace JiangDuo.Application.AppService.System.FileServices.Dtos
{
    [Manual]
    public class DtoUploadFile : SysUploadFile
    {
        /// <summary>
        /// 来源
        /// </summary>
        public string FileSourceName => FileSource.GetDescription();
    }
}