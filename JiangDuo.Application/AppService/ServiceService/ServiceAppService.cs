using Furion.DynamicApiController;
using JiangDuo.Application.AppService.ServiceService.Dto;
using JiangDuo.Application.AppService.ServiceService.Services;
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
[ApiDescriptionSettings(Name = "Service" , Groups =new string[] { "Default", "服务管理（一老一小）" })]
public class ServiceAppService : IDynamicApiController
{
 
    private readonly IServiceService _serviceService;
    public ServiceAppService(IServiceService serviceService)
    {
        _serviceService = serviceService;
    }
    /// <summary>
    /// 获取列表（分页）
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public PagedList<DtoService> Get([FromQuery] DtoServiceQuery model)
    {
        return _serviceService.GetList(model);
    }

    /// <summary>
    /// 根据id获取详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<DtoService> Get(long id)
    {
        return await _serviceService.GetById(id);
    }
    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<int> Insert(DtoServiceForm model)
    {
        return await _serviceService.Insert(model);
    }
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<int> Update(DtoServiceForm model)
    {
        return await _serviceService.Update(model);
    }


    /// <summary>
    /// 修改状态
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("UpdateStatus")]
    public async Task<int> UpdateStatus(DtoUpdateServiceStatus model)
    {
        return await _serviceService.UpdateStatus(model);
    }

    /// <summary>
    /// 根据id删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<int> Delete(long id)
    {
        return await _serviceService.FakeDelete(id);
    }
    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="idList"></param>
    /// <returns></returns>
    [HttpPost("Delete")]
    public async Task<int> Delete([FromBody] List<long> idList)
    {
        return await _serviceService.FakeDelete(idList);
    }
}
