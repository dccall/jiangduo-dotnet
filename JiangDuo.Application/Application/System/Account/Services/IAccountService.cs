using JiangDuo.Application.Account.Dtos;
using JiangDuo.Application.User.Dtos;
using JiangDuo.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JiangDuo.Application.Account.Services;

public interface IAccountService
{

    Task<DtoUser> GetById(long id);
    Task<DtoUserRoutes> GetUserRoutes(long userId);
    
    Task<SysUser> GetUserByUserNameAndPwd(DtoLoginRequest model);

    public Task<int> UpdatePassword(DtoUpdatePassword model);
}