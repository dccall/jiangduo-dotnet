using Furion.DynamicApiController;
using JiangDuo.Application.AppService.System.FileServices.Dtos;
using JiangDuo.Application.AppService.System.FileServices.Services;
using JiangDuo.Application.System.FileServices.Dtos;
using JiangDuo.Application.Tools;
using JiangDuo.Core.Models;
using JiangDuo.Core.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace JiangDuo.Application.System.FileServices
{
    /// <summary>
    /// 文件服务
    /// </summary>
    [ApiDescriptionSettings(Name = "File", Groups = new string[] { "Default", "文件服务" })]
    public class FileAppService : IDynamicApiController
    {
        private readonly IFileService _fileService;

        public FileAppService(IFileService fileService)
        {
            _fileService = fileService;
        }

        /// <summary>
        /// 获取列表（分页）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public PagedList<DtoUploadFile> Get([FromQuery] DtoUploadFileQuery model)
        {
            return _fileService.GetList(model);
        }

        /// <summary>
        /// 根据id获取详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<DtoUploadFile> Get(long id)
        {
            return await _fileService.GetById(id);
        }

        /// <summary>
        /// 根据id删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> Delete(long id)
        {
            return await _fileService.Delete(id);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        [HttpPost("Delete")]
        public async Task<int> Delete([FromBody] List<long> idList)
        {
            return await _fileService.Delete(idList);
        }

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="file"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<SysUploadFile> UploadFile(IFormFile file, [FromForm] UploadRequest model)
        {
            var fileInfo = await _fileService.UploadFileAsync(file, model.FileSource);
            return fileInfo;
        }

        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="fileId">文件id</param>
        /// <returns></returns>
        [HttpGet, NonUnify]
        [AllowAnonymous]
        public IActionResult Download(long fileId)
        {
            return _fileService.FileDownload(fileId);
        }

        /// <summary>
        /// 导出excel
        /// </summary>
        /// <returns></returns>
        [HttpGet, NonUnify]
        public IActionResult ExportExcel()
        {
            List<Test> list = new List<Test>();
            for (int i = 0; i < 10; i++)
            {
                list.Add(new Test() { Id = i, Name = "名称" + i });
            }

            var file = NPOIHelper.ExportXlsFile(list, "测试文件");
            return new FileStreamResult(new MemoryStream(file.ToArray()), "application/octet-stream")
            {
                FileDownloadName = "测试文件.xls" // 配置文件下载显示名
            };

            //return ExcelHelp.ExportExcel("测试文件.xlsx", list);
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