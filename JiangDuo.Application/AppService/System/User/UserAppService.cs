using Furion.DynamicApiController;
using JiangDuo.Application.AppService.System.User.Dtos;
using JiangDuo.Application.Filters;
using JiangDuo.Application.User.Dtos;
using JiangDuo.Application.User.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JiangDuo.Application.User;

/// <summary>
/// 用户管理
/// </summary>
[Route("api/[controller]")]
public class UserAppService : IDynamicApiController
{
    private readonly IUserService _userService;

    public UserAppService(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// 查询列表（分页）
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [AuthorizeCode("user_list")]
    public async Task<PagedList<DtoUser>> Get([FromQuery] DtoUserRequert model)
    {
        return await _userService.GetList(model);
    }

    /// <summary>
    /// 根据id获取详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<DtoUser> Get(long id)
    {
        return await _userService.GetById(id);
    }

    /// <summary>
    /// 新增用户
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<int> Insert(DtoUserForm model)
    {
        return await _userService.Insert(model);
    }

    /// <summary>
    /// 修改用户
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<int> Update(DtoUserForm model)
    {
        return await _userService.Update(model);
    }

    /// <summary>
    /// 根据id删除用户
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<int> Delete(long id)
    {
        return await _userService.FakeDelete(id);
    }

    /// <summary>
    /// 批量删除用户
    /// </summary>
    /// <param name="idList"></param>
    /// <returns></returns>
    [HttpPost("Delete")]
    public async Task<int> Delete([FromBody] List<long> idList)
    {
        return await _userService.FakeDelete(idList);
    }

    /// <summary>
    /// 重置密码
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("ResetPassword")]
    public async Task<string> ResetPassword([FromBody] DtoResetPassword model)
    {
        return await _userService.ResetPassword(model);
    }

    /// <summary>
    /// 修改用户状态
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("UpdateStatus")]
    public async Task<string> UpdateStatus([FromBody] DtoUpdateStatus model)
    {
        return await _userService.UpdateStatus(model);
    }
}