using JiangDuo.Core.Base;
using JiangDuo.Application.Menu.Dtos;
using JiangDuo.Application.Tools;
using JiangDuo.Core;
using JiangDuo.Core.Enums;
using JiangDuo.Core.Models;
using Furion;
using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Furion.FriendlyException;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yitter.IdGenerator;
using JiangDuo.Core.Utils;

namespace JiangDuo.Application.Menu.Services
{
    public class MenuService :IMenuService, ITransient
    {

        private readonly ILogger<MenuService> _logger;
        private readonly IRepository<SysMenu> _menuRepository;

        private readonly IRepository<SysRoleMenu> _roleMenuRepository;
        public MenuService(ILogger<MenuService> logger,
            IRepository<SysRoleMenu> roleMenuRepository,
            IRepository<SysMenu> menuRepository)
        {
            _logger = logger;
            _menuRepository = menuRepository;
            _roleMenuRepository = roleMenuRepository;
        }

        /// <summary>
        /// 获取树形菜单
        /// </summary>
        /// <returns></returns>
        public List<MenuTreeDto> GetTreeMenu(MenuRequest model)
        {
            var query = _menuRepository.Where(x => !x.IsDeleted);
             query = query.Where(!string.IsNullOrEmpty( model.Title), x => x.Title.Contains(model.Title));
            var menulist = query.OrderByDescending(x => x.Order).ProjectToType<MenuTreeDto>().ToList();
            var roots = GetRoots(menulist);
            foreach (var root in roots)
            {
                GetChildren(root, menulist);
            }
            return roots;
        }
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public PagedList<MenuDto> GetList(MenuRequest model)
        {
            var query = _menuRepository.Where(x => !x.IsDeleted);
            return query.OrderByDescending(x => x.Order).ProjectToType<MenuDto>().ToPagedList(model.PageIndex, model.PageSize);
        }

        public async Task<MenuDto> GetById(long id)
        {
            var entity = await _menuRepository.FindOrDefaultAsync(id);

            var dto = entity.Adapt<MenuDto>();

            return dto;
        }

        public async Task<int> Insert(DtoMenuForm model)
        {
            InsertUpdateChecked(model);
            var entity = model.Adapt<SysMenu>();
            entity.Id = YitIdHelper.NextId();
            entity.CreatedTime = DateTime.Now;
            entity.Creator = JwtHelper.GetAccountId();
            _menuRepository.Insert(entity);
            return await _menuRepository.SaveNowAsync();
        }

        public async Task<int> Update(DtoMenuForm model)
        {
            InsertUpdateChecked(model);
            //先根据id查询实体
            var entity = _menuRepository.FindOrDefault(model.Id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            //将模型数据映射给实体属性
            entity = model.Adapt(entity);
            entity.UpdatedTime = DateTime.Now;
            entity.Updater = JwtHelper.GetAccountId();
            _menuRepository.Update(entity);
            return await _menuRepository.SaveNowAsync();
        }
    
        public async Task<int> FakeDelete(long id)
        {
            DeleteCheked(new List<long>() { id });
            var entity = _menuRepository.FindOrDefault(id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            entity.IsDeleted = true;
            return await _menuRepository.SaveNowAsync();
        }
        public async Task<int> FakeDelete(List<long> idList)
        {
            DeleteCheked(idList);
             var result= await _menuRepository.Context.BatchUpdate<SysMenu>()
                .Where(x => idList.Contains(x.Id))
                .Set(x => x.IsDeleted, x => true)
                .ExecuteAsync();
            return result;
        }
        /// <summary>
        /// 新增修改校验
        /// </summary>
        /// <param name="model"></param>
        private void InsertUpdateChecked(DtoMenuForm model)
        {
            var query = _menuRepository.AsQueryable();
            query = query.Where(s => !s.IsDeleted&& s.Title == model.Title);
            if (model.Id.HasValue)
            {
                query = query.Where(s => s.Id != model.Id);
            }
            if (query.Count() > 0)
            {
                throw Oops.Oh("[{0}]菜单名重复", model.Title);
            }
        }
        /// <summary>
        /// 删除前校验
        /// </summary>
        /// <param name="idList"></param>
        private void DeleteCheked(List<long> idList)
        {

            var list = _roleMenuRepository.Where(x => idList.Contains(x.MenuId))
                .Join(_menuRepository.Entities, x => x.MenuId, y => y.Id, (x, y) => y).Select(x => x.Title)
                .Distinct();
            if (list.Count() > 0)
            {
                throw Oops.Oh("{0}菜单被使用", string.Join(",", list));
            }
        }

        private List<MenuTreeDto> GetRoots(List<MenuTreeDto> allList)
        {
            List<MenuTreeDto> roots = new List<MenuTreeDto>();
            foreach (var menuTreeDto in allList)
            {
                var list = allList.Where(s => s.Id == menuTreeDto.ParentId);
                if (list.Count() == 0 || menuTreeDto.ParentId == null)
                {
                    roots.Add(menuTreeDto);
                }

            }
            return roots;
        }
        private void GetChildren(MenuTreeDto node, List<MenuTreeDto> allList)
        {
            var list = allList.Where(s => s.ParentId == node.Id).ToList();
            if (list.Count() > 0)
            {
                node.Children = list;
                foreach (var children in node.Children)
                {
                    GetChildren(children, allList);
                }
            }
        }
    }
}
