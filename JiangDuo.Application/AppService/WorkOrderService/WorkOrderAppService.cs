using Furion.DatabaseAccessor;
using Furion.DynamicApiController;
using JiangDuo.Application.AppService.WorkorderService.Dto;
using JiangDuo.Application.AppService.WorkOrderService.Dto;
using JiangDuo.Application.AppService.WorkOrderService.Services;
using JiangDuo.Application.Tools;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.WorkOrderService;

/// <summary>
/// 工单管理
/// </summary>
[Route("api/[controller]")]
[ApiDescriptionSettings("Default", "工单管理")]
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
    /// 工单导出
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpGet, NonUnify]
    public IActionResult Export([FromQuery] DtoWorkOrderQuery model)
    {
        //查询数据
        var list = _workOrderService.GetList(model).Items.ToList();
        var list2 = list.Adapt<List<DtoWorkOrderExportExcel>>();
        return ExcelHelp.ExportExcel("工单列表.xlsx", list2);
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

    /// <summary>
    /// 工单指派
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<string> WorkOrderAssign(DtoWorkOrderAssign model)
    {
        return await _workOrderService.WorkOrderAssign(model);
    }

    /// <summary>
    /// 工单完成
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<string> WorkOrderCompleted(DtoWorkOrderCompletedHandel model)
    {
        return await _workOrderService.WorkOrderCompleted(model);
    }

    /// <summary>
    /// 工单完结
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<string> WorkOrderEnd(DtoWorkOrderEndHandel model)
    {
        return await _workOrderService.WorkOrderEnd(model);
    }

    /// <summary>
    /// 工单处理（不变更状态）
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<string> WorkOrderHandel(DtoWorkOrderHandel model)
    {
        return await _workOrderService.WorkOrderHandel(model);
    }

    /// <summary>
    /// 工单处理（状态变更）
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<string> WorkOrderUpdateStatus(DtoWorkOrderUpdateStatus model)
    {
        return await _workOrderService.WorkOrderUpdateStatus(model);
    }


  
}