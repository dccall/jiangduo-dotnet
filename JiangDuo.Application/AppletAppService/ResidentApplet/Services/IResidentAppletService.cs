using JiangDuo.Application.AppletAppService.ResidentApplet.Dtos;
using JiangDuo.Application.AppService.NewsService.Dto;
using JiangDuo.Application.AppService.PublicSentimentService.Dto;
using JiangDuo.Application.AppService.ResidentService.Dto;
using JiangDuo.Application.AppService.ServiceService.Dto;
using JiangDuo.Application.AppService.ServiceService.Dtos;
using System.Collections.Generic;
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
        public Task<string> Login(DtoResidentLogin model);

        /// <summary>
        /// 获取账号信息
        /// </summary>
        /// <returns></returns>
        public Task<DtoResident> GetAccountInfo();

        /// <summary>
        /// 用户实名认证
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<string> AccountCertified(DtoAccountCertified model);

        /// <summary>
        /// 修改/完善个人信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<string> UpdateAccountInfo(DtoUpdateAccountInfo model);

        /// <summary>
        /// 获取新闻列表
        /// </summary>
        /// <param name="model">数据</param>
        /// <returns></returns>
        public PagedList<DtoNews> GetNewsList(DtoNewsQuery model);

        /// <summary>
        /// 根据id查询新闻详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<DtoNews> GetNewsById(long id);

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
        public Task<string> JoinService(DtoJoinService model);

        /// <summary>
        /// 取消参与服务(服务/活动)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<string> CancelService(DtoCancelService model);

        /// <summary>
        /// 根据Id获取服务详情
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public Task<DtoServiceInfo> GetServiceById(long id);

        /// <summary>
        /// 查询我的参与和预约的服务
        /// </summary>
        /// <param name="model">数据</param>
        /// <returns></returns>
        public PagedList<DtoServiceInfo> GetMyServiceList(DtoMyServiceQuery model);

        /// <summary>
        /// 获取服务/活动预约记录(占用记录)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<List<DtoParticipant>> GetServiceOccupancyList(DtoServiceSubscribeQuery model);

        /// <summary>
        /// 确认占位服务
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<DtoParticipant> ConfirmOccupancyService(DtoSubscribeService model);

        /// <summary>
        /// 取消占位服务
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<string> CancelOccupancyService(DtoSubscribeService model);

        /// <summary>
        /// 预约服务（占位）提交
        /// </summary>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public Task<string> SubscribeService(List<DtoParticipant> modelList);

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
        public Task<DtoPublicSentiment> GetPublicSentimentDetail(long id);

        /// <summary>
        /// 新增需求（码上说马上办）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<string> AddPublicSentiment(DtoPublicSentimentForm model);
    }
}