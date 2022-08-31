using Furion;
using Furion.DependencyInjection;
using Furion.FriendlyException;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace JiangDuo.Core.Services
{
    public class WeiXinService : ITransient
    {
        private readonly ILogger<WeiXinService> _logger;
        private readonly IWeiXinHttp _weiXinHttp;

        private string appid => App.Configuration["WeiXin:Appid"];
        private string secret => App.Configuration["WeiXin:Secret"];

        public WeiXinService(ILogger<WeiXinService> logger, IWeiXinHttp weiXinHttp)
        {
            _logger = logger;
            _weiXinHttp = weiXinHttp;
        }

        public async Task<WeiXinLoginResult> WeiXinLogin(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                throw Oops.Oh("缺少授权code");
            }
            var res = await _weiXinHttp.WeiXinLogin(appid, secret, code);
            var json = JsonConvert.DeserializeObject<WeiXinLoginResult>(res);
            if (json.Errcode != 0)
            {
                throw Oops.Oh("调用微信登录失败" + json.Errmsg);
            }
            return json;
        }
    }

    public class WeiXinLoginResult
    {
        public string OpenId { get; set; }
        public string Session_Key { get; set; }
        public string UnionId { get; set; }
        public int Errcode { get; set; }
        public string Errmsg { get; set; }
    }
}