using JiangDuo.Application.Account.Dtos;
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

namespace JiangDuo.Application.Account.Services;

/// <summary>
/// 用户管理
/// </summary>
public class AccountService : IAccountService, ITransient
{

    private readonly ILogger<AccountService> _logger;
    private readonly IRepository<SysUser> _userRepository;
    private readonly IRepository<SysRole> _roleRepository;
    private readonly IRepository<SysMenu> _menuRepository;
    private readonly IRepository<SysUserRole> _userRoleRepository;
    private readonly IRepository<SysRoleMenu> _roleMenuRepository;
    public AccountService(ILogger<AccountService> logger,
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

            dto.RoleIdList= _userRoleRepository.Entities.Where(s => s.UserId == id).Join(_roleRepository.Entities, x => x.RoleId, y => y.Id, (x, y) => x.RoleId).ToList();

            return dto;
        }
        return null;
    }
    /// <summary>
    /// 根据用户id获取路由
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<DtoUserRoutes> GetUserRoutes(long userId)
    {
        var user= await GetById(userId);//获取用户信息
        //获取用户所有可用角色id,
        var roleIdList = _roleRepository.Where(s => user.RoleIdList.Contains(s.Id)&&s.Status==RoleStatus.Normal).Select(x=>x.Id);
        //获取角色所有菜单权限id去重复
        var menuIdList =  _roleMenuRepository.Entities .Where(s =>  roleIdList.Contains(s.RoleId)).Select(s=>s.MenuId).Distinct(); 
        //获取角色对应的所有菜单
        var menulist = _menuRepository.Where(x => !x.IsDeleted&& menuIdList.Contains(x.Id)).OrderBy(x=>x.Order).ProjectToType<MenuTreeDto>().ToList();
        var allMenuList = menulist.Where(s => s.Type == MenuType.Menu).ToList();
        var codeList = menulist.Where(s=>!string.IsNullOrEmpty(s.Code)).Select(s => s.Code).ToList();
        var roots = GetRoots(allMenuList);
        foreach (var root in roots)
        {
            GetChildren(root, allMenuList);
        }
        DtoUserRoutes dto = new DtoUserRoutes();
        dto.Home = "dashboard";
        dto.Routes= roots;
        dto.Codes = codeList;
        return dto;
    }
 
    public async Task<List<string>> GetUserRouteCodes(long userId)
    {
        var user = await GetById(userId);//获取用户信息
        //获取用户所有可用角色id,
        var roleIdList = _roleRepository.Where(s => user.RoleIdList.Contains(s.Id) && s.Status == RoleStatus.Normal).Select(x => x.Id);
        //获取角色所有菜单权限id去重复
        var menuIdList = _roleMenuRepository.Entities.Where(s => roleIdList.Contains(s.RoleId)).Select(s => s.MenuId).Distinct();
        //获取角色对应的所有菜单
        var menulist = _menuRepository.Where(x => !x.IsDeleted && menuIdList.Contains(x.Id)).OrderBy(x => x.Order).ProjectToType<MenuTreeDto>().ToList();
        var codeList = menulist.Where(s => !string.IsNullOrEmpty(s.Code)).Select(s => s.Code).ToList();
        return codeList;
    }
    private List<MenuTreeDto> GetRoots(List<MenuTreeDto> allList)
    {
        List<MenuTreeDto> roots = new List<MenuTreeDto>();
        foreach (var menuTreeDto in allList)
        {
            //没有父级菜单的都为一级菜单
            var list=  allList.Where(s => s.Id == menuTreeDto.ParentId);
            if (list.Count()==0|| menuTreeDto.ParentId==null)
            {
                //获取当前节点下的子路由
                var count = allList.Where(s => s.ParentId == menuTreeDto.Id).Count();
                //一级路由组件默认为basic
                menuTreeDto.Component = "basic";
                //没有子路由的设置独立布局页面
                if (count ==0)
                {
                    menuTreeDto.Component = "self";
                    menuTreeDto.SingleLayout = "basic";
                }
                roots.Add(menuTreeDto);
            }
           
        }
        return roots;
    }
    private void GetChildren(MenuTreeDto node, List<MenuTreeDto> allList)
    {
        //获取当前节点子下的子节点
        var list = allList.Where(s => s.ParentId == node.Id).ToList();
        if (list.Count()> 0)
        {
            //如果当前节点已有组件，并且有子路由则当前节点为子目录
            if(string.IsNullOrEmpty(node.Component))
            {
                node.Component = "multi";
            }
            node.Children = list;
            foreach (var children in node.Children)
            {
                GetChildren(children, allList);
            }
        }
        else //没有子节点全部为self
        {
            if (string.IsNullOrEmpty(node.Component))
            {
                node.Component = "self";
            }
        }
    }


    public async Task<SysUser> GetUserByUserNameAndPwd(DtoLoginRequest model)
    {
        var salt = $"${model.UserName}-${model.PassWord}";
        var md5 = MD5Encryption.Encrypt(salt, true);
         var entity = await _userRepository.Entities.AsNoTracking().FirstOrDefaultAsync(u =>!u.IsDeleted&& u.UserName == model.UserName && u.PassWord == md5);
        return entity;
    }
    
    public async Task<int> UpdatePassword(DtoUpdatePassword model)
    {
        var userId = JwtHelper.GetUserId();
        var userEntity = _userRepository.FindOrDefault(userId);
        var salt = $"${userEntity.UserName}-${model.OldPassWord}";
        var md5 = MD5Encryption.Encrypt(salt, true);
        
        if (userEntity.PassWord!= md5)
        {
            throw Oops.Oh($"原密码不正确");
        }
        var newMd5 = MD5Encryption.Encrypt($"${userEntity.UserName}-${model.NewPassWord}", true);
        userEntity.PassWord = newMd5;
        
        return await _userRepository.SaveNowAsync();
    }
}