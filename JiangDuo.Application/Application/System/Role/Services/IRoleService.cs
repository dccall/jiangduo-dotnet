using JiangDuo.Application.Role.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.Role.Services
{
   public interface  IRoleService
    {
        public Task<PagedList<RoleDto>> GetList(RoleRequest model);
        Task<RoleDto> GetById(long id);
        Task<int> Insert(RoleDto model);
        Task<int> Update(RoleDto model);
        public Task<int> FakeDelete(long id);
        public Task<int> FakeDelete(List<long> idList);
    }
}
