using Furion.DynamicApiController;
using JiangDuo.Application.AppService.OfficialService.Dto;
using JiangDuo.Application.AppService.OfficialService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.OfficialService;

/// <summary>
/// 人大名单管理
/// </summary>
[Route("api/[controller]")]
[ApiDescriptionSettings("Default", "人大名单管理")]
public class OfficialAppService : IDynamicApiController
{
    private readonly IOfficialService _officialService;

    public OfficialAppService(IOfficialService officialService)
    {
        _officialService = officialService;
    }

    /// <summary>
    /// 获取列表（分页）
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public PagedList<DtoOfficial> Get([FromQuery] DtoOfficialQuery model)
    {
        return _officialService.GetList(model);
    }

    /// <summary>
    /// 根据id获取详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<DtoOfficial> Get(long id)
    {
        return await _officialService.GetById(id);
    }

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<int> Insert(DtoOfficialForm model)
    {
        return await _officialService.Insert(model);
    }

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<int> Update(DtoOfficialForm model)
    {
        return await _officialService.Update(model);
    }

    /// <summary>
    /// 根据id删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<int> Delete(long id)
    {
        return await _officialService.FakeDelete(id);
    }

    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="idList"></param>
    /// <returns></returns>
    [HttpPost("Delete")]
    public async Task<int> Delete([FromBody] List<long> idList)
    {
        return await _officialService.FakeDelete(idList);
    }


    /// <summary>
    /// 导出excel
    /// </summary>
    /// <returns></returns>
    [HttpGet, NonUnify]
    public IActionResult ExportExcel([FromQuery]DtoOfficialQuery model)
    {
        var ms= _officialService.ExportExcel(model);

        return new FileStreamResult(new MemoryStream(ms.ToArray()), "application/octet-stream")
        {
            FileDownloadName = "人大.xlsx" // 配置文件下载显示名
        };
    }


    /// <summary>
    /// 导入excel
    /// </summary>
    /// <returns></returns>
    public Task<bool> ImportExcel(IFormFile file)
    {
        return _officialService.ImportExcel(file);
    }
}