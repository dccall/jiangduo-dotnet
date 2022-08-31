using Furion.DynamicApiController;
using JiangDuo.Application.AppService.RegionService.Dto;
using JiangDuo.Application.AppService.RegionService.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace JiangDuo.Application.AppService.RegionService;

/// <summary>
/// 省市区
/// </summary>
[Route("api/[controller]")]
[ApiDescriptionSettings("Default", "省市区")]
public class RegionAppService : IDynamicApiController
{
    private readonly IRegionService _regionService;

    public RegionAppService(IRegionService regionService)
    {
        _regionService = regionService;
    }

    /// <summary>
    /// 获取列表（分页）
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public PagedList<DtoRegion> Get([FromQuery] DtoRegionQuery model)
    {
        return _regionService.GetList(model);
    }

    ///// <summary>
    ///// 根据id获取详情
    ///// </summary>
    ///// <param name="id"></param>
    ///// <returns></returns>
    //public async Task<DtoRegion> Get(long id)
    //{
    //    return await _regionService.GetById(id);
    //}
    ///// <summary>
    ///// 新增
    ///// </summary>
    ///// <param name="model"></param>
    ///// <returns></returns>
    //public async Task<int> Insert(DtoRegionForm model)
    //{
    //    return await _regionService.Insert(model);
    //}
    ///// <summary>
    ///// 修改
    ///// </summary>
    ///// <param name="model"></param>
    ///// <returns></returns>
    //public async Task<int> Update(DtoRegionForm model)
    //{
    //    return await _regionService.Update(model);
    //}
    ///// <summary>
    ///// 根据id删除
    ///// </summary>
    ///// <param name="id"></param>
    ///// <returns></returns>
    //public async Task<int> Delete(long id)
    //{
    //    return await _regionService.FakeDelete(id);
    //}
    ///// <summary>
    ///// 批量删除
    ///// </summary>
    ///// <param name="idList"></param>
    ///// <returns></returns>
    //[HttpPost("Delete")]
    //public async Task<int> Delete([FromBody] List<long> idList)
    //{
    //    return await _regionService.FakeDelete(idList);
    //}
}