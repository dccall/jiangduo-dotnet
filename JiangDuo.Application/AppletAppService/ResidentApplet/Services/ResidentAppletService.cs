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
using JiangDuo.Core.Base;
using JiangDuo.Application.AppService.PublicSentimentService.Services;
using JiangDuo.Application.AppService.PublicSentimentService.Dto;

namespace JiangDuo.Application.AppletAppService.ResidentApplet.Services
{
    public class ResidentAppletService : IResidentAppletService, ITransient
    {
        private readonly ILogger<ResidentAppletService> _logger;
        private readonly IRepository<Core.Models.Service> _serviceRepository;
        private readonly IRepository<Workorder> _workOrderRepository;
        private readonly IRepository<Participant> _participantRepository;
        private readonly WeiXinService _weiXinService;
        private readonly IRepository<Resident> _residentRepository;
        private readonly IWorkOrderService _workOrderService;
        private readonly IServiceService _serviceService;
        private readonly IPublicSentimentService _publicSentimentService;
        private readonly IRepository<Venuedevice> _venuedeviceRepository;
        private readonly IRepository<Official> _officialRepository;
        public ResidentAppletService(ILogger<ResidentAppletService> logger,
            IServiceService serviceService,
                 IRepository<Official> officialRepository,
             IRepository<Venuedevice> venuedeviceRepository,
            IPublicSentimentService publicSentimentService, IWorkOrderService workOrderService, IRepository<Resident> residentRepository, WeiXinService weiXinService, IRepository<Core.Models.Service> serviceRepository, IRepository<Workorder> workOrderRepository, IRepository<Participant> participantRepository)
        {
            _logger = logger;
            _serviceRepository = serviceRepository;
            _workOrderRepository = workOrderRepository;
            _participantRepository = participantRepository;
            _weiXinService = weiXinService;
            _residentRepository = residentRepository;
            _workOrderService = workOrderService;
            _publicSentimentService = publicSentimentService;
            _serviceService = serviceService;
            _venuedeviceRepository = venuedeviceRepository;
            _officialRepository = officialRepository;
        }

        ///// <summary>
        ///// 居民端登录
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        public async Task<string> Login(DtoResidentLogin model)
        {
            var result = await _weiXinService.WeiXinLogin(model.Code);
            var residentEntity = _residentRepository.Where(x => x.OpenId == result.OpenId).FirstOrDefault();
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
            accountModel.SelectAreaId = residentEntity.SelectAreaId ?? 0;
            var jwtTokenResult = JwtHelper.GetJwtToken(accountModel);
            return jwtTokenResult.AccessToken;
        }


        /// <summary>
        /// 获取账号信息
        /// </summary>
        /// <returns></returns>
        public async Task<DtoResident> GetAccountInfo()
        {
            var id = JwtHelper.GetAccountId();
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

            //这里进行信息校验（确保）


            //将模型数据映射给实体属性
            entity = model.Adapt(entity);
            entity.UpdatedTime = DateTime.Now;
            entity.Updater = JwtHelper.GetAccountId();
            _residentRepository.Update(entity);
            var count = await _residentRepository.SaveNowAsync();
            if (count > 0)
            {
                //完善信息后，刷新token，主要为了增加SelectAreaId选区。
                //防止从扫码注册后，信息不完善
                return JwtHelper.GetJwtToken(new AccountModel()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    SelectAreaId = entity.SelectAreaId ?? entity.SelectAreaId.Value,
                    Type = AccountType.Resident//账号类型居民
                }).AccessToken;
            }

            return "修改失败";
        }

        /// <summary>
        /// 查询已发布的服务
        /// </summary>
        /// <param name="model">数据</param>
        /// <returns></returns>
        public PagedList<DtoServiceInfo> GetPublishedList(DtoResidentServiceQuery model)
        {
            var userid = JwtHelper.GetAccountId();
            var currentDateTime=DateTime.Now;
            var query = _serviceRepository.Where(x => !x.IsDeleted);
            query = query.Where(x => x.Status == ServiceStatusEnum.Published);//只查询已发布的服务
            //只查询在活动时间 在范围内的
            query = query.Where(x => x.PlanStartTime>= currentDateTime&& currentDateTime<=x.PlanEndTime);
            query = query.Where(!string.IsNullOrEmpty(model.ServiceName), x => x.ServiceName.Contains(model.ServiceName));
            query = query.Where(model.ServiceType != null, x => x.ServiceType == model.ServiceType);

            //将数据映射到DtoServiceInfo中
            return query.OrderByDescending(s => s.CreatedTime).ProjectToType<DtoServiceInfo>().ToPagedList(model.PageIndex, model.PageSize);
        }


        /// <summary>
        /// 根据Id获取服务详情
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<DtoService> GetServiceById(long id)
        {
            var dto = await _serviceService.GetById(id);
            return dto;
        }
        /// <summary>
        /// 查询我的参与和预约的服务
        /// </summary>
        /// <param name="model">数据</param>
        /// <returns></returns>
        public PagedList<DtoService> GetMyServiceList(DtoMyServiceQuery model)
        {
            var id = JwtHelper.GetAccountId();
            var serviceIdList = _participantRepository.Where(x => !x.IsDeleted && x.ResidentId == id).Select(x => x.ServiceId).ToList();

            var query = from p in _participantRepository.Entities
                        join s in _serviceRepository.Entities on p.ServiceId equals s.Id
                        join official in _officialRepository.Entities on s.OfficialsId equals official.Id into result1
                        from so in result1.DefaultIfEmpty()
                        join venuedevice in _venuedeviceRepository.Entities on s.VenueDeviceId equals venuedevice.Id into result2
                        from sv in result2.DefaultIfEmpty()
                        where p.ResidentId == id
                        //根据报名时间排序、活动状态、活动开始时间
                        orderby p.CreatedTime descending, s.Status, s.PlanStartTime ascending 
                        select new DtoService
                        {
                            Id = s.Id,
                            Address = s.Address,
                            Attachments = s.Attachments,
                            AuditFindings = s.AuditFindings,
                            GroupOriented = s.GroupOriented,
                            CreatedTime = s.CreatedTime,
                            Creator = s.Creator,
                            IsDeleted = s.IsDeleted,
                            OfficialsId = s.OfficialsId,
                            OfficialsName = so.Name,
                            PlanNumber = s.PlanNumber,
                            PlanStartTime = s.PlanStartTime,
                            PlanEndTime = s.PlanEndTime,
                            Remarks = s.Remarks,
                            ServiceName = s.ServiceName,
                            ServiceType = s.ServiceType,
                            Status = s.Status,
                            UpdatedTime = s.UpdatedTime,
                            VenueDeviceId = s.VenueDeviceId,
                            VenueDeviceName = sv.Name,
                            ServiceClassifyId = s.ServiceClassifyId,
                            SelectAreaId = s.SelectAreaId,
                            Updater = s.Updater,
                            VillagesRange = s.VillagesRange
                        };
            return query.ToPagedList(model.PageIndex, model.PageSize);
        }
        /// <summary>
        /// 参与服务(服务/活动)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> JoinService(DtoJoinService model)
        {
            var service = await _serviceRepository.FindOrDefaultAsync(model.ServiceId);
            if (service == null)
            {
                throw Oops.Oh("服务不存在");
            }
            if (service.Status != ServiceStatusEnum.Published)
            {
                throw Oops.Oh("服务状态异常，无法参加");
            }
            var id = JwtHelper.GetAccountId();
            var exists = _participantRepository.Where(x => x.ResidentId == id && x.ServiceId == model.ServiceId).Any();
            if (exists)
            {
                throw Oops.Oh("已参与，请勿重复提交");
            }

            Participant entity = new Participant();
            entity.ServiceId = model.ServiceId;
            entity.ResidentId = JwtHelper.GetAccountId();
            entity.RegistTime = DateTime.Now;
            entity.CreatedTime = DateTime.Now;
            entity.Creator = JwtHelper.GetAccountId();
            await _participantRepository.InsertNowAsync(entity);
            return "已参与";
        }
        /// <summary>
        /// 取消参与服务(服务/活动)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> CancelService(DtoJoinService model)
        {
            var service = await _serviceRepository.FindOrDefaultAsync(model.ServiceId);
            if (service == null)
            {
                throw Oops.Oh("服务不存在");
            }
            if (service.Status != ServiceStatusEnum.Published)
            {
                throw Oops.Oh("服务状态异常，无法取消");
            }
            var id = JwtHelper.GetAccountId();
            var entity = _participantRepository.Where(x => x.ResidentId == id && x.ServiceId == model.ServiceId).FirstOrDefault();
            if (entity == null)
            {
                throw Oops.Oh("你没有参过该服务");
            }
            await _participantRepository.DeleteNowAsync(entity);
            return "已取消";
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
            entity.StartTime = model.StartTime;//预约开始时间
            entity.EndTime = model.EndTime;//预约结束时间
            entity.CreatedTime = DateTime.Now;
            entity.Creator = JwtHelper.GetAccountId();
            await _participantRepository.InsertNowAsync(entity);
            return "预约成功";
        }

        /// <summary>
        /// 获取我的需求列表（码上说马上办）
        /// </summary>
        /// <param name="model">数据</param>
        /// <returns></returns>
        public PagedList<DtoPublicSentiment> GetMyPublicSentiment(DtoPublicSentimentQuery model)
        {
            var id = JwtHelper.GetAccountId();
            model.ResidentId = id;//查询我的需求
            return _publicSentimentService.GetList(model);

        }
        /// <summary>
        /// 根据id查询详情
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public async Task<DtoPublicSentiment> GetPublicSentimentDetail(long id)
        {
            return await _publicSentimentService.GetById(id);
        }

        /// <summary>
        /// 新增公共需求（码上说马上办）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> AddPublicSentiment(DtoPublicSentimentForm model)
        {
            var account = JwtHelper.GetAccountInfo();
            model.ResidentId = account.Id;  //居民是自己
            var count = await _publicSentimentService.Insert(model);
            if (count > 0)
            {
                return "提交成功";
            }
            return "提交失败";
        }


    }
}
