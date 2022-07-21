using Furion.DynamicApiController;
using JiangDuo.Application.AppService.OnlineletterService.Dto;
using JiangDuo.Application.AppService.OnlineletterService.Services;
using JiangDuo.Application.AppService.SelectAreaService.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.OnlineletterService;

/// <summary>
/// 码上说马上办
/// </summary>
[Route("api/[controller]")]
[ApiDescriptionSettings("Default", "码上说马上办")]
public class OnlineletterAppService : IDynamicApiController
{
 
    private readonly IOnlineletterService _onlineletterService;
    public OnlineletterAppService(IOnlineletterService onlineletterService)
    {
        _onlineletterService = onlineletterService;
    }
    /// <summary>
    /// 获取列表（分页）
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public PagedList<DtoOnlineletter> Get([FromQuery] DtoOnlineletterQuery model)
    {
        return _onlineletterService.GetList(model);
    }

    /// <summary>
    /// 根据id获取详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<DtoOnlineletter> Get(long id)
    {
        return await _onlineletterService.GetById(id);
    }
    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<int> Insert(DtoOnlineletterForm model)
    {
        return await _onlineletterService.Insert(model);
    }
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<int> Update(DtoOnlineletterForm model)
    {
        return await _onlineletterService.Update(model);
    }
    /// <summary>
    /// 根据id删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<int> Delete(long id)
    {
        return await _onlineletterService.FakeDelete(id);
    }
    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="idList"></param>
    /// <returns></returns>
    [HttpPost("Delete")]
    public async Task<int> Delete([FromBody] List<long> idList)
    {
        return await _onlineletterService.FakeDelete(idList);
    }
}
