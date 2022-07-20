using Furion;
using Furion.DataEncryption;
using Furion.FriendlyException;
using JiangDuo.Core.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace JiangDuo.Core.Utils
{
    public static class JwtHelper
    {

        public static JwtTokenResult GetJwtToken(SysUser user)
        {
            // token
            var accessToken = JWTEncryption.Encrypt(new Dictionary<string, object>()
            {
                { "UserId", user.Id }, // 存储Id
                { "UserName", user.UserName }, // 存储Id
                { "User",user } 
            });
            // 获取刷新 token
            var refreshToken = JWTEncryption.GenerateRefreshToken(accessToken, 43200); // 第二个参数是刷新 token 的有效期（分钟），默认三十天
            App.HttpContext.Response.Headers["access-token"] = accessToken;
            App.HttpContext.Response.Headers["x-access-token"] = refreshToken;
            return new JwtTokenResult { AccessToken = accessToken, RefreshToken = refreshToken };
        }

        public static long GetUserId()
        {
            var userId = App.User.FindFirst(s => s.Type == "UserId");
            if (userId == null)
            {
                throw Oops.Oh("账号异常");
            }
            return long.Parse(userId.Value);
        }
        public static SysUser GetUserInfo()
        {
            var user = App.User.FindFirst(s => s.Type == "User");
            if (user == null)
            {
                throw Oops.Oh("账号异常");
            }
            return JsonConvert.DeserializeObject<SysUser>(user.Value);
        }
    }


    public class JwtTokenResult
    {
        /// <summary>
        /// 账号token
        /// </summary>
        public string AccessToken { get; set; }
        /// <summary>
        /// 刷新token
        /// </summary>
        public string RefreshToken { get; set; }
    }
}
