using JiangDuo.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.Menu.Dtos
{
    public class MenuRequest: RequestBase
    {
        /// <summary>
        /// 菜单/按钮标题
        /// </summary>
        public  string Title { get; set; }

    }
}
