using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Furion.FriendlyException;
using JiangDuo.Application.System.Dept.Dtos;
using JiangDuo.Core.Enums;
using JiangDuo.Core.Models;
using JiangDuo.Core.Utils;
using Mapster;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yitter.IdGenerator;

namespace JiangDuo.Application.System.Dept.Services
{
    public class DeptService : IDeptService, ITransient
    {
        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILogger<DeptService> _logger;

        /// <summary>
        /// SysDept仓储
        /// </summary>
        private readonly IRepository<SysDept> _deptRepository;

        private readonly IRepository<SysUser> _userRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志</param>
        /// <param name="userRepository">用户仓储</param>
        /// <param name="deptRepository">部门仓储</param>
        public DeptService(ILogger<DeptService> logger, IRepository<SysUser> userRepository, IRepository<SysDept> deptRepository)
        {
            _logger = logger;
            _deptRepository = deptRepository;
            _userRepository = userRepository;
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="model">数据</param>
        /// <returns></returns>
        public PagedList<DeptDto> GetList(DeptRequest model)
        {
            var query = _deptRepository.Where(x => !x.IsDeleted);
            query = query.Where(!string.IsNullOrEmpty(model.DeptName), x => x.DeptName.Contains(model.DeptName));
            return query.OrderByDescending(x => x.Order).ProjectToType<DeptDto>().ToPagedList(model.PageIndex, model.PageSize);
        }

        /// <summary>
        /// 根据id查询详情
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task<DeptDto> GetById(long id)
        {
            var entity = await _deptRepository.FindOrDefaultAsync(id);

            var dto = entity.Adapt<DeptDto>();

            return dto;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Insert(DtoDeptForm model)
        {
            InsertUpdateChecked(model);
            var entity = model.Adapt<SysDept>();
            entity.Id = YitIdHelper.NextId();
            entity.CreatedTime = DateTime.Now;
            entity.Creator = JwtHelper.GetAccountId();
            entity.Status = DeptStatus.Normal;
            _deptRepository.Insert(entity);
            return await _deptRepository.SaveNowAsync();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(DtoDeptForm model)
        {
            InsertUpdateChecked(model);
            //先根据id查询实体
            var entity = _deptRepository.FindOrDefault(model.Id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            //将模型数据映射给实体属性
            entity = model.Adapt(entity);
            entity.UpdatedTime = DateTime.Now;
            entity.Updater = JwtHelper.GetAccountId();
            _deptRepository.Update(entity, ignoreNullValues: false);
            return await _deptRepository.SaveNowAsync();
        }

        private void InsertUpdateChecked(DtoDeptForm model)
        {
            var query = _deptRepository.AsQueryable();
            query = query.Where(s => s.DeptName == model.DeptName);
            if (model.Id != 0)
            {
                query = query.Where(s => s.Id != model.Id);
            }
            if (query.Count() > 0)
            {
                throw Oops.Oh("[{0}]部门名重复", model.DeptName);
            }
        }

        /// <summary>
        /// 删除前校验
        /// </summary>
        /// <param name="idList"></param>
        private void DeleteCheked(List<long> idList)
        {
            var list = _deptRepository.Where(x => idList.Contains(x.ParentId.Value) && !x.IsDeleted).Select(x => x.DeptName);
            if (list.Count() > 0)
            {
                throw Oops.Oh("部门存在[{0}]部门，无法删除", string.Join(",", list));
            }
            var list2 = _deptRepository.Where(x => idList.Contains(x.ParentId.Value) && !x.IsDeleted)
               .Join(_userRepository.Entities, x => x.Id, y => y.DeptId, (x, y) => x).Select(x => x.DeptName)
               .Distinct();
            if (list2.Count() > 0)
            {
                throw Oops.Oh("[{0}]部门被使用，无法删除", string.Join(",", list2));
            }
        }

        /// <summary>
        /// 假删除
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<int> FakeDelete(long id)
        {
            DeleteCheked(new List<long> { id });
            var entity = _deptRepository.FindOrDefault(id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            entity.IsDeleted = true;
            return await _deptRepository.SaveNowAsync();
        }

        /// <summary>
        /// 批量假删除
        /// </summary>
        /// <param name="idList">编号</param>
        /// <returns></returns>
        public async Task<int> FakeDelete(List<long> idList)
        {
            DeleteCheked(idList);
            var result = await _deptRepository.Context.BatchUpdate<SysDept>()
                .Where(x => idList.Contains(x.Id))
                .Set(x => x.IsDeleted, x => true)
                .ExecuteAsync();
            return result;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<int> Delete(long id)
        {
            DeleteCheked(new List<long> { id });
            _deptRepository.Delete(id);
            return await _deptRepository.SaveNowAsync();
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="idList">编号</param>
        /// <returns></returns>
        public async Task<int> Delete(List<long> idList)
        {
            DeleteCheked(idList);
            foreach (var id in idList)
            {
                _deptRepository.Delete(id);
            }
            return await _deptRepository.SaveNowAsync();
        }
    }
}