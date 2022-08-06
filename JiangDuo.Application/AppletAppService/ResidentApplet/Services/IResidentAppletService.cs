using JiangDuo.Application.AppletAppService.ResidentApplet.Dtos;
using JiangDuo.Application.AppService.PublicSentimentService.Dto;
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
        /// 取消参与服务(服务/活动)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public  Task<string> CancelService(DtoJoinService model);

        /// <summary>
        /// 根据Id获取服务详情
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public  Task<DtoService> GetServiceById(long id);

        /// <summary>
        /// 查询我的参与和预约的服务
        /// </summary>
        /// <param name="model">数据</param>
        /// <returns></returns>
        public PagedList<DtoService> GetMyServiceList(DtoMyServiceQuery model);

        /// <summary>
        /// 预约服务
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<string> SubscribeService(DtoSubscribeService model);


        /// <summary>
        /// 获取我的需求列表（码上说马上办）
        /// </summary>
        /// <param name="model">数据</param>
        /// <returns></returns>
        public PagedList<DtoPublicSentiment> GetMyPublicSentiment(DtoPublicSentimentQuery model);

        /// <summary>
        /// 根据id查询详情
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public  Task<DtoPublicSentiment> GetPublicSentimentDetail(long id);
        /// <summary>
        /// 新增需求（码上说马上办）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<string> AddPublicSentiment(DtoPublicSentimentForm model);
    }
}
