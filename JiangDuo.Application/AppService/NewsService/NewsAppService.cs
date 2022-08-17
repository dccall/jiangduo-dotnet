using Furion.DynamicApiController;
using JiangDuo.Application.AppService.NewsService.Dto;
using JiangDuo.Application.AppService.NewsService.Services;
using JiangDuo.Application.AppService.SelectAreaService.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.NewsService;

/// <summary>
/// 新闻管理
/// </summary>
[Route("api/[controller]")]
[ApiDescriptionSettings("Default", "新闻管理")]
public class NewsAppService : IDynamicApiController
{
 
    private readonly INewsService _newsService;
    public NewsAppService(INewsService newsService)
    {
        _newsService = newsService;
    }
    /// <summary>
    /// 获取列表（分页）
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public PagedList<DtoNews> Get([FromQuery] DtoNewsQuery model)
    {
        return _newsService.GetList(model);
    }

    /// <summary>
    /// 根据id获取详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<DtoNews> Get(long id)
    {
        return await _newsService.GetById(id);
    }
    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<int> Insert(DtoNewsForm model)
    {
        return await _newsService.Insert(model);
    }
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<int> Update(DtoNewsForm model)
    {
        return await _newsService.Update(model);
    }
    /// <summary>
    /// 更新状态
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("UpdateStatus")]
    public async Task<int> UpdateStatus(DtoNewsUpdateStatus model)
    {
        return await _newsService.UpdateStatus(model);
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
    [HttpPost("Delete")]
    public async Task<int> Delete([FromBody] List<long> idList)
    {
        return await _newsService.FakeDelete(idList);
    }
}
