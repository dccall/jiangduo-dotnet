using Furion.DynamicApiController;
using JiangDuo.Application.AppService.BuildingService.Dto;
using JiangDuo.Application.AppService.BuildingService.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.BuildingService;

/// <summary>
/// 建筑管理
/// </summary>
[Route("api/[controller]")]
[ApiDescriptionSettings("Default", "建筑管理")]
public class BuildingAppService : IDynamicApiController
{
    private readonly IBuildingService _buildingService;

    public BuildingAppService(IBuildingService buildingService)
    {
        _buildingService = buildingService;
    }

    /// <summary>
    /// 获取列表（分页）
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public PagedList<DtoBuilding> Get([FromQuery] DtoBuildingQuery model)
    {
        return _buildingService.GetList(model);
    }

    /// <summary>
    /// 根据id获取详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<DtoBuilding> Get(long id)
    {
        return await _buildingService.GetById(id);
    }

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<int> Insert(DtoBuildingForm model)
    {
        return await _buildingService.Insert(model);
    }

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<int> Update(DtoBuildingForm model)
    {
        return await _buildingService.Update(model);
    }

    /// <summary>
    /// 根据id删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<int> Delete(long id)
    {
        return await _buildingService.FakeDelete(id);
    }

    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="idList"></param>
    /// <returns></returns>
    [HttpPost("Delete")]
    public async Task<int> Delete([FromBody] List<long> idList)
    {
        return await _buildingService.FakeDelete(idList);
    }
}