using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Furion.FriendlyException;
using JiangDuo.Application.AppletAppService.ResidentApplet.Dtos;
using JiangDuo.Application.AppletService.ResidentApplet.Services;
using JiangDuo.Application.AppService.NewsService.Dto;
using JiangDuo.Application.AppService.NewsService.Services;
using JiangDuo.Application.AppService.PublicSentimentService.Dto;
using JiangDuo.Application.AppService.PublicSentimentService.Services;
using JiangDuo.Application.AppService.ResidentService.Dto;
using JiangDuo.Application.AppService.ResidentService.Services;
using JiangDuo.Application.AppService.ServiceService.Dto;
using JiangDuo.Application.AppService.ServiceService.Dtos;
using JiangDuo.Application.AppService.ServiceService.Services;
using JiangDuo.Application.AppService.VenuedeviceService.Services;
using JiangDuo.Application.AppService.WorkOrderService.Services;
using JiangDuo.Core.Enums;
using JiangDuo.Core.Models;
using JiangDuo.Core.Services;
using JiangDuo.Core.Utils;
using Mapster;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yitter.IdGenerator;

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
        private readonly INewsService _newService;
        private readonly IResidentService _residentService;
        private readonly IRepository<Village> _villageRepository;
        private readonly IRepository<SelectArea> _selectAreaRepository;
        private readonly IVenuedeviceService _venuedeviceService;

        private readonly IRepository<SysUploadFile> _uploadFileRepository;

        public ResidentAppletService(ILogger<ResidentAppletService> logger,
            IServiceService serviceService,
                 IRepository<Official> officialRepository,
             IRepository<Venuedevice> venuedeviceRepository,
             IVenuedeviceService venuedeviceService,
             IRepository<SysUploadFile> uploadFileRepository,
             INewsService newService,
             IRepository<Village> villageRepository,
             IResidentService residentService,
             IRepository<SelectArea> selectAreaRepository,
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
            _newService = newService;
            _villageRepository = villageRepository;
            _selectAreaRepository = selectAreaRepository;
            _residentService = residentService;
            _uploadFileRepository = uploadFileRepository;
            _venuedeviceService = venuedeviceService;
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

            var dto = await _residentService.GetById(id);
            return dto;
        }

        /// <summary>
        /// 用户实名认证
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> AccountCertified(DtoAccountCertified model)
        {
            var id = JwtHelper.GetAccountId();
            //先根据id查询实体
            var entity = _residentRepository.FindOrDefault(id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            //这里进行信息校验（确保）
            var cardInfo = IdCardHelper.GetBirthdayAgeSex(model.Idnumber);
            if (cardInfo == null)
            {
                throw Oops.Oh("身份证号码不正确");
            }
            //将模型数据映射给实体属性
            entity = model.Adapt(entity);
            entity.UpdatedTime = DateTime.Now;
            entity.Updater = JwtHelper.GetAccountId();
            entity.Age = cardInfo.Age;
            entity.Sex = (SexEnum)cardInfo.Sex;
            entity.Birthday = DateTime.Parse(cardInfo.Birthday);
            //状态改为已认证（暂时只做简单认证，只要调用了这个接口就已认证）
            entity.Status = ResidentStatus.Certified;
            if (entity.VillageId != null)
            {
                var village = _villageRepository.FindOrDefault(entity.VillageId);
                entity.SelectAreaId = village?.SelectAreaId;
            }
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
                    SelectAreaId = entity.SelectAreaId ?? 0,
                    Type = AccountType.Resident//账号类型居民
                }).AccessToken;
            }
            return "修改失败";
        }

        /// <summary>
        /// 用户信息修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> UpdateAccountInfo(DtoUpdateAccountInfo model)
        {
            var id = JwtHelper.GetAccountId();
            //先根据id查询实体
            var entity = _residentRepository.FindOrDefault(id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            //将模型数据映射给实体属性
            entity = model.Adapt(entity);
            entity.UpdatedTime = DateTime.Now;
            entity.Updater = JwtHelper.GetAccountId();
            if (entity.VillageId != null)
            {
                var village = _villageRepository.FindOrDefault(entity.VillageId);
                entity.SelectAreaId = village?.SelectAreaId;
            }
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
                    SelectAreaId = entity.SelectAreaId ?? 0,
                    Type = AccountType.Resident//账号类型居民
                }).AccessToken;
            }
            return "修改失败";
        }

        /// <summary>
        /// 获取新闻列表
        /// </summary>
        /// <param name="model">数据</param>
        /// <returns></returns>
        public PagedList<DtoNews> GetNewsList(DtoNewsQuery model)
        {
            //只查询已发布的新闻
            model.Status = NewsStatus.Publish;
            return _newService.GetList(model);
        }

        /// <summary>
        /// 根据id查询新闻详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<DtoNews> GetNewsById(long id)
        {
            return await _newService.GetById(id);
        }

        /// <summary>
        /// 查询已发布的服务
        /// </summary>
        /// <param name="model">数据</param>
        /// <returns></returns>
        public PagedList<DtoServiceInfo> GetPublishedList(DtoResidentServiceQuery model)
        {
            var userid = JwtHelper.GetAccountId();
            var currentDateTime = DateTime.Now;
            var query = _serviceRepository.Where(x => !x.IsDeleted);
            query = query.Where(x => x.Status == ServiceStatusEnum.Published);//只查询已发布的服务
            //只查询在活动时间 在范围内的
            query = query.Where(x => currentDateTime <= x.PlanEndTime);
            query = query.Where(!string.IsNullOrEmpty(model.ServiceName), x => x.ServiceName.Contains(model.ServiceName));
            query = query.Where(model.ServiceType != null, x => x.ServiceType == model.ServiceType);

            var query2 = from s in query
                         join official in _officialRepository.Entities on s.OfficialsId equals official.Id into result2
                         from so in result2.DefaultIfEmpty()
                         join venuedevice in _venuedeviceRepository.Entities on s.VenueDeviceId equals venuedevice.Id into result3
                         from sv in result3.DefaultIfEmpty()
                         orderby s.CreatedTime descending
                         select new DtoServiceInfo
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
                             VillagesRange = s.VillagesRange,
                             IsSignUp = _participantRepository.Entities.Where(x => x.Status == ParticipantStatus.Normal && x.ResidentId == userid && x.ServiceId == s.Id).Any()
                         };
            return query2.ToPagedList(model.PageIndex, model.PageSize);
        }

        /// <summary>
        /// 根据Id获取服务详情
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<DtoServiceInfo> GetServiceById(long id)
        {
            var userid = JwtHelper.GetAccountId();
            var query = from s in _serviceRepository.Entities.Where(x => x.Id == id)
                        join official in _officialRepository.Entities on s.OfficialsId equals official.Id into result2
                        from so in result2.DefaultIfEmpty()
                        join venuedevice in _venuedeviceRepository.Entities on s.VenueDeviceId equals venuedevice.Id into result3
                        from sv in result3.DefaultIfEmpty()
                        orderby s.CreatedTime descending
                        select new DtoServiceInfo
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
                            VillagesRange = s.VillagesRange,
                            IsSignUp = _participantRepository.Entities.Where(x => x.ResidentId == userid && x.Status == ParticipantStatus.Normal && x.ServiceId == s.Id).Any()
                        };
            var dto = query.FirstOrDefault();
            var result = _participantRepository
              .Where(x => !x.IsDeleted && x.Status == ParticipantStatus.Normal && x.ServiceId == id)
              .Join(_residentRepository.Where(x => !x.IsDeleted), x => x.ResidentId, y => y.Id, (x, y) => new DtoJoinServiceResident()
              {
                  Resident = y,
                  RegistTime = x.RegistTime,
                  StartTime = x.StartTime,
                  EndTime = x.EndTime,
              }).ToList();
            //获取服务参与人
            dto.JoinServiceResident = result;

            if (!string.IsNullOrEmpty(dto.Attachments))
            {
                var idList = dto.Attachments.Split(',').ToList();
                dto.AttachmentsFiles = _uploadFileRepository.Where(x => idList.Contains(x.FileId.ToString())).ToList();
            }
            if (dto.VenueDeviceId != null)
            {
                dto.Venuedevice = await _venuedeviceService.GetById(dto.VenueDeviceId.Value);
            }
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

            var query = _participantRepository.AsQueryable(false)
                .Where(p => p.Status == ParticipantStatus.Normal && p.ResidentId == id);
            var currentDate = DateTime.Now;
            query = query.Where(model.NotStarted, x => x.StartTime.Value > currentDate|| x.RegistTime.Value > currentDate.Date);

            var query2 = from p in query
                         join s in _serviceRepository.Entities on p.ServiceId equals s.Id
                        join official in _officialRepository.Entities on s.OfficialsId equals official.Id into result1
                        from so in result1.DefaultIfEmpty()
                        join venuedevice in _venuedeviceRepository.Entities on s.VenueDeviceId equals venuedevice.Id into result2
                        from sv in result2.DefaultIfEmpty()
                            //根据报名时间排序、活动状态、活动开始时间
                        orderby p.CreatedTime descending, s.Status, s.PlanStartTime ascending

                        select new DtoServiceInfo
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
                            VillagesRange = s.VillagesRange,
                            IsSignUp = p.ResidentId == id,
                            RegistTime = p.RegistTime,
                            StartTime = p.StartTime,
                            EndTime = p.EndTime,
                            ParticipantId = p.Id
                        };
            return query2.ToPagedList(model.PageIndex, model.PageSize);
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
        public async Task<string> CancelService(DtoCancelService model)
        {
            var id = JwtHelper.GetAccountId();
            //var service = await _serviceRepository.FindOrDefaultAsync(model.ServiceId);
            //if (service == null)
            //{
            //    throw Oops.Oh("服务不存在");
            //}
            //if (service.Status != ServiceStatusEnum.Published)
            //{
            //    throw Oops.Oh("服务状态异常，无法取消");
            //}
            //var id = JwtHelper.GetAccountId();
            var entity = _participantRepository.FindOrDefault(model.ParticipantId);
            if (entity == null)
            {
                throw Oops.Oh("你没有参加过该服务");
            }
            if (entity.ResidentId != id)
            {
                throw Oops.Oh("操作失败,只能取消自己的参加！");
            }
            await _participantRepository.DeleteNowAsync(entity);
            return "已取消";
        }

        /// <summary>
        /// 获取服务/活动预约记录(占用记录)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<List<DtoParticipant>> GetServiceOccupancyList(DtoServiceSubscribeQuery model)
        {
            //只查询当前服务，自己的报名记录
            model.ResidentId = JwtHelper.GetAccountId();
            model.Status = ParticipantStatus.Occupancy;
            var list = await _serviceService.GetServiceRegistList(model);

            //刷新当前服务占用预约的创建时间
            var currentDate = DateTime.Now;
            var idList = list.Select(x => x.Id);
            _participantRepository.Context.BatchUpdate<Participant>()
                .Where(x => idList.Contains(x.Id))
                .Set(x => x.CreatedTime, x => currentDate)
                .Execute();

            return list;
        }

        /// <summary>
        /// 确认占位服务
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<DtoParticipant> ConfirmOccupancyService(DtoSubscribeService model)
        {
            ChckedServiceStatus(model.ServiceId, model.StartTime, model.EndTime);

            var id = JwtHelper.GetAccountId();
            var exists = _participantRepository.Where(x =>
            //x.ResidentId == id &&
            x.ServiceId == model.ServiceId
            && model.StartTime == x.StartTime
            && model.EndTime == x.EndTime).Any();
            if (exists)
            {
                throw Oops.Oh("预约失败，当前时间段已有预约");
            }
            Participant entity = new Participant();
            entity.Id = YitIdHelper.NextId();
            entity.ServiceId = model.ServiceId;
            entity.ResidentId = JwtHelper.GetAccountId();
            entity.RegistTime = model.RegistTime;//预约时间
            entity.StartTime = model.StartTime;//预约开始时间
            entity.EndTime = model.EndTime;//预约结束时间
            entity.CreatedTime = DateTime.Now;
            entity.Status = ParticipantStatus.Occupancy;//状态为占用
            entity.Creator = JwtHelper.GetAccountId();
            await _participantRepository.InsertNowAsync(entity);
            var dto = entity.Adapt<DtoParticipant>();
            return dto;
        }

        /// <summary>
        /// 取消占位服务
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> CancelOccupancyService(DtoSubscribeService model)
        {
            var id = JwtHelper.GetAccountId();
            var query = _participantRepository.Where(x => x.ResidentId == id && x.ServiceId == model.ServiceId && x.Status == ParticipantStatus.Occupancy);
            query = query.Where(model.StartTime != null, x => model.StartTime == x.StartTime);
            query = query.Where(model.EndTime != null, x => model.EndTime == x.EndTime);
            var list = query.ToList();
            _participantRepository.DeleteNow(list);
            return "取消成功";
        }

        private void ChckedServiceStatus(long serviceId, DateTime? startTime = null, DateTime? endTime = null)
        {
            var service = _serviceRepository.FindOrDefault(serviceId);
            if (service == null)
            {
                throw Oops.Oh("服务不存在");
            }
            //判断服务状态是否在发布状态
            if (service.Status != ServiceStatusEnum.Published)
            {
                throw Oops.Oh("服务状态异常，无法预约");
            }
            if (startTime != null && endTime != null)
            {
                //判断预约时间是 在服务范围内
                if (!(service.PlanStartTime <= startTime && endTime <= service.PlanEndTime))
                {
                    throw Oops.Oh("{0}~{1}预约时间不在服务范围内", startTime.Value.ToString("yyyy-MM-dd HH:mm:ss"), endTime.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                }
            }
        }

        /// <summary>
        /// 预约服务(服务/活动)
        /// </summary>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public async Task<string> SubscribeService(List<DtoParticipant> modelList)
        {
            if (modelList != null && modelList.Any())
            {
                ChckedServiceStatus(modelList[0].ServiceId.Value);
            }
            var idList = modelList.Select(x => x.Id).ToList();
            var count = await _participantRepository.Context.BatchUpdate<Participant>()
                .Where(x => idList.Contains(x.Id))
                .Set(x => x.Status, x => ParticipantStatus.Normal) //将占用改为默认状态
                .ExecuteAsync();
            return count > 0 ? "预约成功" : "预约失败";
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
            var resident = await _residentRepository.FindOrDefaultAsync(account.Id);
            
            model.ResidentId = account.Id;  //居民是自己
            model.PhoneNumber = resident.PhoneNumber;
            var count = await _publicSentimentService.Insert(model);
            if (count > 0)
            {
                return "提交成功";
            }
            return "提交失败";
        }
    }
}