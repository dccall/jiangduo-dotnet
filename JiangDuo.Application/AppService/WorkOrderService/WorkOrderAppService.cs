using Furion.DynamicApiController;
using JiangDuo.Application.AppService.WorkOrderService.Dto;
using JiangDuo.Application.AppService.WorkOrderService.Services;
using JiangDuo.Application.AppService.SelectAreaService.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JiangDuo.Application.AppService.WorkorderService.Dto;
using Furion.DatabaseAccessor;

namespace JiangDuo.Application.AppService.WorkOrderService;

/// <summary>
/// 工单管理
/// </summary>
[Route("api/[controller]")]
[ApiDescriptionSettings("Default","工单管理")]
public class WorkOrderAppService : IDynamicApiController
{
 
    private readonly IWorkOrderService _workOrderService;
    public WorkOrderAppService(IWorkOrderService workOrderService)
    {
        _workOrderService = workOrderService;
    }
    /// <summary>
    /// 获取列表（分页）
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public PagedList<DtoWorkOrder> Get([FromQuery] DtoWorkOrderQuery model)
    {
        return _workOrderService.GetList(model);
    }

    /// <summary>
    /// 根据id获取详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<DtoWorkOrder> Get(long id)
    {
        return await _workOrderService.GetById(id);
    }
    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [UnitOfWork]
    public async Task<int> Insert(DtoWorkOrderForm model)
    {
        return await _workOrderService.Insert(model);
    }
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [UnitOfWork]
    public async Task<int> Update(DtoWorkOrderForm model)
    {
        return await _workOrderService.Update(model);
    }
    /// <summary>
    /// 根据id删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<int> Delete(long id)
    {
        return await _workOrderService.FakeDelete(id);
    }
    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="idList"></param>
    /// <returns></returns>
    [HttpPost("Delete")]
    public async Task<int> Delete([FromBody] List<long> idList)
    {
        return await _workOrderService.FakeDelete(idList);
    }
}
