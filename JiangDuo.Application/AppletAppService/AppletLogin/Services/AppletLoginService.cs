using Furion.DatabaseAccessor;
using Furion.DataValidation;
using Furion.DependencyInjection;
using Furion.FriendlyException;
using JiangDuo.Application.AppletAppService.AppletLogin.Dtos;
using JiangDuo.Application.AppService.ReserveService.Services;
using JiangDuo.Application.AppService.WorkOrderService.Services;
using JiangDuo.Core.Models;
using JiangDuo.Core.Services;
using JiangDuo.Core.Utils;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Yitter.IdGenerator;

namespace JiangDuo.Application.AppletAppService.AppletLogin.Services
{
    public class AppletLoginService : IAppletLoginService, ITransient
    {
        private readonly ILogger<AppletLoginService> _logger;
        private readonly WeiXinService _weiXinService;
        private readonly IRepository<Resident> _residentRepository;
        private readonly IVerifyCodeService _verifyCodeService;
        private readonly IRepository<Official> _officialRepository;

        public AppletLoginService(ILogger<AppletLoginService> logger,
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
            IRepository<Participant> participantRepository)
        {
            _logger = logger;
            _weiXinService = weiXinService;
            _residentRepository = residentRepository;
            _verifyCodeService = verifyCodeService;
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
        /// 小程序登录(手机号登录)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<DtoAppletLoginResult> LoginByPhone(DtoAppletLogin model)
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
            //先判人大账号是否存在
            var officialEntity = _officialRepository.Where(x => !x.IsDeleted && x.PhoneNumber == model.Phone).FirstOrDefault();
            if (officialEntity != null)
            {
                var weixinResult = await _weiXinService.WeiXinLogin(model.JsCode);
                officialEntity.OpenId = weixinResult.OpenId;
                _officialRepository.UpdateNow(officialEntity);
                var jwtToken = JwtHelper.GetJwtToken(new AccountModel()
                {
                    Id = officialEntity.Id,
                    Name = officialEntity.Name,
                    SelectAreaId = officialEntity.SelectAreaId??0,
                    //SelectAreaId = string.IsNullOrEmpty(officialEntity.SelectAreaId)?0:long.Parse(officialEntity.SelectAreaId.Split(",")[0]),
                    Type = AccountType.Official//账号类型人大
                });

                return new DtoAppletLoginResult()
                {
                    AccessToken = jwtToken.AccessToken,
                    RefreshToken = jwtToken.RefreshToken,
                    Type = AccountType.Official
                };
            }
            //判断居民账号是否存在
            var residentEntity = _residentRepository.Where(x => x.PhoneNumber == model.Phone).FirstOrDefault();
            if (residentEntity == null)//没有就新增
            {
                var weixinResult = await _weiXinService.WeiXinLogin(model.JsCode);
                residentEntity = new Resident();
                residentEntity.PhoneNumber = model.Phone;
                residentEntity.OpenId = weixinResult.OpenId;
                residentEntity.Id = YitIdHelper.NextId();
                residentEntity.Birthday = DateTime.Now;
                residentEntity.CreatedTime = DateTime.Now;
                residentEntity.Creator = 0;
                await _residentRepository.InsertNowAsync(residentEntity);
            }

            var jwt = JwtHelper.GetJwtToken(new AccountModel()
            {
                Id = residentEntity.Id,
                Name = residentEntity.Name,
                SelectAreaId = residentEntity.SelectAreaId ?? 0,
                Type = AccountType.Resident//账号类型居民
            });

            return new DtoAppletLoginResult()
            {
                AccessToken = jwt.AccessToken,
                RefreshToken = jwt.RefreshToken,
                Type = AccountType.Resident
            };
        }
    }
}