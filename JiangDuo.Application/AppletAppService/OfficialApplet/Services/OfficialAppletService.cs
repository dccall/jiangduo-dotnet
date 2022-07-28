using JiangDuo.Application.System.Config.Dto;
using JiangDuo.Application.Tools;
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
using JiangDuo.Application.AppletService.OfficialApplet.Services;
using JiangDuo.Application.AppletAppService.OfficialApplet.Dtos;
using JiangDuo.Core.Services;
using JiangDuo.Application.AppService.WorkOrderService.Services;
using JiangDuo.Application.AppService.ServiceService.Services;
using Furion.DataValidation;
using JiangDuo.Application.AppService.WorkOrderService.Dto;
using JiangDuo.Core.Enums;
using JiangDuo.Application.AppService.WorkorderService.Dto;
using JiangDuo.Core.Base;

namespace JiangDuo.Application.AppletAppService.OfficialApplet.Services
{
    public class OfficialAppletService:IOfficialAppletService, ITransient
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
        public OfficialAppletService(ILogger<OfficialAppletService> logger, 
            IWorkOrderService workOrderService, 
            IRepository<Resident> residentRepository, 
            WeiXinService weiXinService, 
            IRepository<Core.Models.Service> serviceRepository, 
            IRepository<Workorder> workOrderRepository,
            IVerifyCodeService verifyCodeService,
            IAliyunSmsService aliyunSmsService,
            IRepository<Official> officialRepository,
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
            var officialEntity = _officialRepository.Where(x => !x.IsDeleted&& x.OpenId == result.OpenId).FirstOrDefault();
            if (officialEntity == null)
            {
                throw Oops.Oh($"账号不存在");
            }
            AccountModel accountModel = new AccountModel();
            accountModel.Id = officialEntity.Id;
            accountModel.Name = officialEntity.Name;
            accountModel.Type = AccountType.Official;//账号类型
            var jwtTokenResult = JwtHelper.GetJwtToken(accountModel);
            return jwtTokenResult.AccessToken;
        }


        /// <summary>
        /// 申请服务(工单)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> ApplyForServices(DtoWorkOrderForm model)
        {
            model.WorkorderSource = WorkorderSourceEnum.Official;
            var count = await _workOrderService.Insert(model);
            if (count > 0)
            {
                return "申请已提交";
            }
            return "申请提交失败";
        }


        /// <summary>
        /// 获取我的工单
        /// </summary>
        /// <param name="model">数据</param>
        /// <returns></returns>
        public PagedList<DtoWorkOrder> GetMyWorkOrderList(DtoMyWorkOrderQuery model)
        {
            var id = JwtHelper.GetAccountId();
            var query = _workOrderRepository.Where(x => !x.IsDeleted);
            query = query.Where(x => x.Creator == id||x.ReceiverId==id);//我创建的或指派给我的

            if (model.Status != null)
            {
                if (model.Status == 1) //待处理
                {
                    var statusList = new List<WorkorderStatusEnum>() {
                        WorkorderStatusEnum.NotProcessed,
                    };
                    query = query.Where(x => statusList.Contains( x.Status));
                }
                if (model.Status == 2) //待审核
                {
                    //var statusList = new List<WorkorderStatusEnum>() {
                    //    WorkorderStatusEnum.NotProcessed,
                    //};
                    //query = query.Where(x => statusList.Contains(x.Status));
                }
                if (model.Status ==3)//已完成
                {
                    var statusList = new List<WorkorderStatusEnum>() {
                        WorkorderStatusEnum.End,//已结束
                        WorkorderStatusEnum.Approve,//已同意
                        WorkorderStatusEnum.Reject,//已拒绝
                    };
                    query = query.Where(x => statusList.Contains(x.Status));
                }
            }

            //将数据映射到DtoWorkOrder中
            return query.OrderByDescending(s => s.CreatedTime).ProjectToType<DtoWorkOrder>().ToPagedList(model.PageIndex, model.PageSize);
        }
    }
}
