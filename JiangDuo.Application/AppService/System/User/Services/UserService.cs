using JiangDuo.Core.Base;
using JiangDuo.Application.Menu.Dtos;
using JiangDuo.Application.Tools;
using JiangDuo.Application.User.Dtos;
using JiangDuo.Core;
using JiangDuo.Core.Enums;
using JiangDuo.Core.Models;
using Furion.DatabaseAccessor;
using Furion.DataEncryption;
using Furion.DependencyInjection;
using Furion.FriendlyException;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yitter.IdGenerator;
using JiangDuo.Core.Utils;
using Furion;
using JiangDuo.Application.AppService.System.User.Dtos;

namespace JiangDuo.Application.User.Services;

/// <summary>
/// 用户管理
/// </summary>
public class UserService : IUserService, ITransient
{

    private readonly ILogger<UserService> _logger;
    private readonly IRepository<SysUser> _userRepository;
    private readonly IRepository<SysRole> _roleRepository;
    private readonly IRepository<SysMenu> _menuRepository;
    private readonly IRepository<SysUserRole> _userRoleRepository;
    private readonly IRepository<SysRoleMenu> _roleMenuRepository;
    public UserService(ILogger<UserService> logger,
         IRepository<SysRole> roleRepository,
         IRepository<SysUserRole> userRoleRepository,
        IRepository<SysUser> userRepository,
        IRepository<SysMenu> menuRepository,
        IRepository<SysRoleMenu> roleMenuRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _userRoleRepository = userRoleRepository;
        _menuRepository = menuRepository;
        _roleMenuRepository = roleMenuRepository;
    }

    /// <summary>
    /// 获取用户列表
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<PagedList<DtoUser>> GetList(DtoUserRequert model)
    {
        var query = _userRepository.Where(x => !x.IsDeleted && x.Id != 1).AsQueryable();
        query = query.Where(!string.IsNullOrEmpty(model.UserName), x => x.UserName.Contains(model.UserName));
        query = query.Where(!string.IsNullOrEmpty(model.NickName), x => x.NickName.Contains(model.NickName));
        query = query.Where(model.DeptId != null, x => x.DeptId == model.DeptId);

        var list = await query.OrderByDescending(x => x.CreatedTime).Select(user => new DtoUser
        {
            Id = user.Id,
            UserName = user.UserName,
            PassWord = null, //密码不展示
            DeptId = user.DeptId,
            NickName = user.NickName,
            Type = user.Type,
            Email = user.Email,
            Phonenumber = user.Phonenumber,
            Sex = user.Sex,
            Avatar = user.Avatar,
            Status = user.Status,
            LoginIp = user.LoginIp,
            LoginDate = user.LoginDate,
            Remark = user.Remark,
            RoleIdList = _userRoleRepository.Entities.Where(s => s.UserId == user.Id).Join(_roleRepository.Entities, x => x.RoleId, y => y.Id, (x, y) => x.RoleId).ToList(),
            Creator = user.Creator,
            Updater = user.Updater,
            CreatedTime = user.CreatedTime,
            UpdatedTime = user.UpdatedTime,
            IsDeleted = user.IsDeleted,
        }).ToPagedListAsync(model.PageIndex, model.PageSize);
        return list;

    }
    /// <summary>
    /// 根据id获取用户
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<DtoUser> GetById(long id)
    {
        var entity = await _userRepository.FindOrDefaultAsync(id);

        if (entity != null)
        {
            var dto = entity.Adapt<DtoUser>();

            dto.RoleIdList = _userRoleRepository.Entities.Where(s => s.UserId == id).Join(_roleRepository.Entities, x => x.RoleId, y => y.Id, (x, y) => x.RoleId).ToList();
            dto.PassWord = null;//密码不展示
            return dto;
        }
        return null;
    }

    public async Task<int> Insert(DtoUserForm model)
    {
        InsertUpdateChecked(model);
        var entity = model.Adapt<SysUser>();
        entity.Id = YitIdHelper.NextId();
        entity.CreatedTime = DateTime.Now;
        entity.Creator = JwtHelper.GetAccountId();
        entity.Status = UserStatus.Normal;
        var salt = $"${entity.UserName}-${entity.PassWord}";
        var md5 = MD5Encryption.Encrypt(salt, true);
        entity.PassWord = md5;
        entity.IsDeleted = false;
        _userRepository.Insert(entity);

        if (model.RoleIdList != null && model.RoleIdList.Count() > 0)
        {
            List<SysUserRole> urList = model.RoleIdList.Select(roleId => new SysUserRole
            {
                Id = YitIdHelper.NextId(),
                UserId = entity.Id,
                RoleId = roleId
            }).ToList();
            _userRoleRepository.Insert(urList);
        }

        return await _userRoleRepository.SaveNowAsync();
    }

    public async Task<int> Update(DtoUserForm model)
    {
        InsertUpdateChecked(model);
        //先根据id查询实体
        var entity = _userRepository.FindOrDefault(model.Id);
        if (entity == null)
        {
            throw Oops.Oh("数据不存在");
        }
        //将模型数据映射给实体属性
        entity = model.Adapt(entity);
        entity.UpdatedTime = DateTime.Now;
        entity.Updater = JwtHelper.GetAccountId();
        //更新用户，排除UserName，PassWord
        _userRepository.UpdateExcludeNow(entity, new[] { nameof(SysUser.UserName), nameof(SysUser.PassWord) });

        if (model.RoleIdList != null && model.RoleIdList.Count() > 0)
        {
            var oldList = _userRoleRepository.Where(s => s.UserId == entity.Id).ToList();
            _userRoleRepository.Delete(oldList);
            List<SysUserRole> urList = model.RoleIdList.Select(roleId => new SysUserRole
            {
                Id = YitIdHelper.NextId(),
                UserId = entity.Id,
                RoleId = roleId
            }).ToList();
            _userRoleRepository.Insert(urList);
        }
        return await _userRoleRepository.SaveNowAsync();
    }
    public async Task<int> FakeDelete(long id)
    {
        var entity = _userRepository.FindOrDefault(id);
        if (entity == null)
        {
            throw Oops.Oh("数据不存在");
        }
        entity.IsDeleted = true;
        return await _userRepository.SaveNowAsync();
    }
    public async Task<int> FakeDelete(List<long> idList)
    {
        var result = await _userRepository.Context.BatchUpdate<SysUser>()
            .Where(x => idList.Contains(x.Id))
            .Set(x => x.IsDeleted, x => true)
            .ExecuteAsync();
        return result;
    }
    /// <summary>
    /// 重置密码
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<string> ResetPassword(DtoResetPassword model)
    {
        //先根据id查询实体
        var entity = await _userRepository.FindOrDefaultAsync(model.Id);
        if (entity == null)
        {
            throw Oops.Oh("数据不存在");
        }
        var defaultPassword = App.Configuration["UserDefaultPassword"];
        var salt = $"${entity.UserName}-${defaultPassword}";
        var md5 = MD5Encryption.Encrypt(salt, true);
        entity.PassWord = md5;
        //只更新密码属性
        _userRepository.UpdateInclude(entity, new[] { nameof(SysUser.PassWord) });
        _userRepository.SaveNow();
        return "密码已重置"+ defaultPassword;
    }
    /// <summary>
    /// 修改用户状态
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<string> UpdateStatus(DtoUpdateStatus model)
    {
        //先根据id查询实体
        var entity = await _userRepository.FindOrDefaultAsync(model.Id);
        if (entity == null)
        {
            throw Oops.Oh("数据不存在");
        }
        entity.Status = model.Status;
        //只更状态属性
        _userRepository.UpdateInclude(entity, new[] { nameof(SysUser.Status) });
        _userRepository.SaveNow();
        return "操作成功";
    }

    /// <summary>
    /// 新增/修改前校验
    /// </summary>
    /// <param name="model"></param>
    private void InsertUpdateChecked(DtoUserForm model)
    {
        var query = _userRepository.AsQueryable();
        query = query.Where(s => s.UserName == model.UserName);
        if (model.Id != null)
        {
            query = query.Where(s => s.Id != model.Id);
        }
        if (query.Count() > 0)
        {
            throw Oops.Oh("[{0}]用户名重复", model.UserName);
        }
    }
}