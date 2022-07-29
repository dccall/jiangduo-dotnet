using Furion.DynamicApiController;
using JiangDuo.Application.AppletAppService.OfficialApplet.Dtos;
using JiangDuo.Application.AppletService.OfficialApplet.Services;
using JiangDuo.Application.AppService.ReserveService.Dto;
using JiangDuo.Application.AppService.ServiceService.Dto;
using JiangDuo.Application.AppService.WorkorderService.Dto;
using JiangDuo.Application.AppService.WorkOrderService.Dto;
using Microsoft.AspNetCore.Authorization;
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
    /// 人大登录（微信code登录）
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [AllowAnonymous]
    public async Task<string> LoginWeiXin(DtoOfficialLogin model)
    {
        return await _officialAppletService.LoginWeiXin(model);
    }

    /// <summary>
    /// 人大登录(手机号登录)
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [AllowAnonymous]
    public async Task<string> LoginByPhone(DtoOfficialLogin model)
    {
        return await _officialAppletService.LoginByPhone(model);
    }
    /// <summary>
    /// 获取验证码
    /// </summary>
    /// <param name="phone"></param>
    /// <returns></returns>
    [HttpGet("GetVerifyCode")]
    public async Task<bool> GetVerifyCode([FromQuery]string phone)
    {
        return await _officialAppletService.GetVerifyCode(phone);
    }
    /// <summary>
    /// 我的服务列表(一老一小)
    /// </summary>
    /// <param name="model">数据</param>
    /// <returns></returns>
    [HttpGet("GetMyServices")]
    public  PagedList<DtoService> GetMyServices([FromQuery] DtoServiceQuery model)
    {
        return  _officialAppletService.GetMyServices(model);
    }
    /// <summary>
    /// 服务详情(一老一小)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("GetServicesDetail")]
    public Task<DtoService> GetServicesDetail([FromQuery] long id)
    {
        return _officialAppletService.GetServicesDetail(id);
    }
    /// <summary>
    /// 创建服务(一老一小）
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("AddServices")]
    public async Task<string> AddServices(DtoServiceForm model)
    {
        return await _officialAppletService.AddServices(model);
    }
    /// <summary>
    /// 删除服务(一老一小)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("DeleteServices")]
    public async Task<string> DeleteServices([FromQuery] long id)
    {
        return await _officialAppletService.DeleteServices(id);
    }
    /// <summary>
    /// 获取我的预约（有事好商量）
    /// </summary>
    /// <param name="model">数据</param>
    /// <returns></returns>
    [HttpGet("GetMyReserves")]
    public PagedList<DtoReserve> GetMyReserves([FromQuery] DtoReserveQuery model)
    {
        return  _officialAppletService.GetMyReserves(model);
    }
    /// <summary>
    /// 获取预约详情(有事好商量)
    /// </summary>
    /// <param name="id">id</param>
    /// <returns></returns>
    [HttpGet("GetReserveDetail")]
    public async Task<DtoReserve> GetReserveDetail([FromQuery] long id)
    {
        return await _officialAppletService.GetReserveDetail(id);
    }
    /// <summary>
    /// 添加预约(有事好商量)
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("AddReserve")]
    public async Task<string> AddReserve(DtoReserveForm model)
    {
        return await _officialAppletService.AddReserve(model);
    }
    /// <summary>
    /// 删除预约（有事好商量）
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("DeleteReserve")]
    public async Task<string> DeleteReserve([FromQuery] long id)
    {
        return await _officialAppletService.DeleteReserve(id);
    }
    /// <summary>
    /// 获取我的工单
    /// </summary>
    /// <param name="model">数据</param>
    /// <returns></returns>
    [HttpGet("GetMyWorkOrderList")]
    public PagedList<DtoWorkOrder> GetMyWorkOrderList([FromQuery] DtoMyWorkOrderQuery model)
    {
        return _officialAppletService.GetMyWorkOrderList(model);
    }

    /// <summary>
    /// 工单完成
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("WorkOrderCompleted")]
    public async Task<string> WorkOrderCompleted(DtoWorkOrderCompletedHandel model)
    {
        return await _officialAppletService.WorkOrderCompleted(model);
    }
}
