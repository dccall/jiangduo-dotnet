using JiangDuo.Core;
using JiangDuo.Core.Enums;
using JiangDuo.Core.Models;
using Furion;
using Furion.DatabaseAccessor;
using Furion.FriendlyException;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yitter.IdGenerator;
using JiangDuo.Core.Utils;

namespace JiangDuo.Application.Tools
{
    public static class FileHelp
    {
        
        public static async Task<SysUploadFile> UploadFileAsync(IFormFile file, UploadFileSource fileSource= UploadFileSource.Null)
        {
            if (file == null)
            {
                throw Oops.Oh($"缺少上传文件");
            }
            var uploadFileRepository = Db.GetRepository<SysUploadFile>();
            var userId = JwtHelper.GetAccountId();
            // 如：保存到网站根目录下的 uploads 目录
            var path = "uploads/"+ userId;  
            var saveDirectory = Path.Combine(App.HostEnvironment.ContentRootPath, path);
            if (!Directory.Exists(saveDirectory)) Directory.CreateDirectory(saveDirectory);
            //// 这里还可以获取文件的信息
            var size = file.Length;  // 文件大小 KB
            var oldName = file.FileName; // 客户端上传的文件名
            var fileExt = Path.GetExtension(file.FileName);
            // var contentType = file.ContentType; // 获取文件 ContentType 或解析 MIME 类型
            // 避免文件名重复，采用 GUID 生成
            var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(path, fileName);
            var savePath= Path.Combine(saveDirectory, fileName);
            // 保存到指定路径
            using (var stream = File.Create(savePath))
            {
                await file.CopyToAsync(stream);
            }
            var request = App.HttpContext.Request;
            SysUploadFile fileInfo = new SysUploadFile();
            fileInfo.FileId = YitIdHelper.NextId();
            fileInfo.Name= oldName;
            fileInfo.FileName = fileName;
            //fileInfo.FilePath = filePath;
            //fileInfo.FilePath = $"{request.Scheme}://{request.Host.Value}/api/file/download/{fileInfo.Id}";
            fileInfo.FilePath = $"/file/download/{fileInfo.FileId}";
            fileInfo.FileSource = fileSource;
            fileInfo.FileExt = fileExt;
            fileInfo.FileLength = size;  // 文件大小 KB
            fileInfo.CreatedTime = DateTimeOffset.UtcNow;
            fileInfo.Creator = userId;
            await uploadFileRepository.InsertNowAsync(fileInfo);

            return fileInfo;
        }

        public static IActionResult FileDownload(long fileId)
        {
            var uploadFileRepository = Db.GetRepository<SysUploadFile>();
            var uploadFile= uploadFileRepository.FindOrDefault(fileId);
            if (uploadFile == null)
            {
                throw Oops.Oh($"未找到对应的文件");
            }
            // 如：保存到网站根目录下的 uploads 目录
            var filePath = uploadFile.FilePath;
            var saveDirectory = Path.Combine(App.HostEnvironment.ContentRootPath, filePath);
            if (!File.Exists(saveDirectory))
            {
                throw Oops.Oh($"文件不存在");
            }
            return new FileStreamResult(new FileStream(filePath, FileMode.Open), "application/octet-stream")
            {
                FileDownloadName = uploadFile.Name // 配置文件下载显示名
            };
        }

        public static FileStream GetFileStream(long fileId)
        {
            var uploadFileRepository = Db.GetRepository<SysUploadFile>();
            var uploadFile = uploadFileRepository.FindOrDefault(fileId);
            // 如：保存到网站根目录下的 uploads 目录
            var filePath = uploadFile.FilePath;
            var saveDirectory = Path.Combine(App.HostEnvironment.ContentRootPath, filePath);
            if (!File.Exists(saveDirectory))
            {
                throw Oops.Oh($"未找到对应的文件");
            }
            return new FileStream(filePath, FileMode.Open);
        }
    }


   
}
