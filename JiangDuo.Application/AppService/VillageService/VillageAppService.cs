using Furion.DynamicApiController;
using JiangDuo.Application.AppService.VillageService.Dto;
using JiangDuo.Application.AppService.VillageService.Services;
using JiangDuo.Application.AppService.SelectAreaService.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.VillageService;

/// <summary>
/// 村落管理
/// </summary>
[Route("api/[controller]")]
[ApiDescriptionSettings("Default", "村落管理")]
public class VillageAppService : IDynamicApiController
{
 
    private readonly IVillageService _villageService;
    public VillageAppService(IVillageService villageService)
    {
        _villageService = villageService;
    }
    /// <summary>
    /// 获取列表（分页）
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public PagedList<DtoVillage> Get([FromQuery] DtoVillageQuery model)
    {
        return _villageService.GetList(model);
    }

    /// <summary>
    /// 根据id获取详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<DtoVillage> Get(long id)
    {
        return await _villageService.GetById(id);
    }
    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<int> Insert(DtoVillageForm model)
    {
        return await _villageService.Insert(model);
    }
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<int> Update(DtoVillageForm model)
    {
        return await _villageService.Update(model);
    }
    /// <summary>
    /// 根据id删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<int> Delete(long id)
    {
        return await _villageService.FakeDelete(id);
    }
    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="idList"></param>
    /// <returns></returns>
    [HttpPost("Delete")]
    public async Task<int> Delete([FromBody] List<long> idList)
    {
        return await _villageService.FakeDelete(idList);
    }
}
