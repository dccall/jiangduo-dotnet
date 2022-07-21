using Furion.DynamicApiController;
using JiangDuo.Application.AppService.SelectAreaService.Dto;
using JiangDuo.Application.AppService.SelectAreaService.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.SelectAreaService;

/// <summary>
/// 选区管理
/// </summary>
[Route("api/[controller]")]
[ApiDescriptionSettings("Default", "选区管理")]
public class SelectAreaAppService : IDynamicApiController
{
 
    private readonly ISelectAreaService _selectAreaService;
    public SelectAreaAppService(ISelectAreaService SelectAreaService)
    {
        _selectAreaService = SelectAreaService;
    }
    /// <summary>
    /// 获取列表（分页）
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public PagedList<DtoSelectArea> Get([FromQuery] DtoSelectAreaQuery model)
    {
        return _selectAreaService.GetList(model);
    }

    /// <summary>
    /// 根据id获取详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<DtoSelectArea> Get(long id)
    {
        return await _selectAreaService.GetById(id);
    }
    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<int> Insert(DtoSelectAreaForm model)
    {
        return await _selectAreaService.Insert(model);
    }
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<int> Update(DtoSelectAreaForm model)
    {
        return await _selectAreaService.Update(model);
    }
    /// <summary>
    /// 根据id删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<int> Delete(long id)
    {
        return await _selectAreaService.FakeDelete(id);
    }
    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="idList"></param>
    /// <returns></returns>
    [HttpPost("Delete")]
    public async Task<int> Delete([FromBody] List<long> idList)
    {
        return await _selectAreaService.FakeDelete(idList);
    }
}
