using Furion.DynamicApiController;
using JiangDuo.Application.AppService.OfficialsstructService.Dto;
using JiangDuo.Application.AppService.OfficialsstructService.Services;
using JiangDuo.Application.AppService.SelectAreaService.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.OfficialsstructService;

/// <summary>
/// 人大结构管理
/// </summary>
[Route("api/[controller]")]
public class OfficialsstructAppService : IDynamicApiController
{
 
    private readonly IOfficialsstructService _officialsstructService;
    public OfficialsstructAppService(IOfficialsstructService officialsstructService)
    {
        _officialsstructService = officialsstructService;
    }
    /// <summary>
    /// 获取列表（分页）
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public PagedList<DtoOfficialsstruct> Get([FromQuery] DtoOfficialsstructQuery model)
    {
        return _officialsstructService.GetList(model);
    }

    /// <summary>
    /// 根据id获取详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<DtoOfficialsstruct> Get(long id)
    {
        return await _officialsstructService.GetById(id);
    }
    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<int> Insert(DtoOfficialsstructForm model)
    {
        return await _officialsstructService.Insert(model);
    }
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<int> Update(DtoOfficialsstructForm model)
    {
        return await _officialsstructService.Update(model);
    }
    /// <summary>
    /// 根据id删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<int> Delete(long id)
    {
        return await _officialsstructService.FakeDelete(id);
    }
    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="idList"></param>
    /// <returns></returns>
    [HttpPost("Delete")]
    public async Task<int> Delete([FromBody] List<long> idList)
    {
        return await _officialsstructService.FakeDelete(idList);
    }
}
