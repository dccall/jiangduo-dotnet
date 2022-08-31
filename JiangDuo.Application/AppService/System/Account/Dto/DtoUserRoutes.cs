using JiangDuo.Application.Menu.Dtos;
using System.Collections.Generic;

namespace JiangDuo.Application.Account.Dtos
{
    public class DtoUserRoutes
    {
        /// <summary>
        /// 首页
        /// </summary>
        public string Home { get; set; }

        /// <summary>
        /// 路由列表
        /// </summary>
        public List<MenuTreeDto> Routes { get; set; }

        /// <summary>
        /// 权限code
        /// </summary>
        public List<string> Codes { get; set; }
    }
}