﻿using Furion.DynamicApiController;
using JiangDuo.Application.AppService.ReserveService.Dto;
using JiangDuo.Application.AppService.ReserveService.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.ReserveService;

/// <summary>
/// 有事好商量（场地预约）
/// </summary>
[Route("api/[controller]")]
[ApiDescriptionSettings("Default", "有事好商量（场地预约）")]
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
    /// 修改状态
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("UpdateStatus")]
    public async Task<int> UpdateStatus(DtoReserveQueryStatus model)
    {
        return await _reserveService.UpdateStatus(model);
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