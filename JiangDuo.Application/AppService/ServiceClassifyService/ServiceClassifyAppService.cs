using Furion.DynamicApiController;
using JiangDuo.Application.AppService.ServiceClassifyService.Dto;
using JiangDuo.Application.AppService.ServiceClassifyService.Services;
using JiangDuo.Application.AppService.SelectAreaService.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.ServiceClassifyService;

/// <summary>
/// 服务分类
/// </summary>
[Route("api/[controller]")]
[ApiDescriptionSettings("Default", "服务管理（一老一小）", "服务分类")]
public class ServiceClassifyAppService : IDynamicApiController
{
 
    private readonly IServiceClassifyService _serviceClassifyService;
    public ServiceClassifyAppService(IServiceClassifyService serviceClassifyService)
    {
        _serviceClassifyService = serviceClassifyService;
    }
    /// <summary>
    /// 获取列表（分页）
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public PagedList<DtoServiceClassify> Get([FromQuery] DtoServiceClassifyQuery model)
    {
        return _serviceClassifyService.GetList(model);
    }

    /// <summary>
    /// 根据id获取详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<DtoServiceClassify> Get(long id)
    {
        return await _serviceClassifyService.GetById(id);
    }
    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<int> Insert(DtoServiceClassifyForm model)
    {
        return await _serviceClassifyService.Insert(model);
    }
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<int> Update(DtoServiceClassifyForm model)
    {
        return await _serviceClassifyService.Update(model);
    }
    /// <summary>
    /// 根据id删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<int> Delete(long id)
    {
        return await _serviceClassifyService.FakeDelete(id);
    }
    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="idList"></param>
    /// <returns></returns>
    [HttpPost("Delete")]
    public async Task<int> Delete([FromBody] List<long> idList)
    {
        return await _serviceClassifyService.FakeDelete(idList);
    }
}
