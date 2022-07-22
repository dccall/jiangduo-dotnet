using JiangDuo.Application.Role.Dtos;
using JiangDuo.Application.Tools;
using JiangDuo.Core;
using JiangDuo.Core.Enums;
using JiangDuo.Core.Models;
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

namespace JiangDuo.Application.Role.Services
{
    public class RoleService : IRoleService, ITransient
    {
        private readonly ILogger<RoleService> _logger;
        private readonly IRepository<SysRole> _roleRepository;
        private readonly IRepository<SysUserRole> _userRoleRepository;
        private readonly IRepository<SysRoleMenu> _roleMenuRepository;
        public RoleService(ILogger<RoleService> logger,
            IRepository<SysUserRole> userRoleRepository,
            IRepository<SysRoleMenu> roleMenuRepository,
        IRepository<SysRole> roleRepository)
        {
            _logger = logger;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
            _roleMenuRepository = roleMenuRepository;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<PagedList<RoleDto>> GetList(RoleRequest model)
        {
            var query = _roleRepository.Where(x => !x.IsDeleted).AsQueryable();
            query = query.Where(!string.IsNullOrEmpty(model.RoleName), x => x.RoleName.Contains(model.RoleName));
            query = query.Where(model.Status != null, x => x.Status == model.Status);
            query = query.Where(model.StartTime != null, x => x.CreatedTime >= model.StartTime);
            query = query.Where(model.EndTime != null, x => x.CreatedTime <= model.EndTime);
            var list = await query.OrderByDescending(x => x.CreatedTime).ProjectToType<RoleDto>().ToPagedListAsync(model.PageIndex, model.PageSize);
            return list;
        }
        /// <summary>
        ///根据主键查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<RoleDto> GetById(long id)
        {
            var entity = await _roleRepository.FindOrDefaultAsync(id);
            var dto = entity.Adapt<RoleDto>();
            dto.MenuIdList = _roleMenuRepository.Where(x => x.RoleId == id).Select(x=>x.MenuId).ToList();
            return dto;
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Insert(DtoRoleFormcs model)
        {
            InsertUpdateChecked(model);
            var entity = model.Adapt<SysRole>();
            entity.Id = YitIdHelper.NextId();
            entity.CreatedTime = DateTimeOffset.UtcNow;
            entity.Creator = JwtHelper.GetUserId();
            entity.Status = RoleStatus.Normal;
            _roleRepository.Insert(entity);

            if(model.MenuIdList!=null&& model.MenuIdList.Count() > 0)
            {
                //添加新菜单
                var newRoleMenu = model.MenuIdList.Select(menuId => new SysRoleMenu()
                {
                    Id = YitIdHelper.NextId(),
                    MenuId = menuId,
                    RoleId = entity.Id
                });
                _roleMenuRepository.Insert(newRoleMenu);
            }
            await _roleRepository.SaveNowAsync();

            return 1;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(DtoRoleFormcs model)
        {
            InsertUpdateChecked(model);
            //先根据id查询实体
            var entity = _roleRepository.FindOrDefault(model.Id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            //将模型数据映射给实体属性
            entity = model.Adapt(entity);
            entity.UpdatedTime = DateTimeOffset.UtcNow;
            entity.Updater = JwtHelper.GetUserId();
            await _roleRepository.UpdateAsync(entity);
            if (model.MenuIdList != null && model.MenuIdList.Count() > 0)
            {
                //删除原有菜单
                var roleMenu = _roleMenuRepository.Where(s => s.RoleId == entity.Id).ToList();
                _roleMenuRepository.Delete(roleMenu);
                //添加新菜单
                var newRoleMenu = model.MenuIdList.Select(menuId => new SysRoleMenu()
                {
                    Id = YitIdHelper.NextId(),
                    MenuId = menuId,
                    RoleId = entity.Id
                });
                _roleMenuRepository.Insert(newRoleMenu);
            }
            await _roleRepository.SaveNowAsync();
            return 1;
        }
        /// <summary>
        /// 假删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> FakeDelete(long id)
        {
            DeleteCheked(new List<long> { id });
            var entity = _roleRepository.FindOrDefault(id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            entity.IsDeleted = true;
            return await _roleRepository.SaveNowAsync();
        }
        /// <summary>
        /// 批量假删除
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        public async Task<int> FakeDelete(List<long> idList)
        {
            DeleteCheked(idList);
            var result = await _roleRepository.Context.BatchUpdate<SysRole>()
               .Where(x => idList.Contains(x.Id))
               .Set(x => x.IsDeleted, x => true)
               .ExecuteAsync();
            return result;
        }

        /// <summary>
        /// 新增前校验
        /// </summary>
        /// <param name="model"></param>
        private void InsertUpdateChecked(DtoRoleFormcs model)
        {
            var query = _roleRepository.AsQueryable();
            query = query.Where(s => s.RoleName == model.RoleName);
            if (model.Id != 0)
            {
                query = query.Where(s => s.Id != model.Id);
            }
            if (query.Count() > 0)
            {
                throw Oops.Oh("[{0}]角色名重复", model.RoleName);
            }
        }
        /// <summary>
        /// 删除前校验
        /// </summary>
        /// <param name="idList"></param>
        private void DeleteCheked(List<long> idList)
        {
            var roleList = _userRoleRepository.Where(x => idList.Contains(x.RoleId))
                .Join(_roleRepository.Entities, x => x.RoleId, y => y.Id, (x, y) => y).Select(x => x.RoleName)
                .Distinct();
            if (roleList.Count() > 0)
            {
                throw Oops.Oh("{0}角色被使用", string.Join(",", roleList));
            }
        }
    }
}
