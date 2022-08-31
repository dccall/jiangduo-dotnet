using JiangDuo.Core.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace JiangDuo.Application.Filters
{
    /// <summary>
    /// 角色拦截
    /// </summary>
    public class AppletRoleFilterAttribute : Attribute, IActionFilter
    {
        public AccountType RuleType { get; set; }

        public AppletRoleFilterAttribute(AccountType ruleType)
        {
            RuleType = ruleType;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var allowAnonymousAttribute = context.HttpContext.GetMetadata<AllowAnonymousAttribute>();
            if (allowAnonymousAttribute == null)
            {
                var accountInfo = JwtHelper.GetAccountInfo();
                if (accountInfo.Type != RuleType)
                {
                    throw new Exception("账号没有足够的权限");
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}