using Furion;
using Furion.DataEncryption;
using Furion.FriendlyException;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;

namespace JiangDuo.Core.Utils
{
    public static class JwtHelper
    {
        public static JwtTokenResult GetJwtToken(AccountModel model)
        {
            // token
            var accessToken = JWTEncryption.Encrypt(new Dictionary<string, object>()
            {
                { "Id", model.Id }, // 存储Id
                { "Name", model.Name }, // 存储名称
                { "AccountModel",model }
            });
            // 获取刷新 token
            var refreshToken = JWTEncryption.GenerateRefreshToken(accessToken, 43200); // 第二个参数是刷新 token 的有效期（分钟），默认三十天
            App.HttpContext.Response.Headers["access-token"] = accessToken;
            App.HttpContext.Response.Headers["x-access-token"] = refreshToken;
            return new JwtTokenResult { AccessToken = accessToken, RefreshToken = refreshToken };
        }

        public static long GetAccountId()
        {
            var id = App.User.FindFirst(s => s.Type == "Id");
            if (id == null)
            {
                throw Oops.Oh("账号异常");
            }
            return long.Parse(id.Value);
        }

        public static AccountModel GetAccountInfo()
        {
            var user = App.User.FindFirst(s => s.Type == "AccountModel");
            if (user == null)
            {
                throw Oops.Oh("账号异常");
            }
            return JsonConvert.DeserializeObject<AccountModel>(user.Value);
        }
    }

    public class AccountModel
    {
        /// <summary>
        /// id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 选区
        /// </summary>
        public long SelectAreaId { get; set; }

        /// <summary>
        /// 账号类型
        /// </summary>
        public AccountType Type { get; set; }
    }

    public enum AccountType
    {
        [Description("系统")]
        System = 0,

        [Description("居民")]
        Resident = 1,

        [Description("人大")]
        Official = 2,

        [Description("志愿者")]
        Volunteer = 3,
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