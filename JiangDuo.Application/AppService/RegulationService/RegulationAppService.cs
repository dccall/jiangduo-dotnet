using Furion.DynamicApiController;
using JiangDuo.Application.AppService.RegulationService.Dto;
using JiangDuo.Application.AppService.RegulationService.Services;
using JiangDuo.Application.AppService.SelectAreaService.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.RegulationService;

/// <summary>
/// 规则制度管理
/// </summary>
[Route("api/[controller]")]
public class RegulationAppService : IDynamicApiController
{
 
    private readonly IRegulationService _newsService;
    public RegulationAppService(IRegulationService newsService)
    {
        _newsService = newsService;
    }
    /// <summary>
    /// 获取列表（分页）
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public PagedList<DtoRegulation> Get([FromQuery] DtoRegulationQuery model)
    {
        return _newsService.GetList(model);
    }

    /// <summary>
    /// 根据id获取详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<DtoRegulation> Get(long id)
    {
        return await _newsService.GetById(id);
    }
    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<int> Insert(DtoRegulationForm model)
    {
        return await _newsService.Insert(model);
    }
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<int> Update(DtoRegulationForm model)
    {
        return await _newsService.Update(model);
    }
    /// <summary>
    /// 根据id删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<int> Delete(long id)
    {
        return await _newsService.FakeDelete(id);
    }
    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="idList"></param>
    /// <returns></returns>
    [HttpPost("Deletess")]
    public async Task<int> Delete([FromBody] List<long> idList)
    {
        return await _newsService.FakeDelete(idList);
    }
}
