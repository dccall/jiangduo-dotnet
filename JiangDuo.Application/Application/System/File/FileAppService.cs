﻿
using JiangDuo.Application.Role.Dtos;
using JiangDuo.Application.Role.Services;
using JiangDuo.Application.System.File.Dtos;
using JiangDuo.Application.Tools;
using JiangDuo.Core.Attributes;
using JiangDuo.Core.Enums;
using JiangDuo.Core.Models;
using Furion;
using Furion.DependencyInjection;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JiangDuo.Core.Filters;

namespace JiangDuo.Application.System.File
{
    /// <summary>
    /// 文件服务
    /// </summary>
    public class FileAppService : IDynamicApiController
    {
       

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="file"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<UploadFile> UploadFile(IFormFile file, [FromForm] UploadRequest model)
        {
           var fileInfo= await FileHelp.UploadFileAsync(file, model.FileSource);
           return fileInfo;
        }
        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="fileId">文件id</param>
        /// <returns></returns>
        [HttpGet, NonUnify]
        public  IActionResult Download(long fileId)
        {
            return  FileHelp.FileDownload(fileId);
        }

        /// <summary>
        /// 导出excel
        /// </summary>
        /// <returns></returns>
        [HttpGet, NonUnify]
        public  IActionResult ExportExcel()
        {
            List<Test> list = new List<Test>();
            for (int i = 0; i < 10; i++)
            {
                list.Add(new Test() { Id = i, Name = "名称"+ i });
            }
            return ExcelHelp.ExportExcel("测试文件.xlsx", list);
        }
        /// <summary>
        /// 导入Excel
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public List<Test> ImportExcel(IFormFile file)
        {
            var list = ExcelHelp.ParseExcelToList<Test>(file.OpenReadStream());
            return list;
        }
    }
}
