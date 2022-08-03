using Furion.DynamicApiController;
using JiangDuo.Application.AppletAppService.ResidentApplet.Dtos;
using JiangDuo.Application.AppletService.ResidentApplet.Services;
using JiangDuo.Application.AppService.PublicSentimentService.Dto;
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

    ///// <summary>
    ///// 居民端登录
    ///// </summary>
    ///// <param name="model"></param>
    ///// <returns></returns>
    //[AllowAnonymous]
    //public async Task<string> Login(DtoResidentLogin model)
    //{
    //   return await _residentAppletService.Login(model);
    //}
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
    public async Task<DtoService> GetServiceById([FromQuery] long id)
    {
        return await _residentAppletService.GetServiceById(id);
    }

    /// <summary>
    /// 查询我的参与和预约的服务
    /// </summary>
    /// <param name="model">数据</param>
    /// <returns></returns>
    public PagedList<DtoService> GetMyServiceList([FromQuery] DtoMyServiceQuery model)
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
    /// 获取我的需求列表（码上说马上办）
    /// </summary>
    /// <param name="model">数据</param>
    /// <returns></returns>
    public PagedList<DtoPublicSentiment> GetMyPublicSentiment(DtoPublicSentimentQuery model)
    {
        return  _residentAppletService.GetMyPublicSentiment(model);
    }

    /// <summary>
    /// 根据id查询需求详情
    /// </summary>
    /// <param name="id">id</param>
    /// <returns></returns>
    public Task<DtoPublicSentiment> GetPublicSentimentDetail([FromQuery] long id)
    {
        return _residentAppletService.GetPublicSentimentDetail(id);
    }
    /// <summary>
    /// 新增公共需求（码上说马上办）
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<string> AddPublicSentiment(DtoPublicSentimentForm model)
    {
        return await _residentAppletService.AddPublicSentiment(model);
    }
  

}
