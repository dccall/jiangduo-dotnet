using Furion.DynamicApiController;
using JiangDuo.Application.AppService.ResidentService.Dto;
using JiangDuo.Application.AppService.ResidentService.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.ResidentService;

/// <summary>
/// 居民管理
/// </summary>
[Route("api/[controller]")]
[ApiDescriptionSettings("Default", "居民管理")]
public class ResidentAppService : IDynamicApiController
{
    private readonly IResidentService _residentService;

    public ResidentAppService(IResidentService residentService)
    {
        _residentService = residentService;
    }

    /// <summary>
    /// 获取列表（分页）
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public PagedList<DtoResident> Get([FromQuery] DtoResidentQuery model)
    {
        return _residentService.GetList(model);
    }

    /// <summary>
    /// 根据id获取详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<DtoResident> Get(long id)
    {
        return await _residentService.GetById(id);
    }

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<int> Insert(DtoResidentForm model)
    {
        return await _residentService.Insert(model);
    }

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<int> Update(DtoResidentForm model)
    {
        return await _residentService.Update(model);
    }

    /// <summary>
    /// 根据id删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<int> Delete(long id)
    {
        return await _residentService.FakeDelete(id);
    }

    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="idList"></param>
    /// <returns></returns>
    [HttpPost("Delete")]
    public async Task<int> Delete([FromBody] List<long> idList)
    {
        return await _residentService.FakeDelete(idList);
    }
}