using Furion;
using Furion.DatabaseAccessor;
using JiangDuo.Core.Enums;
using JiangDuo.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Core.Filters;

public class OperLogActionFilterAttribute :Attribute, IAsyncActionFilter
{

    public string Name { get; set; }

    public OperLogActionFilterAttribute(string name)
    {
        Name = name;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var OperLogSwitch = false;
        bool.TryParse(App.Configuration["OperLogSwitch"], out OperLogSwitch);
        if (OperLogSwitch)
        {
            //============== 这里是执行方法之前获取数据 ====================
            // 获取控制器、路由信息
            var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            // 获取请求的方法
            var method = actionDescriptor!.MethodInfo;
            // 获取 HttpContext 和 HttpRequest 对象
            var httpContext = context.HttpContext;
            var httpRequest = httpContext.Request;
            // 获取客户端 Ipv4 地址
            var remoteIPv4 = httpContext.GetRemoteIpAddressToIPv4();
            // 获取请求的 Url 地址
            var requestUrl = httpRequest.GetRequestUrlAddress();
            // 获取来源 Url 地址
            var refererUrl = httpRequest.GetRefererUrlAddress();
            // 获取请求参数（写入日志，需序列化成字符串后存储），可以自由篡改！！！！！！
            var parameters = context.ActionArguments;
            // 获取操作人（必须授权访问才有值）"userId" 为你存储的 claims type，jwt 授权对应的是 payload 中存储的键名
            var userId = httpContext.User?.FindFirstValue("userId");
            var userName = httpContext.User?.FindFirstValue("UserName");
            // 请求时间
            var requestedTime = DateTimeOffset.Now;
            //============== 这里是执行方法之后获取数据 ====================
            var actionContext = await next();
            // 获取返回的结果
            var returnResult = actionContext.Result;
            // 判断是否请求成功，没有异常就是请求成功
            var isRequestSucceed = actionContext.Exception == null;
            // 获取调用堆栈信息，提供更加简单明了的调用和异常堆栈
            var stackTrace = EnhancedStackTrace.Current();
            // 其他操作，如写入日志

            SysOperLog entity = new SysOperLog();
            entity.Title = Name;
            entity.OperName = userName;
            entity.Method = actionDescriptor.DisplayName;
            entity.OperIp = remoteIPv4;
            entity.OperTime = requestedTime;
            entity.RequestMethod = httpRequest.Method;
            entity.OperUrl = requestUrl;
            entity.OperSource = refererUrl;
            entity.OperParam = JsonConvert.SerializeObject(parameters);
            entity.Status = isRequestSucceed ? OperLogStatus.Success : OperLogStatus.Fail;
            entity.JsonResult = isRequestSucceed ? JsonConvert.SerializeObject(returnResult) : "";
            entity.ErrorMsg = isRequestSucceed ? "" : actionContext.Exception.Message;
            entity.StackTrace = isRequestSucceed ? "" : actionContext.Exception.StackTrace;

            var db = Db.GetRepository<SysOperLog>();
            db.InsertNow(entity);
        }
        else
        {
            await next();
        }
    }
}