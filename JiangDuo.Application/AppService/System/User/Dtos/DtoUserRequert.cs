using JiangDuo.Core.Base;
using Mapster;

namespace JiangDuo.Application.User.Dtos;

/// <summary>
/// 用户列表参数
/// </summary>
public class DtoUserRequert : BaseRequest
{
    /// <summary>
    /// 部门ID
    /// </summary>
    public long? DeptId { get; set; }
    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }
    /// <summary>
    /// 昵称
    /// </summary>
    public string NickName { get; set; }
    
}