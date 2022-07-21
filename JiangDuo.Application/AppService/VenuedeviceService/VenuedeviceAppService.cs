using Furion.DynamicApiController;
using JiangDuo.Application.AppService.VenuedeviceService.Dto;
using JiangDuo.Application.AppService.VenuedeviceService.Services;
using JiangDuo.Application.AppService.SelectAreaService.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.VenuedeviceService;

/// <summary>
/// 场地设备管理
/// </summary>
[Route("api/[controller]")]
[ApiDescriptionSettings("Default", "场地设备管理")]
public class VenuedeviceAppService : IDynamicApiController
{
 
    private readonly IVenuedeviceService _venuedeviceService;
    public VenuedeviceAppService(IVenuedeviceService venuedeviceService)
    {
        _venuedeviceService = venuedeviceService;
    }
    /// <summary>
    /// 获取列表（分页）
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public PagedList<DtoVenuedevice> Get([FromQuery] DtoVenuedeviceQuery model)
    {
        return _venuedeviceService.GetList(model);
    }

    /// <summary>
    /// 根据id获取详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<DtoVenuedevice> Get(long id)
    {
        return await _venuedeviceService.GetById(id);
    }
    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<int> Insert(DtoVenuedeviceForm model)
    {
        return await _venuedeviceService.Insert(model);
    }
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<int> Update(DtoVenuedeviceForm model)
    {
        return await _venuedeviceService.Update(model);
    }
    /// <summary>
    /// 根据id删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<int> Delete(long id)
    {
        return await _venuedeviceService.FakeDelete(id);
    }
    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="idList"></param>
    /// <returns></returns>
    [HttpPost("Delete")]
    public async Task<int> Delete([FromBody] List<long> idList)
    {
        return await _venuedeviceService.FakeDelete(idList);
    }
}
