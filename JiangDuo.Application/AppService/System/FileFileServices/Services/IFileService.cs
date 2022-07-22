using JiangDuo.Core.Enums;
using JiangDuo.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.System.FileFileServices.Services
{
    public interface IFileService
    {

        public Task<SysUploadFile> UploadFileAsync(IFormFile file, UploadFileSource fileSource = UploadFileSource.Null);
        public IActionResult FileDownload(long fileId);
    }
}
