using JiangDuo.Application.AppletAppService.ResidentApplet.Dtos;
using JiangDuo.Application.AppService.ResidentService.Dto;
using JiangDuo.Application.AppService.ServiceService.Dto;
using JiangDuo.Application.AppService.WorkorderService.Dto;
using JiangDuo.Application.AppService.WorkOrderService.Dto;
using JiangDuo.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppletService.ResidentApplet.Services
{
    public interface IResidentAppletService
    {
        /// <summary>
        /// 居民端登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public  Task<string> Login(DtoResidentLogin model);
        /// <summary>
        /// 获取账号信息
        /// </summary>
        /// <returns></returns>
        public  Task<DtoResident> GetAccountInfo();

        /// <summary>
        /// 修改/完善个人信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<string> UpdateAccountInfo(DtoResidentForm model);
        /// <summary>
        /// 查询已发布的服务
        /// </summary>
        /// <param name="model">数据</param>
        /// <returns></returns>
        public PagedList<DtoServiceInfo> GetPublishedList(DtoResidentServiceQuery model);

        /// <summary>
        /// 参与服务
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public  Task<string> JoinService(DtoJoinService model);

        /// <summary>
        /// 根据Id获取服务详情
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public  Task<DtoServiceInfo> GetServiceById(long id);

        /// <summary>
        /// 查询我的参与和预约的服务
        /// </summary>
        /// <param name="model">数据</param>
        /// <returns></returns>
        public PagedList<DtoServiceInfo> GetMyServiceList(DtoMyServiceQuery model);

        /// <summary>
        /// 预约服务
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<string> SubscribeService(DtoSubscribeService model);

        /// <summary>
        /// 获取我的工单
        /// </summary>
        /// <param name="model">数据</param>
        /// <returns></returns>
        public PagedList<DtoWorkOrder> GetMyWorkOrderList(BaseRequest model);

        /// <summary>
        /// 申请服务(工单)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public  Task<string> ApplyForServices(DtoWorkOrderForm model);

        /// <summary>
        /// 码上说马上办(工单)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public  Task<string> OnlineLettersServices(DtoWorkOrderForm model);

        /// <summary>
        /// 查看工单详情
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public  Task<DtoWorkOrder> GetWorkOrderDetail(long id);
    }
}
