using Furion.DynamicApiController;
using JiangDuo.Application.AppService.NewsService.Dto;
using JiangDuo.Application.AppService.NewsService.Services;
using JiangDuo.Application.AppService.OfficialAppletService.Services;
using JiangDuo.Application.AppService.SelectAreaService.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.NewsService;

/// <summary>
///人大小程序端
/// </summary>
[Route("api/[controller]")]
[ApiDescriptionSettings("Default", "人大小程序端接口")]
public class OfficialAppletService : IDynamicApiController
{
 
    private readonly IOfficialAppletService _officialAppletService;
    public OfficialAppletService(IOfficialAppletService officialAppletService)
    {
        _officialAppletService = officialAppletService;
    }


	

}
