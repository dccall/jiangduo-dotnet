using JiangDuo.Core.Enums;

namespace JiangDuo.Application.System.FileServices.Dtos
{
    /// <summary>
    /// 上传请求参数
    /// </summary>
    public class UploadRequest
    {
        /// <summary>
        /// 文件来源
        /// </summary>
        public UploadFileSource FileSource { get; set; } = UploadFileSource.Null;
    }
}