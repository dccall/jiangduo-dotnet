using AlibabaCloud.OpenApiClient.Models;
using AlibabaCloud.SDK.Dysmsapi20170525;
using AlibabaCloud.SDK.Dysmsapi20170525.Models;
using AlibabaCloud.TeaUtil.Models;
using Furion;
using Furion.DependencyInjection;
using Furion.FriendlyException;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Core.Services
{
    public class AliyunSmsService: IAliyunSmsService, ITransient
    {

        private readonly ILogger<AliyunSmsService> _logger;
        public AliyunSmsService(ILogger<AliyunSmsService> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="templateCode"></param>
        /// <param name="data"></param>
        public  void SendSms(string mobile, string templateCode, Dictionary<string, string> data)
        {

            var accessKeyId = App.Configuration["sms-accessKeyId"];
            var accessKeySecret = App.Configuration["sms-accessKeySecret"];
            Config config = new Config
            {
                // 您的 AccessKey ID
                AccessKeyId = accessKeyId,
                // 您的 AccessKey Secret
                AccessKeySecret = accessKeySecret,
                Endpoint = "dysmsapi.aliyuncs.com",
                //其它配置项

            };
            // 访问的域名
            config.Endpoint = "dysmsapi.aliyuncs.com";

            var client = new Client(config);
            SendSmsRequest sendSmsRequest = new SendSmsRequest();
            sendSmsRequest.PhoneNumbers = mobile; //发送的手机号
            sendSmsRequest.SignName = "江苏裕通";//标题
            sendSmsRequest.TemplateCode = templateCode;//模板"SMS_154950909"
            sendSmsRequest.TemplateParam = JsonConvert.SerializeObject(data);// "{\"code\":\"4321\"}"
            RuntimeOptions runtime = new RuntimeOptions();

            try
            {
                SendSmsResponse resp = client.SendSmsWithOptions(sendSmsRequest, runtime);
            }
            catch (Exception e)
            {
                //打印 error
                AlibabaCloud.TeaUtil.Common.AssertAsString(e.Message);
                _logger.LogError("发送短信失败" + e.Message);
                throw Oops.Oh("发送短信失败" + e.Message);
            }
        }

    }
}
