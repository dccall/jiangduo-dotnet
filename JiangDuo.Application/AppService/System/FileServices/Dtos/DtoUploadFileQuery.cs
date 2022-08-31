using Furion.DatabaseAccessor;
using JiangDuo.Core.Base;
using JiangDuo.Core.Enums;

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