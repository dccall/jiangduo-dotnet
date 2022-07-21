using Furion.DynamicApiController;
using JiangDuo.Application.AppService.ReserveService.Dto;
using JiangDuo.Application.AppService.ReserveService.Services;
using JiangDuo.Application.AppService.SelectAreaService.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.ReserveService;

/// <summary>
/// 有事好商量（场地预约）
/// </summary>
[Route("api/[controller]")]
public class ReserveAppService : IDynamicApiController
{
 
    private readonly IReserveService _reserveService;
    public ReserveAppService(IReserveService reserveService)
    {
        _reserveService = reserveService;
    }
    /// <summary>
    /// 获取列表（分页）
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public PagedList<DtoReserve> Get([FromQuery] DtoReserveQuery model)
    {
        return _reserveService.GetList(model);
    }

    /// <summary>
    /// 根据id获取详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<DtoReserve> Get(long id)
    {
        return await _reserveService.GetById(id);
    }
    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<int> Insert(DtoReserveForm model)
    {
        return await _reserveService.Insert(model);
    }
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<int> Update(DtoReserveForm model)
    {
        return await _reserveService.Update(model);
    }
    /// <summary>
    /// 根据id删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<int> Delete(long id)
    {
        return await _reserveService.FakeDelete(id);
    }
    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="idList"></param>
    /// <returns></returns>
    [HttpPost("Delete")]
    public async Task<int> Delete([FromBody] List<long> idList)
    {
        return await _reserveService.FakeDelete(idList);
    }
}
