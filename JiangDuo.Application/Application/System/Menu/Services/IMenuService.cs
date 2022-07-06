using JiangDuo.Application.Menu.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.Menu.Services
{
   public interface  IMenuService
    {
        public PagedList<MenuDto> GetList(MenuRequest model);
        public List<MenuTreeDto> GetTreeMenu(MenuRequest model);

        Task<MenuDto> GetById(long id);

        Task<int> Insert(MenuDto model);

        Task<int> Update(MenuDto model);
        public Task<int> FakeDelete(long id);
        public Task<int> FakeDelete(List<long> idList);
    }
}
