using Furion.DatabaseAccessor;
using JiangDuo.Core.Models;
using System.Collections.Generic;

namespace JiangDuo.Application.AppService.QrcodeService.Dto
{
    [Manual]
    public class DtoQrcode : Qrcode
    {
        /// <summary>
        /// 附件文件对象
        /// </summary>
        public List<SysUploadFile> AttachmentsFiles { get; set; } = new List<SysUploadFile>();
    }
}