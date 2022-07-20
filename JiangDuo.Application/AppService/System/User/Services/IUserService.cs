using JiangDuo.Application.User.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JiangDuo.Application.User.Services;

public interface IUserService
{
    public Task<PagedList<DtoUser>> GetList(DtoUserRequert model);
    Task<DtoUser> GetById(long id);
    Task<int> Insert(DtoUserForm model);
    Task<int> Update(DtoUserForm model);
    public Task<int> FakeDelete(long id);
    public Task<int> FakeDelete(List<long> idList);

}