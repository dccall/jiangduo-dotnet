using Furion;
using Furion.InstantMessaging;
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
        /// <summary>
        /// 建立连接
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            var userId = "";
            var connectionId = Context.ConnectionId;
            var userIdClaim = Context.User?.FindFirst(s => s.Type == "UserId");
            if (userIdClaim != null)
            {
                userId=userIdClaim.Value;
            }
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
            //给指定用户发消息
            await Clients.User(userId).ReceiveMessage2(userId, message);
            //给指定用户发消息
            await Clients.Users(userId, userId, userId).ReceiveMessage2(userId, message);
        }
    }
}
