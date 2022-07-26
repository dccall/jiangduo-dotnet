using Furion.DynamicApiController;
using JiangDuo.Application.AppletAppService.ResidentApplet.Dtos;
using JiangDuo.Application.AppletService.ResidentApplet.Services;
using JiangDuo.Application.AppService.ResidentService.Dto;
using JiangDuo.Application.AppService.ServiceService.Dto;
using JiangDuo.Application.AppService.WorkorderService.Dto;
using JiangDuo.Application.AppService.WorkOrderService.Dto;
using JiangDuo.Application.Filters;
using JiangDuo.Core.Base;
using JiangDuo.Core.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppletAppService.ResidentApplet;

/// <summary>
///居民小程序端
/// </summary>
[Route("api/[controller]")]
[ApiDescriptionSettings("Default", "居民小程序端接口")]
[AppletRoleFilter(AccountType.Resident)]//只允许居民账号类型访问
public class ResidentAppletAppService : IDynamicApiController
{
 
    private readonly IResidentAppletService _residentAppletService;
    public ResidentAppletAppService(IResidentAppletService residentAppletService)
    {
        _residentAppletService = residentAppletService;
    }

    /// <summary>
    /// 居民端登录
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [AllowAnonymous]
    public async Task<string> Login(DtoResidentLogin model)
    {
       return await _residentAppletService.Login(model);
    }
    /// <summary>
    /// 获取账号信息
    /// </summary>
    /// <returns></returns>
    public async Task<DtoResident> GetAccountInfo()
    {
        return await _residentAppletService.GetAccountInfo();
    }

    /// <summary>
    /// 修改/完善个人信息
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<string> UpdateAccountInfo(DtoResidentForm model)
    {
        return await _residentAppletService.UpdateAccountInfo(model);
    }
    /// <summary>
    /// 查询已发布的服务
    /// </summary>
    /// <param name="model">数据</param>
    /// <returns></returns>
    public PagedList<DtoServiceInfo> GetPublishedList([FromQuery] DtoResidentServiceQuery model)
    {
        return _residentAppletService.GetPublishedList(model);
    }

    /// <summary>
    /// 参与服务
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<string> JoinService(DtoJoinService model)
    {
        return await _residentAppletService.JoinService(model);
    }
    /// <summary>
    /// 根据Id获取服务详情
    /// </summary>
    /// <param name="id">编号</param>
    /// <returns></returns>
    public async Task<DtoServiceInfo> GetServiceById([FromQuery] long id)
    {
        return await _residentAppletService.GetServiceById(id);
    }

    /// <summary>
    /// 查询我的参与和预约的服务
    /// </summary>
    /// <param name="model">数据</param>
    /// <returns></returns>
    public PagedList<DtoServiceInfo> GetMyServiceList([FromQuery] DtoMyServiceQuery model)
    {
        return _residentAppletService.GetMyServiceList(model);
    }

    /// <summary>
    /// 预约服务
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<string> SubscribeService(DtoSubscribeService model)
    {
        return await _residentAppletService.SubscribeService(model);
    }


    /// <summary>
    /// 获取我的工单
    /// </summary>
    /// <param name="model">数据</param>
    /// <returns></returns>
    public PagedList<DtoWorkOrder> GetMyWorkOrderList([FromQuery] BaseRequest model)
    {
        return  _residentAppletService.GetMyWorkOrderList(model);
    }

    /// <summary>
    /// 申请服务(工单)
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<string> ApplyForServices(DtoWorkOrderForm model)
    {
        return await _residentAppletService.ApplyForServices(model);
    }

    /// <summary>
    /// 码上说马上办(工单)
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<string> OnlineLettersServices(DtoWorkOrderForm model)
    {
        return await _residentAppletService.OnlineLettersServices(model);
    }

    /// <summary>
    /// 查看工单详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<DtoWorkOrder> GetWorkOrderDetail([FromQuery]long id)
    {
        return await _residentAppletService.GetWorkOrderDetail(id);
    }

}
