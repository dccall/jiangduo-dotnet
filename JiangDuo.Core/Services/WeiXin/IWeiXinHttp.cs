using Furion.RemoteRequest;
using System.Threading.Tasks;

namespace JiangDuo.Core.Services;

public interface IWeiXinHttp : IHttpDispatchProxy
{
    /// <summary>
    /// 通过 code 换取网页授权access_token 和openid
    /// </summary>
    /// <param name="appid"></param>
    /// <param name="secret"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    [Get("/sns/jscode2session?appid={appid}&secret={secret}&js_code={code}&grant_type=authorization_code"), Client("WeiXin")]
    Task<string> WeiXinLogin(string appid, string secret, string code);
}