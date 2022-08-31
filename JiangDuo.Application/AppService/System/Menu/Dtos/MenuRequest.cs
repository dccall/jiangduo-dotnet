using JiangDuo.Core.Base;

namespace JiangDuo.Application.Menu.Dtos
{
    public class MenuRequest : BaseRequest
    {
        /// <summary>
        /// 菜单/按钮标题
        /// </summary>
        public string Title { get; set; }
    }
}