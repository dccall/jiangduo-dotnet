using Furion;
using Furion.DynamicApiController;
using Furion.ViewEngine;
using JiangDuo.Application.AppService.ReserveService.Dto;
using JiangDuo.Application.AppService.ReserveService.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.ReserveService;

/// <summary>
/// 有事好商量（场地预约）
/// </summary>
[Route("api/[controller]")]
[ApiDescriptionSettings("Default", "有事好商量（场地预约）")]
public class ReserveAppService : IDynamicApiController
{
    private readonly IReserveService _reserveService;
    private readonly IViewEngine _viewEngine;
    public ReserveAppService(IReserveService reserveService, IViewEngine viewEngine)
    {
        _reserveService = reserveService;
        _viewEngine = viewEngine;
    }

    /// <summary>
    /// 获取列表（分页）
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public PagedList<DtoReserve> Get([FromQuery] DtoReserveQuery model)
    {
        return _reserveService.GetList(model);
    }

    /// <summary>
    /// 根据id获取详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<DtoReserve> Get(long id)
    {
        return await _reserveService.GetById(id);
    }

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<int> Insert(DtoReserveForm model)
    {
        return await _reserveService.Insert(model);
    }

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<int> Update(DtoReserveForm model)
    {
        return await _reserveService.Update(model);
    }

    /// <summary>
    /// 修改状态
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("UpdateStatus")]
    public async Task<int> UpdateStatus(DtoReserveStatus model)
    {
        return await _reserveService.UpdateStatus(model);
    }
    /// <summary>
    /// 预约完结
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("ReserveOver")]
    public async Task<int> ReserveOver(DtoReserveOverForm model)
    {
        return await _reserveService.ReserveOver(model);
    }

    /// <summary>
    /// 根据id删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<int> Delete(long id)
    {
        return await _reserveService.FakeDelete(id);
    }

    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="idList"></param>
    /// <returns></returns>
    [HttpPost("Delete")]
    public async Task<int> Delete([FromBody] List<long> idList)
    {
        return await _reserveService.FakeDelete(idList);
    }

    /// <summary>
    /// 导出word
    /// </summary>
    /// <returns></returns>
    [HttpGet, NonUnify]
    public IActionResult ExportWord([FromQuery] DtoReserveQuery model)
    {
        var path = $"template/ReserveTemplate.html";
        var templatePath = Path.Combine(App.HostEnvironment.ContentRootPath, path);
        var file = new FileStream(templatePath, FileMode.Open, FileAccess.Read);
        var template = File.ReadAllText(templatePath);
        var pageList=  _reserveService.GetList(model);
        var html= _viewEngine.RunCompile(template, pageList,builderAction: builder =>
        {
            builder.AddAssemblyReference(typeof(PagedList)); // 通过类型
            builder.AddAssemblyReferenceByName("JiangDuo.Core");
        });

        MemoryStream stream = new MemoryStream();
        StreamWriter writer = new StreamWriter(stream);
        writer.Write(html);
        writer.Flush();

        return new FileStreamResult(new MemoryStream(stream.ToArray()), "application/msword")
        {
            FileDownloadName = "代表建议.doc" // 配置文件下载显示名
        };
    }
}