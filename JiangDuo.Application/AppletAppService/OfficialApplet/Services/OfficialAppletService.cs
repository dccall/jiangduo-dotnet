using Furion.DatabaseAccessor;
using Furion.DataValidation;
using Furion.DependencyInjection;
using Furion.FriendlyException;
using JiangDuo.Application.AppletAppService.OfficialApplet.Dtos;
using JiangDuo.Application.AppletService.OfficialApplet.Services;
using JiangDuo.Application.AppService.OfficialService.Dto;
using JiangDuo.Application.AppService.ReserveService.Dto;
using JiangDuo.Application.AppService.ReserveService.Services;
using JiangDuo.Application.AppService.ServiceService.Dto;
using JiangDuo.Application.AppService.ServiceService.Services;
using JiangDuo.Application.AppService.WorkorderService.Dto;
using JiangDuo.Application.AppService.WorkOrderService.Dto;
using JiangDuo.Application.AppService.WorkOrderService.Services;
using JiangDuo.Core.Base;
using JiangDuo.Core.Enums;
using JiangDuo.Core.Models;
using JiangDuo.Core.Services;
using JiangDuo.Core.Utils;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppletAppService.OfficialApplet.Services
{
    public class OfficialAppletService : IOfficialAppletService, ITransient
    {
        private readonly ILogger<OfficialAppletService> _logger;
        private readonly IRepository<Core.Models.Service> _serviceRepository;
        private readonly IRepository<Workorder> _workOrderRepository;
        private readonly IRepository<Participant> _participantRepository;
        private readonly WeiXinService _weiXinService;
        private readonly IRepository<Resident> _residentRepository;
        private readonly IWorkOrderService _workOrderService;
        private readonly IServiceService _serviceService;
        private readonly IVerifyCodeService _verifyCodeService;
        private readonly IAliyunSmsService _aliyunSmsService;
        private readonly IRepository<Official> _officialRepository;
        private readonly IRepository<Reserve> _reserveRepository;
        private readonly IReserveService _reserveService;
        private readonly IRepository<Venuedevice> _venuedeviceRepository;
        private readonly IRepository<SelectArea> _selectAreaRepository;
        private readonly IRepository<Business> _businessRepository;

        public OfficialAppletService(ILogger<OfficialAppletService> logger,
            IWorkOrderService workOrderService,
            IRepository<Resident> residentRepository,
            WeiXinService weiXinService,
            IRepository<Core.Models.Service> serviceRepository,
            IRepository<Workorder> workOrderRepository,
            IVerifyCodeService verifyCodeService,
            IAliyunSmsService aliyunSmsService,
            IRepository<Official> officialRepository,
            IRepository<Reserve> reserveRepository,
            IReserveService reserveService,
            IServiceService serviceService,
            IRepository<SelectArea> selectAreaRepository,
            IRepository<Venuedevice> venuedeviceRepositor,
            IRepository<Business> businessRepository,
            IRepository<Participant> participantRepository)
        {
            _logger = logger;
            _serviceRepository = serviceRepository;
            _workOrderRepository = workOrderRepository;
            _participantRepository = participantRepository;
            _weiXinService = weiXinService;
            _residentRepository = residentRepository;
            _workOrderService = workOrderService;
            _verifyCodeService = verifyCodeService;
            _aliyunSmsService = aliyunSmsService;
            _officialRepository = officialRepository;
            _reserveRepository = reserveRepository;
            _reserveService = reserveService;
            _serviceService = serviceService;
            _venuedeviceRepository = venuedeviceRepositor;
            _selectAreaRepository = selectAreaRepository;
            _businessRepository = businessRepository;
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public async Task<bool> GetVerifyCode(string phone)
        {
            if (!phone.TryValidate(ValidationTypes.PhoneNumber).IsValid)
            {
                throw Oops.Oh($"请输入正确的手机号");
            }
            var validCode = await _verifyCodeService.GenerateVerificationCodeAsync(phone);
            //var templateCode = "";//模板
            //var data = new Dictionary<string, string>();//模板
            //data.Add("code", validCode);
            //_aliyunSmsService.SendSms(phone,templateCode,data);
            return true;
        }

        /// <summary>
        /// 人大登录(手机号登录)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> LoginByPhone(DtoOfficialLogin model)
        {
            if (!model.Phone.TryValidate(ValidationTypes.PhoneNumber).IsValid)
            {
                throw Oops.Oh($"请输入正确的手机号");
            }
            //校验手机验证码是否正确（暂不验证，没有短信服务）
            //var IsValidCode = await _verifyCodeService.CheckVerificationCodeAsync(model.Phone, model.Code);
            //if (!IsValidCode)
            //{
            //    throw Oops.Oh($"验证码不正确或已失效。");
            //}

            var officialEntity = _officialRepository.Where(x => !x.IsDeleted && x.PhoneNumber == model.Phone).FirstOrDefault();
            if (officialEntity == null)
            {
                throw Oops.Oh($"账号不存在");
            }

            AccountModel accountModel = new AccountModel();
            accountModel.Id = officialEntity.Id;
            accountModel.Name = officialEntity.Name;
            accountModel.Type = AccountType.Official;//账号类型
            //accountModel.SelectAreaId = string.IsNullOrEmpty(officialEntity.SelectAreaId) ? 0 : long.Parse(officialEntity.SelectAreaId.Split(",")[0]);
            accountModel.SelectAreaId = officialEntity.SelectAreaId??0;
            var jwtTokenResult = JwtHelper.GetJwtToken(accountModel);
            return jwtTokenResult.AccessToken;
        }

        /// <summary>
        /// 人大登录（微信code登录）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> LoginWeiXin(DtoOfficialLogin model)
        {
            //获取openId
            var result = await _weiXinService.WeiXinLogin(model.JsCode);
            var officialEntity = _officialRepository.Where(x => !x.IsDeleted && x.OpenId == result.OpenId).FirstOrDefault();
            if (officialEntity == null)
            {
                throw Oops.Oh($"账号不存在");
            }
            AccountModel accountModel = new AccountModel();
            accountModel.Id = officialEntity.Id;
            accountModel.Name = officialEntity.Name;
            accountModel.Type = AccountType.Official;//账号类型
            accountModel.SelectAreaId = officialEntity.SelectAreaId??0;
            //accountModel.SelectAreaId= string.IsNullOrEmpty(officialEntity.SelectAreaId) ? 0 : long.Parse(officialEntity.SelectAreaId.Split(",")[0]);
            var jwtTokenResult = JwtHelper.GetJwtToken(accountModel);
            return jwtTokenResult.AccessToken;
        }

        /// <summary>
        /// 我的服务列表(一老一小)
        /// </summary>
        /// <param name="model">数据</param>
        /// <returns></returns>
        public PagedList<DtoService> GetMyServices([FromQuery] DtoServiceQuery model)
        {
            var account = JwtHelper.GetAccountInfo();
            var query = _serviceRepository.Where(x => !x.IsDeleted);
            query = query.Where(!string.IsNullOrEmpty(model.ServiceName), x => x.ServiceName.Contains(model.ServiceName));
            query = query.Where(model.ServiceType != null, x => x.ServiceType == model.ServiceType);
            query = query.Where(model.ServiceClassifyId != null, x => x.ServiceClassifyId == model.ServiceClassifyId.Value);
            query = query.Where(model.Status != null, x => x.Status == model.Status);
            query = query.Where(x => x.Creator == account.Id || x.OfficialsId == account.Id);
            var query2 = from service in query
                         join official in _officialRepository.Entities on service.OfficialsId equals official.Id into result1
                         from so in result1.DefaultIfEmpty()
                         join venuedevice in _venuedeviceRepository.Entities on service.VenueDeviceId equals venuedevice.Id into result2
                         from sv in result2.DefaultIfEmpty()
                         orderby service.CreatedTime descending
                         select new DtoService()
                         {
                             Id = service.Id,
                             ServiceType = service.ServiceType,
                             Address = service.Address,
                             GroupOriented = service.GroupOriented,
                             OfficialsId = service.OfficialsId,
                             OfficialsName = so.Name,
                             PlanEndTime = service.PlanEndTime,
                             PlanNumber = service.PlanNumber,
                             VillagesRange = service.VillagesRange,
                             PlanStartTime = service.PlanStartTime,
                             ServiceClassifyId = service.ServiceClassifyId,
                             ServiceName = service.ServiceName,
                             Attachments = service.Attachments,
                             Remarks = service.Remarks,
                             VenueDeviceId = service.VenueDeviceId,
                             VenueDeviceName = sv.Name,
                             AuditFindings = service.AuditFindings,
                             IsDeleted = service.IsDeleted,
                             Status = service.Status,
                             UpdatedTime = service.UpdatedTime,
                             Updater = service.Updater,
                             Creator = service.Creator,
                             SelectAreaId = service.SelectAreaId,
                             CreatedTime = service.CreatedTime
                         };

            return query2.ToPagedList(model.PageIndex, model.PageSize);
        }

        /// <summary>
        /// 创建服务(一老一小）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> AddServices(DtoServiceForm model)
        {
            var account = JwtHelper.GetAccountInfo();
            model.OfficialsId = account.Id;
            model.SelectAreaId = account.SelectAreaId;
            var count = await _serviceService.Insert(model);
            if (count > 0)
            {
                return "添加成功";
            }
            return "添加失败";
        }

        /// <summary>
        /// 删除服务(一老一小)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> DeleteServices([FromQuery] long id)
        {
            var count = await _serviceService.FakeDelete(id);
            if (count > 0)
            {
                return "删除成功";
            }
            return "删除失败";
        }

        /// <summary>
        /// 服务详情(一老一小)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<DtoService> GetServicesDetail([FromQuery] long id)
        {
            var dto = await _serviceService.GetById(id);
            return dto;
        }

        /// <summary>
        /// 获取我的预约（有事好商量）
        /// </summary>
        /// <param name="model">数据</param>
        /// <returns></returns>
        public PagedList<DtoReserve> GetMyReserves([FromQuery] DtoReserveQuery model)
        {
            var account = JwtHelper.GetAccountInfo();
            var query = _reserveRepository.Where(x => !x.IsDeleted);
            query = query.Where(!string.IsNullOrEmpty(model.Theme), x => x.Theme.Contains(model.Theme));
            query = query.Where(model.SelectAreaId != null, x => x.SelectAreaId == model.SelectAreaId);
            query = query.Where(model.Status != null, x => x.Status == model.Status);
            query = query.Where( x => x.Creator == account.Id); //只查自己的
            query = query.Where(model.StartTime != null, x => x.ReserveDate >= model.StartTime);
            query = query.Where(model.StartTime != null, x => x.ReserveDate <= model.EndTime);
            var query2 = from x in query
                         join venuedevice in _venuedeviceRepository.Entities on x.VenueDeviceId equals venuedevice.Id into result1
                         from rv in result1.DefaultIfEmpty()
                         join official in _officialRepository.Entities on x.Creator equals official.Id into result2
                         from ro in result2.DefaultIfEmpty()
                         orderby x.CreatedTime descending
                         select new DtoReserve()
                         {
                             Id = x.Id,
                             Theme = x.Theme,
                             Number = x.Number,
                             ReserveDate = x.ReserveDate,
                             StartTime = x.StartTime,
                             EndTime = x.EndTime,
                             MeetingResults = x.MeetingResults,
                             Remarks = x.Remarks,
                             VenueDeviceId = x.VenueDeviceId,
                             VenueDeviceName = rv.Name,
                             AuditFindings = x.AuditFindings,
                             WorkOrderId = x.WorkOrderId,
                             IsDeleted = x.IsDeleted,
                             Status = x.Status,
                             UpdatedTime = x.UpdatedTime,
                             Updater = x.Updater,
                             Creator = x.Creator,
                             CreatorName=ro.Name,
                             SelectAreaId = x.SelectAreaId,
                             CreatedTime = x.CreatedTime
                         };
            return query2.ToPagedList(model.PageIndex, model.PageSize);
        }

        /// <summary>
        /// 获取预约详情(有事好商量)
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public async Task<DtoReserve> GetReserveDetail([FromQuery] long id)
        {
            return await _reserveService.GetById(id);
        }

        /// <summary>
        /// 添加预约(有事好商量)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> AddReserve(DtoReserveForm model)
        {
            var account = JwtHelper.GetAccountInfo();
            model.SelectAreaId = account.SelectAreaId;
            model.Creator = JwtHelper.GetAccountId();
            var count = await _reserveService.Insert(model);
            if (count > 0)
            {
                return "添加成功";
            }
            return "删除成功";
        }

        /// <summary>
        /// 删除预约（有事好商量）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> DeleteReserve([FromQuery] long id)
        {
            var count = await _reserveService.FakeDelete(id);
            if (count > 0)
            {
                return "删除成功";
            }
            return "删除失败";
        }

        /// <summary>
        /// 获取我的工单
        /// </summary>
        /// <param name="model">数据</param>
        /// <returns></returns>
        public PagedList<DtoWorkOrder> GetMyWorkOrderList([FromQuery] DtoMyWorkOrderQuery model)
        {
            var id = JwtHelper.GetAccountId();
            var query = _workOrderRepository.Where(x => !x.IsDeleted);
            query = query.Where(x => x.RecipientId == id || x.AssistantId == id);//派给我的/或者我协助的
            if (model.Status == WorkorderStatusEnum.Completed)
            {
                List<WorkorderStatusEnum> statusList = new List<WorkorderStatusEnum>() {
                    WorkorderStatusEnum.Completed,//完成
                    WorkorderStatusEnum.Audited,//审核通过
                    WorkorderStatusEnum.End };//完结
                query = query.Where(x => statusList.Contains(x.Status));
            }
            else
            {
                List<WorkorderStatusEnum> statusList = new List<WorkorderStatusEnum>() {
                    WorkorderStatusEnum.InProgress,//进行中
                    WorkorderStatusEnum.AuditFailed//审核未通过
                };
                query = query.Where(model.Status != null, x => x.Status == model.Status);
            }

            var query2 = from w in query
                         join a in _selectAreaRepository.Entities on w.SelectAreaId equals a.Id into result1
                         from wa in result1.DefaultIfEmpty()
                         join b in _businessRepository.Entities on w.BusinessId equals b.Id into result3
                         from wb in result3.DefaultIfEmpty()
                         select new DtoWorkOrder
                         {
                             Id = w.Id,
                             AssistantId = w.AssistantId,
                             AssistantName = w.AssistantName,
                             Attachments = w.Attachments,
                             BusinessId = w.BusinessId,
                             BusinessName = wb.Name,
                             Content = w.Content,
                             CreatedTime = w.CreatedTime,
                             Creator = w.Creator,
                             IsDeleted = w.IsDeleted,
                             OriginatorId = w.OriginatorId,
                             OriginatorName = w.OriginatorName,
                             PublicSentimentId = w.PublicSentimentId,
                             RecipientId = w.RecipientId,
                             RecipientName = w.RecipientName,
                             OverTime = w.OverTime,
                             Score = w.Score,
                             SelectAreaId = w.SelectAreaId,
                             StartTime = w.StartTime,
                             Status = w.Status,
                             WorkOrderNo = w.WorkOrderNo,
                             UpdatedTime = w.UpdatedTime,
                             Updater = w.Updater,
                             SelectAreaName = wa.SelectAreaName,
                             WorkorderSource = w.WorkorderSource,
                             WorkorderType = w.WorkorderType,
                             IsHelper = w.AssistantId == id,//是否是协助订单
                             PhoneNumber = w.PhoneNumber
                         };

            return query2.OrderByDescending(s => s.CreatedTime).ToPagedList(model.PageIndex, model.PageSize);
        }

        /// <summary>
        /// 获取协助人（分页）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public PagedList<DtoOfficial> GetHelpers(BaseRequest model)
        {
            var id = JwtHelper.GetAccountId();
            var query = _officialRepository.Where(x => !x.IsDeleted);
            query = query.Where(x => x.Id != id);
            //将数据映射到DtoOfficial中
            return query.OrderByDescending(s => s.CreatedTime).ProjectToType<DtoOfficial>().ToPagedList(model.PageIndex, model.PageSize);
        }

        /// <summary>
        /// 根据id查询工单详情
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public async Task<DtoWorkOrder> GetWorkOrderDetail([FromQuery] long id)
        {
            return await _workOrderService.GetById(id);
        }

        /// <summary>
        /// 工单完成
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> WorkOrderCompleted(DtoWorkOrderCompletedHandel model)
        {
            return await _workOrderService.WorkOrderCompleted(model);
        }

        /// <summary>
        /// 工单指派（接收人/协助人）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> WorkOrderAssign(DtoWorkOrderAssign model)
        {
            return await _workOrderService.WorkOrderAssign(model);
        }

        /// <summary>
        /// 工单处理（不变更状态）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> WorkOrderHandel(DtoWorkOrderHandel model)
        {
            return await _workOrderService.WorkOrderHandel(model);
        }
    }
}