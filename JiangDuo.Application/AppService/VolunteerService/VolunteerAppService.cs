using Furion.DynamicApiController;
using JiangDuo.Application.AppService.VolunteerService.Dto;
using JiangDuo.Application.AppService.VolunteerService.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.VolunteerService;

/// <summary>
/// 志愿者管理
/// </summary>
[Route("api/[controller]")]
[ApiDescriptionSettings("Default", "志愿者管理")]
public class VolunteerAppService : IDynamicApiController
{
    private readonly IVolunteerService _volunteerService;

    public VolunteerAppService(IVolunteerService volunteerService)
    {
        _volunteerService = volunteerService;
    }

    /// <summary>
    /// 获取列表（分页）
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public PagedList<DtoVolunteer> Get([FromQuery] DtoVolunteerQuery model)
    {
        return _volunteerService.GetList(model);
    }

    /// <summary>
    /// 根据id获取详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<DtoVolunteer> Get(long id)
    {
        return await _volunteerService.GetById(id);
    }

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<int> Insert(DtoVolunteerForm model)
    {
        return await _volunteerService.Insert(model);
    }

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<int> Update(DtoVolunteerForm model)
    {
        return await _volunteerService.Update(model);
    }

    /// <summary>
    /// 根据id删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<int> Delete(long id)
    {
        return await _volunteerService.FakeDelete(id);
    }

    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="idList"></param>
    /// <returns></returns>
    [HttpPost("Delete")]
    public async Task<int> Delete([FromBody] List<long> idList)
    {
        return await _volunteerService.FakeDelete(idList);
    }
}