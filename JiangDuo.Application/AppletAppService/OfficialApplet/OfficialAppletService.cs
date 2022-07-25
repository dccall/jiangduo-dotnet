using Furion.DynamicApiController;
using JiangDuo.Application.AppletService.OfficialApplet.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppletAppService.OfficialApplet;

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
