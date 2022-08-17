using JiangDuo.Application.AppService.System.FileServices.Dtos;
using JiangDuo.Core.Enums;
using JiangDuo.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.System.FileServices.Services
{
    public interface IFileService
    {
        /// <summary>
        /// 获取列表（分页）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public PagedList<DtoUploadFile> GetList([FromQuery] DtoUploadFileQuery model);

        /// <summary>
        /// 根据id获取详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<DtoUploadFile> GetById(long id);
        /// <summary>
        /// 根据id删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<int> Delete(long id);
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        public Task<int> Delete([FromBody] List<long> idList);


        public Task<SysUploadFile> UploadFileAsync(IFormFile file, UploadFileSource fileSource = UploadFileSource.Null);
        public IActionResult FileDownload(long fileId);
    }
}
