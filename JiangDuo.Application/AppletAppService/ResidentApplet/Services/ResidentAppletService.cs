using JiangDuo.Core.Models;
using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Mapster;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yitter.IdGenerator;
using JiangDuo.Core.Utils;
using Furion.FriendlyException;
using JiangDuo.Application.AppletService.ResidentApplet.Services;
using JiangDuo.Application.AppService.ServiceService.Dto;
using JiangDuo.Core.Enums;
using JiangDuo.Application.AppletAppService.ResidentApplet.Dtos;
using JiangDuo.Core.Services;
using Furion;
using JiangDuo.Application.AppService.ResidentService.Dto;
using JiangDuo.Application.AppService.WorkOrderService.Dto;
using JiangDuo.Application.AppService.WorkOrderService.Services;
using JiangDuo.Application.AppService.WorkorderService.Dto;
using JiangDuo.Application.AppService.ServiceService.Services;

namespace JiangDuo.Application.AppletAppService.ResidentApplet.Services
{
    public class ResidentAppletService:IResidentAppletService, ITransient
    {
        private readonly ILogger<ResidentAppletService> _logger;
        private readonly IRepository<Service> _serviceRepository;
        private readonly IRepository<Workorder> _workOrderRepository;
        private readonly IRepository<Participant> _participantRepository;
        private readonly WeiXinService _weiXinService;
        private readonly IRepository<Resident> _residentRepository;
        private readonly IWorkOrderService _workOrderService;
        private readonly IServiceService _serviceService;

        public ResidentAppletService(ILogger<ResidentAppletService> logger, IWorkOrderService workOrderService, IRepository<Resident> residentRepository, WeiXinService weiXinService, IRepository<Service> serviceRepository, IRepository<Workorder> workOrderRepository, IRepository<Participant> participantRepository)
        {
            _logger = logger;
            _serviceRepository = serviceRepository;
            _workOrderRepository = workOrderRepository;
            _participantRepository = participantRepository;
            _weiXinService = weiXinService;
            _residentRepository = residentRepository;
            _workOrderService = workOrderService;
        }

        /// <summary>
        /// 居民端登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> Login(DtoResidentLogin model)
        {
            var result = await _weiXinService.WeiXinLogin(model.Code);
            var residentEntity =  _residentRepository.Where(x => x.OpenId == result.OpenId).FirstOrDefault();
            if (residentEntity == null)
            {
                residentEntity = new Resident();
                residentEntity.Id = YitIdHelper.NextId();
                residentEntity.CreatedTime = DateTime.Now;
                residentEntity.Creator = JwtHelper.GetAccountId();
                _residentRepository.InsertNow(residentEntity);
            }
          
            AccountModel accountModel = new AccountModel();
            accountModel.Id = residentEntity.Id;
            accountModel.Name = residentEntity.Name;
            accountModel.Type = AccountType.Resident;
            var jwtTokenResult = JwtHelper.GetJwtToken(accountModel);
            return jwtTokenResult.AccessToken;
        }


        /// <summary>
        /// 获取账号信息
        /// </summary>
        /// <returns></returns>
        public async Task<DtoResident> GetAccountInfo()
        {
            var id= JwtHelper.GetAccountId();
            var entity = await _residentRepository.FindOrDefaultAsync(id);
            var dto = entity.Adapt<DtoResident>();
            return dto;
        }
        /// <summary>
        /// 修改/完善个人信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> UpdateAccountInfo(DtoResidentForm model)
        {
            //先根据id查询实体
            var entity = _residentRepository.FindOrDefault(model.Id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            //将模型数据映射给实体属性
            entity = model.Adapt(entity);
            entity.UpdatedTime = DateTime.Now;
            entity.Updater = JwtHelper.GetAccountId();
            _residentRepository.Update(entity);
             var count=  await _residentRepository.SaveNowAsync();
            if (count > 0)
            {
                return "保存成功";
            }

            return "保存失败";
        }

        /// <summary>
        /// 查询已发布的服务
        /// </summary>
        /// <param name="model">数据</param>
        /// <returns></returns>
        public PagedList<DtoServiceInfo> GetPublishedList(DtoResidentServiceQuery model)
        {
            var userid = JwtHelper.GetAccountId();
            var query = _serviceRepository.Where(x => !x.IsDeleted);
            query = query.Where(x =>x.Status== ServiceStatusEnum.Published);//只查询已发布的服务
            query = query.Where(!string.IsNullOrEmpty(model.ServiceName), x => x.ServiceName.Contains(model.ServiceName));
            query = query.Where(model.ServiceTypeId!=null, x => x.ServiceTypeId== model.ServiceTypeId);

            //将数据映射到DtoServiceInfo中
            return query.OrderByDescending(s => s.CreatedTime).ProjectToType<DtoServiceInfo>().ToPagedList(model.PageIndex, model.PageSize);
        }


        /// <summary>
        /// 根据Id获取服务详情
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<DtoServiceInfo> GetServiceById(long id)
        {
            var entity = await _serviceRepository.FindOrDefaultAsync(id);

            var dto = entity.Adapt<DtoServiceInfo>();

        
            return dto;
        }


        /// <summary>
        /// 查询我的参与和预约的服务
        /// </summary>
        /// <param name="model">数据</param>
        /// <returns></returns>
        public PagedList<DtoServiceInfo> GetMyServiceList(DtoMyServiceQuery model)
        {
            var id = JwtHelper.GetAccountId();
            var serviceIdList= _participantRepository.Where(x => !x.IsDeleted&&x.ResidentId== id).Select(x=>x.ServiceId).ToList();
            if (serviceIdList.Any())
            {
                //只查询自己的参与的所有服务/活动
                var query = _serviceRepository.Where(x => !x.IsDeleted);
                query = query.Where(x =>serviceIdList.Contains(x.Id));
                return query.OrderByDescending(s => s.CreatedTime).ProjectToType<DtoServiceInfo>().ToPagedList(model.PageIndex, model.PageSize);
            }
            return new PagedList<DtoServiceInfo>() { PageIndex= model.PageIndex, PageSize = model.PageSize };

        }
        /// <summary>
        /// 参与服务(服务/活动)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> JoinService(DtoJoinService model)
        {
            var service = await  _serviceRepository.FindOrDefaultAsync(model.ServiceId);
            if (service == null)
            {
                throw Oops.Oh("服务不存在");
            }
            if (service.Status != ServiceStatusEnum.Published)
            {
                throw Oops.Oh("服务状态异常，无法参加");
            }
            var id = JwtHelper.GetAccountId();
            var exists=  _participantRepository.Where(x => x.ResidentId == id && x.ServiceId == model.ServiceId).Any();
            if (exists)
            {
                throw Oops.Oh("已参与，请勿重复提交");
            }

            Participant entity = new Participant();
            entity.ServiceId = model.ServiceId;
            entity.ResidentId = JwtHelper.GetAccountId();
            entity.CreatedTime = DateTime.Now;
            entity.Creator = JwtHelper.GetAccountId();
            await _participantRepository.InsertNowAsync(entity);
            return "已参与";
        }
        /// <summary>
        /// 预约服务(服务/活动)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> SubscribeService(DtoSubscribeService model)
        {
            var service = await _serviceRepository.FindOrDefaultAsync(model.ServiceId);
            if (service == null)
            {
                throw Oops.Oh("服务不存在");
            }
            if (service.Status != ServiceStatusEnum.Published)
            {
                throw Oops.Oh("服务状态异常，无法预约");
            }
            var id = JwtHelper.GetAccountId();

            var exists = _participantRepository.Where(x => x.ResidentId == id && x.ServiceId == model.ServiceId).Any();
            if (exists)
            {
                throw Oops.Oh("已有预约，请勿重复提交");
            }
            //时间段校验
            //var exists2 = _participantRepository.Where(x => !(model.StartTime>= x.EndTime || model.EndTime <=x.StartTime)).Any();
            //if (exists2)
            //{
            //    throw Oops.Oh("预约失败，当前时间段已有预约");
            //}
            //时间段校验
            var exists2 = _participantRepository.Where(x => model.StartTime == x.StartTime && model.EndTime == x.EndTime).Any();
            if (exists2)
            {
                throw Oops.Oh("预约失败，当前时间段已有预约");
            }

            Participant entity = new Participant();
            entity.ServiceId = model.ServiceId;
            entity.ResidentId = JwtHelper.GetAccountId();
            entity.StartTime =model.StartTime;//预约开始时间
            entity.EndTime =model.EndTime;//预约结束时间
            entity.CreatedTime = DateTime.Now;
            entity.Creator = JwtHelper.GetAccountId();
            await _participantRepository.InsertNowAsync(entity);
            return "预约成功";
        }

        /// <summary>
        /// 申请服务(工单)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> ApplyForServices(DtoWorkOrderForm model)
        {
            var count= await _workOrderService.Insert(model);
            if (count > 0)
            {
                return "申请已提交";
            }
            return "申请提交失败";
        }
        /// <summary>
        /// 码上说马上办(工单)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> OnlineLettersServices(DtoWorkOrderForm model)
        {
            var count = await _workOrderService.Insert(model);
            if (count > 0)
            {
                return "工单已提交";
            }
            return "工单提交失败";
        }
        /// <summary>
        /// 查看工单详情
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<DtoWorkOrder> GetWorkOrderDetail(long id)
        {
            return await _workOrderService.GetById(id);
        }
    }
}
