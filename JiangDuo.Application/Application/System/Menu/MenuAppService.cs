
using JiangDuo.Application.Menu.Dtos;
using JiangDuo.Application.Menu.Services;
using JiangDuo.Core.Attributes;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JiangDuo.Core.Filters;

namespace JiangDuo.Application.Menu
{

    /// <summary>
    /// 用户菜单
    /// </summary>
    public class MenuAppService : IDynamicApiController
    {

        private readonly IMenuService _menuService;
        public MenuAppService(IMenuService menuService)
        {
            _menuService=menuService;
        }
        /// <summary>
        /// 获取菜单列表（分页）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public PagedList<MenuDto> Get([FromQuery] MenuRequest model)
        {
            return _menuService.GetList(model);
        }
        /// <summary>
        /// 获取树形菜单
        /// </summary>
        /// <returns></returns>
        public List<MenuTreeDto> GetTreeMenu([FromQuery] MenuRequest model)
        {
            var result= _menuService.GetTreeMenu(model);
            return result;
        }

        /// <summary>
        /// 根据id获取详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<MenuDto> Get(long id)
        {
            return await _menuService.GetById(id);
        }
        /// <summary>
        /// 菜单新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Insert(MenuDto model)
        {
            return await _menuService.Insert(model);
        }
        /// <summary>
        /// 菜单修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(MenuDto model)
        {
            return await _menuService.Update(model);
        }
        /// <summary>
        /// 根据id删除菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> Delete(long id)
        {
            return await _menuService.FakeDelete(id);
        }
        /// <summary>
        /// 批量删除菜单
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        public async Task<int> Delete(List<long> idList)
        {
            return await _menuService.FakeDelete(idList);
        }

    }
}
