using Furion.DynamicApiController;
using JiangDuo.Application.AppletAppService.OfficialApplet.Dtos;
using JiangDuo.Application.AppletService.OfficialApplet.Services;
using JiangDuo.Application.AppService.WorkorderService.Dto;
using JiangDuo.Application.AppService.WorkOrderService.Dto;
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

    /// <summary>
    /// 获取我的工单
    /// </summary>
    /// <param name="model">数据</param>
    /// <returns></returns>
    public PagedList<DtoWorkOrder> GetMyWorkOrderList([FromQuery] DtoMyWorkOrderQuery model)
    {
        return  _officialAppletService.GetMyWorkOrderList(model);
    }


    /// <summary>
    /// 申请服务(工单)
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<string> ApplyForServices(DtoWorkOrderForm model)
    {
        return await _officialAppletService.ApplyForServices(model);
    }


    /// <summary>
    /// 人大登录（微信code登录）
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<string> LoginWeiXin(DtoOfficialLogin model)
    {
        return await _officialAppletService.LoginWeiXin(model);
    }

    /// <summary>
    /// 人大登录(手机号登录)
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<string> LoginByPhone(DtoOfficialLogin model)
    {
        return await _officialAppletService.LoginByPhone(model);
    }



    /// <summary>
    /// 获取验证码
    /// </summary>
    /// <param name="phone"></param>
    /// <returns></returns>
    public async Task<bool> GetVerifyCode([FromQuery]string phone)
    {
        return await _officialAppletService.GetVerifyCode(phone);
    }


}
