using Furion.DynamicApiController;
using JiangDuo.Application.AppService.NewsclassifyService.Dto;
using JiangDuo.Application.AppService.NewsclassifyService.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.NewsclassifyService;

/// <summary>
/// 新闻分类管理
/// </summary>
[Route("api/[controller]")]
[ApiDescriptionSettings("Default", "新闻分类管理")]
public class NewsclassifyAppService : IDynamicApiController
{
    private readonly INewsclassifyService _newsclassifyService;

    public NewsclassifyAppService(INewsclassifyService newsclassifyService)
    {
        _newsclassifyService = newsclassifyService;
    }

    /// <summary>
    /// 获取列表（分页）
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public PagedList<DtoNewsclassify> Get([FromQuery] DtoNewsclassifyQuery model)
    {
        return _newsclassifyService.GetList(model);
    }

    /// <summary>
    /// 根据id获取详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<DtoNewsclassify> Get(long id)
    {
        return await _newsclassifyService.GetById(id);
    }

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<int> Insert(DtoNewsclassifyForm model)
    {
        return await _newsclassifyService.Insert(model);
    }

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<int> Update(DtoNewsclassifyForm model)
    {
        return await _newsclassifyService.Update(model);
    }

    /// <summary>
    /// 根据id删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<int> Delete(long id)
    {
        return await _newsclassifyService.FakeDelete(id);
    }

    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="idList"></param>
    /// <returns></returns>
    [HttpPost("Delete")]
    public async Task<int> Delete([FromBody] List<long> idList)
    {
        return await _newsclassifyService.FakeDelete(idList);
    }
}