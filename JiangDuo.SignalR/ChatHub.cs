using Furion;
using Furion.FriendlyException;
using Furion.InstantMessaging;
using JiangDuo.Core.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.SignalR
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChatHub : Hub<IChatClient>
    {

        // 其他代码
        public static void HttpConnectionDispatcherOptionsSettings(HttpConnectionDispatcherOptions options)
        {
            // 配置
            //options.CloseOnAuthenticationExpiration
        }
        public static void HubEndpointConventionBuilderSettings(HubEndpointConventionBuilder Builder)
        {
            // 配置
        }

        /// <summary>
        /// 建立连接
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            var userId = Context.User?.FindFirst(s => s.Type == "Id")?.Value;
            var connectionId = Context.ConnectionId;
            //添加到组
            await Groups.AddToGroupAsync(connectionId, userId);
            
            await base.OnConnectedAsync();
        }
        /// <summary>
        /// 连接关闭
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = Context.User?.FindFirst(s => s.Type == "Id");
            var connectionId = Context.ConnectionId;
            Groups.RemoveFromGroupAsync(connectionId,userId.ToString());
            return base.OnDisconnectedAsync(exception);
        }
        // 定义一个方法供客户端调用
        public async Task SendMessage(string userId, string message)
        {
            // 给指定组发消息
            await Clients.Group(userId).ReceiveMessage(userId, message);
            //给所有人发消息
            await Clients.All.ReceiveMessage(userId, message);
            //给自己发消息
            await Clients.Caller.ReceiveMessage(userId, message);
            //给除了自己的其他所有人
            await Clients.Others.ReceiveMessage(userId, message);
        }
    }
}
