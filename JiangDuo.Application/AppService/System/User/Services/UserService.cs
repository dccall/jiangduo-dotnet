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
        var query = _userRepository.Where(x => !x.IsDeleted).AsQueryable();

        query = query.Where(!string.IsNullOrEmpty(model.UserName), x => x.UserName.Contains(model.UserName));
        query = query.Where(model.DeptId!=null, x => x.DeptId== model.DeptId);

        var list = await query.OrderByDescending(x => x.CreatedTime).Select(user => new DtoUser
        {
            Id = user.Id,
            UserName = user.UserName,
            PassWord = user.PassWord,
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

            return dto;
        }
        return null;
    }

    public async Task<int> Insert(DtoUserForm model)
    {
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
            _userRoleRepository.Insert(urList);//
        }

        return await _userRoleRepository.SaveNowAsync();
    }

    public async Task<int> Update(DtoUserForm model)
    {
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

}