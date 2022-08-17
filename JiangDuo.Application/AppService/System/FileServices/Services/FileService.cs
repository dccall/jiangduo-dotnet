﻿using Furion;
using Furion.DatabaseAccessor;
using Furion.DatabaseAccessor.Extensions;
using Furion.DependencyInjection;
using Furion.FriendlyException;
using JiangDuo.Application.AppService.System.FileServices.Dtos;
using JiangDuo.Core.Enums;
using JiangDuo.Core.Models;
using JiangDuo.Core.Utils;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yitter.IdGenerator;

namespace JiangDuo.Application.AppService.System.FileServices.Services
{
    public class FileService: IFileService, ITransient
    {

        private readonly ILogger<FileService> _logger;
        private readonly IRepository<SysUploadFile> _uploadRepository;
        public FileService(ILogger<FileService> logger,
            IRepository<SysUploadFile> uploadRepository)
        {
            _logger = logger;
            _uploadRepository = uploadRepository;
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="model">数据</param>
        /// <returns></returns>
        public PagedList<DtoUploadFile> GetList(DtoUploadFileQuery model)
        {
            var query = _uploadRepository.Where(x => !x.IsDeleted);
            query = query.Where(model.FileSource != null, x => x.FileSource == model.FileSource) ;
            var pageList= _uploadRepository.Where(x => !x.IsDeleted).ProjectToType<DtoUploadFile>().ToPagedList(model.PageIndex, model.PageSize);
            return pageList;
        }
        /// <summary>
        /// 根据id查询详情
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<DtoUploadFile> GetById(long id)
        {
            var entity = await _uploadRepository.FindOrDefaultAsync(id);
            if (entity == null)
            {
                throw Oops.Oh($"数据不存在");
            }
            var dto = entity.Adapt<DtoUploadFile>();
            return dto;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public async Task<int> Delete(long id)
        {
            var file = await _uploadRepository.FindOrDefaultAsync(id);
            var count = 0;
            if (file != null)
            {
                _uploadRepository.Delete(id);
                count = await _uploadRepository.SaveNowAsync();
            }
            if (count > 0)
            {
                // 获取文件路径
                var filePath = file.FilePath;
                var saveDirectory = Path.Combine(App.HostEnvironment.ContentRootPath, filePath);
                if (File.Exists(saveDirectory))
                {
                    try
                    {
                        File.Delete(saveDirectory);
                    }
                    catch (Exception) { }
                }
            }
            return count;
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="idList">id集合</param>
        /// <returns></returns>
        public async Task<int> Delete(List<long> idList)
        {
            var fileList= _uploadRepository.AsQueryable().Where(x => idList.Contains(x.FileId));
            foreach (var file in fileList)
            {
                _uploadRepository.Delete(file);
            }
            var count = await _uploadRepository.SaveNowAsync();
            if (count > 0)
            {
                //删除物理文件
                foreach (var file in fileList)
                {
                    // 获取文件路径
                    var filePath = file.FilePath;
                    var saveDirectory = Path.Combine(App.HostEnvironment.ContentRootPath, filePath);
                    if (File.Exists(saveDirectory))
                    {
                        try
                        {
                            File.Delete(saveDirectory);
                        }
                        catch (Exception){}
                    }
                }
            }
            return count;
        }

        public async Task<SysUploadFile> UploadFileAsync(IFormFile file, UploadFileSource fileSource = UploadFileSource.Null)
        {
            if (file == null)
            {
                throw Oops.Oh($"缺少上传文件");
            }
            var userId = JwtHelper.GetAccountId();
            // 如：保存到网站根目录下的 uploads 目录
            var path = @"uploads\" + userId;
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
            var savePath = Path.Combine(saveDirectory, fileName);
            // 保存到指定路径
            using (var stream = File.Create(savePath))
            {
                await file.CopyToAsync(stream);
            }
            var request = App.HttpContext.Request;
            SysUploadFile fileInfo = new SysUploadFile();
            fileInfo.FileId = YitIdHelper.NextId();
            fileInfo.Name = oldName;
            fileInfo.FileName = fileName;
            fileInfo.FilePath = filePath;
            //fileInfo.Url = $"{request.Scheme}://{request.Host.Value}/api/file/download/{fileInfo.Id}";
            fileInfo.Url = $"/api/file/download/{fileInfo.FileId}";
            fileInfo.FileSource = fileSource;
            fileInfo.FileExt = fileExt;
            fileInfo.FileLength = size;  // 文件大小 KB
            fileInfo.CreatedTime = DateTime.Now;
            fileInfo.Creator = userId;
            await _uploadRepository.InsertNowAsync(fileInfo);

            return fileInfo;
        }

        public  IActionResult FileDownload(long fileId)
        {
            var uploadFile = _uploadRepository.FindOrDefault(fileId);
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

        private  FileStream GetFileStream(long fileId)
        {
            var uploadFile = _uploadRepository.FindOrDefault(fileId);
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
