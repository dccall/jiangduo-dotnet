using System.Collections.Generic;

namespace JiangDuo.Core.Services
{
    public interface IAliyunSmsService
    {
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="templateCode"></param>
        /// <param name="data"></param>
        public void SendSms(string mobile, string templateCode, Dictionary<string, string> data);
    }
}