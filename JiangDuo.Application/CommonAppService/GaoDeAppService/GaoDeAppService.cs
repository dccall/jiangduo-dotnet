using Furion.DynamicApiController;
using JiangDuo.Core.Services.GaoDe;
using JiangDuo.Core.Services.GaoDe.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JiangDuo.Application.CommonAppService.GaoAppDeService;

/// <summary>
/// 新闻管理
/// </summary>
[Route("api/[controller]")]
[ApiDescriptionSettings("Default", "高德地图接口")]
public class GaoDeAppService : IDynamicApiController
{
    private readonly GaoDeService _gaoDeService;

    public GaoDeAppService(GaoDeService gaoDeService)
    {
        _gaoDeService = gaoDeService;
    }

    /// <summary>
    /// 根据关键词查询地点
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<List<DtoAddressInfo>> SearchLocation(SearchLocationRequestModel model)
    {
        return await _gaoDeService.SearchLocation(model);
    }
}