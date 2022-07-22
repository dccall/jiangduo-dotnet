using JiangDuo.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.System.FileFileServices.Dtos
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
