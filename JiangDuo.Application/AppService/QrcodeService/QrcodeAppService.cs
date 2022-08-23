using Furion.DynamicApiController;
using JiangDuo.Application.AppService.QrcodeService.Dto;
using JiangDuo.Application.AppService.QrcodeService.Services;
using JiangDuo.Application.AppService.SelectAreaService.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.QrcodeService;

/// <summary>
/// 二维码管理
/// </summary>
[Route("api/[controller]")]
[ApiDescriptionSettings("Default", "二维码管理")]
public class QrcodeAppService : IDynamicApiController
{
 
    private readonly IQrcodeService _qrcodeService;
    public QrcodeAppService(IQrcodeService qrcodeService)
    {
        _qrcodeService = qrcodeService;
    }
    /// <summary>
    /// 获取列表（分页）
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public PagedList<DtoQrcode> Get([FromQuery] DtoQrcodeQuery model)
    {
        return _qrcodeService.GetList(model);
    }

    /// <summary>
    /// 根据id获取详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<DtoQrcode> Get(long id)
    {
        return await _qrcodeService.GetById(id);
    }
    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<int> Insert(DtoQrcodeForm model)
    {
        return await _qrcodeService.Insert(model);
    }
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<int> Update(DtoQrcodeForm model)
    {
        return await _qrcodeService.Update(model);
    }
    /// <summary>
    /// 根据id删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<int> Delete(long id)
    {
        return await _qrcodeService.FakeDelete(id);
    }
    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="idList"></param>
    /// <returns></returns>
    [HttpPost("Delete")]
    public async Task<int> Delete([FromBody] List<long> idList)
    {
        return await _qrcodeService.FakeDelete(idList);
    }
}
