﻿using Furion.DynamicApiController;
using JiangDuo.Application.AppService.BusinessService.Dto;
using JiangDuo.Application.AppService.BusinessService.Services;
using JiangDuo.Application.AppService.SelectAreaService.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.BusinessService;

/// <summary>
/// 新闻管理
/// </summary>
[Route("api/[controller]")]
[ApiDescriptionSettings("Default", "新闻管理")]
public class BusinessAppService : IDynamicApiController
{
 
    private readonly IBusinessService _businessService;
    public BusinessAppService(IBusinessService businessService)
    {
        _businessService = businessService;
    }
    /// <summary>
    /// 获取列表（分页）
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public PagedList<DtoBusiness> Get([FromQuery] DtoBusinessQuery model)
    {
        return _businessService.GetList(model);
    }

    /// <summary>
    /// 根据id获取详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<DtoBusiness> Get(long id)
    {
        return await _businessService.GetById(id);
    }
    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<int> Insert(DtoBusinessForm model)
    {
        return await _businessService.Insert(model);
    }
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<int> Update(DtoBusinessForm model)
    {
        return await _businessService.Update(model);
    }
    /// <summary>
    /// 根据id删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<int> Delete(long id)
    {
        return await _businessService.FakeDelete(id);
    }
    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="idList"></param>
    /// <returns></returns>
    [HttpPost("Delete")]
    public async Task<int> Delete([FromBody] List<long> idList)
    {
        return await _businessService.FakeDelete(idList);
    }
}
