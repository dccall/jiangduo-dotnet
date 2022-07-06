using JiangDuo.Application.User.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JiangDuo.Application.User.Services;

public interface IUserService
{
    public Task<PagedList<DtoUser>> GetList(DtoUserRequert model);
    Task<DtoUser> GetById(long id);
    Task<int> Insert(DtoUser model);
    Task<int> Update(DtoUser model);
    public Task<int> FakeDelete(long id);
    public Task<int> FakeDelete(List<long> idList);

}