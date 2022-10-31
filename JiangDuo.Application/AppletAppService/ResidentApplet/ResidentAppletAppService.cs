using Furion.DynamicApiController;
using JiangDuo.Application.AppletAppService.ResidentApplet.Dtos;
using JiangDuo.Application.AppletService.ResidentApplet.Services;
using JiangDuo.Application.AppService.NewsService.Dto;
using JiangDuo.Application.AppService.PublicSentimentService.Dto;
using JiangDuo.Application.AppService.ResidentService.Dto;
using JiangDuo.Application.AppService.ServiceService.Dto;
using JiangDuo.Application.AppService.ServiceService.Dtos;
using JiangDuo.Application.Filters;
using JiangDuo.Core.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppletAppService.ResidentApplet;

/// <summary>
///居民小程序端
/// </summary>
[Route("api/[controller]")]
[ApiDescriptionSettings("Default", "小程序居民端接口")]
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
    /// 用户实名认证
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<string> AccountCertified(DtoAccountCertified model)
    {
        return await _residentAppletService.AccountCertified(model);
    }

    /// <summary>
    /// 修改/完善个人信息
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<string> UpdateAccountInfo(DtoUpdateAccountInfo model)
    {
        return await _residentAppletService.UpdateAccountInfo(model);
    }

    /// <summary>
    /// 获取新闻列表
    /// </summary>
    /// <param name="model">数据</param>
    /// <returns></returns>
    public PagedList<DtoNews> GetNewsList([FromQuery] DtoNewsQuery model)
    {
        return _residentAppletService.GetNewsList(model);
    }

    /// <summary>
    /// 根据id查询新闻详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<DtoNews> GetNewsById([FromQuery] long id)
    {
        return await _residentAppletService.GetNewsById(id);
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
    /// 取消参与服务(服务/活动)
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<string> CancelService(DtoCancelService model)
    {
        return await _residentAppletService.CancelService(model);
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
    /// 获取服务/活动预约记录(占用记录)
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpGet("GetServiceOccupancyList")]
    public Task<List<DtoParticipant>> GetServiceOccupancyList([FromQuery] DtoServiceSubscribeQuery model)
    {
        return _residentAppletService.GetServiceOccupancyList(model);
    }

    /// <summary>
    /// 确认占位服务
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("ConfirmOccupancyService")]
    public Task<DtoParticipant> ConfirmOccupancyService(DtoSubscribeService model)
    {
        return _residentAppletService.ConfirmOccupancyService(model);
    }

    /// <summary>
    /// 取消占位服务
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("CancelOccupancyService")]
    public Task<string> CancelOccupancyService(DtoSubscribeService model)
    {
        return _residentAppletService.CancelOccupancyService(model);
    }

    /// <summary>
    /// 预约服务（占位）提交
    /// </summary>
    /// <param name="modelList"></param>
    /// <returns></returns>
    [HttpPost("SubscribeService")]
    public Task<string> SubscribeService(List<DtoParticipant> modelList)
    {
        return _residentAppletService.SubscribeService(modelList);
    }

    /// <summary>
    /// 获取我的需求列表（码上说马上办）
    /// </summary>
    /// <param name="model">数据</param>
    /// <returns></returns>
    public PagedList<DtoPublicSentiment> GetMyPublicSentiment([FromQuery] DtoPublicSentimentQuery model)
    {
        return _residentAppletService.GetMyPublicSentiment(model);
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


    /// <summary>
    /// 获取指定日期服务的报名列表
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpGet("GetParticipantByDate")]
    public async Task<List<DtoJoinServiceResident>> GetParticipantByDate([FromQuery]DtoGetParticipant model)
    {
        return await _residentAppletService.GetParticipantByDate(model);
    }
}