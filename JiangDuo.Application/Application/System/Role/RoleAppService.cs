
using JiangDuo.Application.Role.Dtos;
using JiangDuo.Application.Role.Services;
using Furion.DependencyInjection;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.Menu
{
    /// <summary>
    /// 用户角色
    /// </summary>
    public class RoleAppService : IDynamicApiController
    {
        private readonly IRoleService _roleService;
        public RoleAppService(IRoleService roleService)
        {
            _roleService=roleService;
        }
        /// <summary>
        /// 查询列表（分页）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<PagedList<RoleDto>> Get([FromQuery]RoleRequest model)
        {
            return await _roleService.GetList(model);
        }
        /// <summary>
        /// 根据id获取详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<RoleDto> Get(long id)
        {
            return await _roleService.GetById(id);
        }
        /// <summary>
        /// 角色新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Insert(RoleDto model)
        {
            return await _roleService.Insert(model);
        }
        /// <summary>
        /// 角色修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(RoleDto model)
        {
            return await _roleService.Update(model);
        }
        /// <summary>
        /// 根据id删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> Delete(long id)
        {
            return await _roleService.FakeDelete(id);
        }
        /// <summary>
        /// 批量删除角色
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        public async Task<int> Delete([FromBody] List<long> idList)
        {
            return await _roleService.FakeDelete(idList);
        }
    }
}
