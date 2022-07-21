using Furion.DynamicApiController;
using JiangDuo.Application.AppService.ServiceService.Dto;
using JiangDuo.Application.AppService.ServiceService.Services;
using JiangDuo.Application.AppService.SelectAreaService.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.ServiceService;

/// <summary>
/// 服务管理
/// </summary>
[Route("api/[controller]")]
[ApiDescriptionSettings(Name = "Service" , Groups =new string[] { "Default", "服务管理（一老一少）" })]
public class ServiceAppService : IDynamicApiController
{
 
    private readonly IServiceService _newsService;
    public ServiceAppService(IServiceService newsService)
    {
        _newsService = newsService;
    }
    /// <summary>
    /// 获取列表（分页）
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public PagedList<DtoService> Get([FromQuery] DtoServiceQuery model)
    {
        return _newsService.GetList(model);
    }

    /// <summary>
    /// 根据id获取详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<DtoService> Get(long id)
    {
        return await _newsService.GetById(id);
    }
    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<int> Insert(DtoServiceForm model)
    {
        return await _newsService.Insert(model);
    }
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<int> Update(DtoServiceForm model)
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
    [HttpPost("Delete")]
    public async Task<int> Delete([FromBody] List<long> idList)
    {
        return await _newsService.FakeDelete(idList);
    }
}
