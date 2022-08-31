using System.Threading.Tasks;

namespace JiangDuo.Core.Services;

/// <summary>
/// 验证码服务
/// </summary>
public interface IVerifyCodeService
{
    /// <summary>
    /// 根据手机号生成验证码
    /// </summary>
    /// <param name="phone">手机号</param>
    /// <returns></returns>
    Task<string> GenerateVerificationCodeAsync(string phone);

    /// <summary>
    /// 验证验证码是否正确
    /// </summary>
    /// <param name="phone">手机号</param>
    /// <param name="code">代码</param>
    /// <returns></returns>
    Task<bool> CheckVerificationCodeAsync(string phone, string code);
}