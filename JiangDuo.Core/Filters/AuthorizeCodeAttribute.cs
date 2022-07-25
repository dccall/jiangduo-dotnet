
using JiangDuo.Core.Models;
using Furion;
using Furion.DatabaseAccessor;
using Furion.FriendlyException;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JiangDuo.Core.Enums;
using JiangDuo.Core.Utils;

namespace JiangDuo.Application.Filters
{
    /// <summary>
    /// 授权代码
    /// </summary>
    public class AuthorizeCodeAttribute : Attribute,IActionFilter
    {
        public string Code { get; set; }


        public AuthorizeCodeAttribute( string code)
        {
            Code = code;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var authorizeCodeAttribute = context.FindEffectivePolicy<AuthorizeCodeAttribute>();
            if (authorizeCodeAttribute != null)
            {
                var AuthorizeCodeChecked = false;
                bool.TryParse(App.Configuration["AuthorizeCodeChecked"],out AuthorizeCodeChecked);
                if (AuthorizeCodeChecked)
                {
                    var codes = GetUserRouteCodes(JwtHelper.GetAccountId());
                    if (!codes.Contains(authorizeCodeAttribute.Code))
                    {
                        throw Oops.Oh($"你没有足够的权限访问");
                    }
                }
              
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }



        public  List<string> GetUserRouteCodes(long userId)
        {
            var _userRepository=  Db.GetRepository<SysUser>();
            var _roleRepository = Db.GetRepository<SysRole>();
            var _userRoleRepository = Db.GetRepository<SysUserRole>();
            var _roleMenuRepository = Db.GetRepository<SysRoleMenu>();
            var _menuRepository = Db.GetRepository<SysMenu>();
            //获取用户所有可用角色id,
            var roleIdList = _userRoleRepository.Entities.Where(s => s.UserId == userId).Join(_roleRepository.Entities.Where(s=>!s.IsDeleted&& s.Status == RoleStatus.Normal), x => x.RoleId, y => y.Id, (x, y) => x.RoleId).ToList();

            //获取角色所有菜单权限id去重复
            var menuIdList = _roleMenuRepository.Entities.Where(s => roleIdList.Contains(s.RoleId)).Select(s => s.MenuId).Distinct();
            //获取角色对应的所有菜单
            var menulist = _menuRepository.Where(x => !x.IsDeleted && menuIdList.Contains(x.Id)).OrderBy(x => x.Order).ToList();
            var codeList = menulist.Where(s => !string.IsNullOrEmpty(s.Code)).Select(s => s.Code).ToList();
            return codeList;
        }
    }

}
