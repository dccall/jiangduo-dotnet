using System.ComponentModel.DataAnnotations;

namespace JiangDuo.Application.Account.Dtos
{
    /// <summary>
    /// 用户登录参数
    /// </summary>
    public class DtoLoginRequest
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "密码")]
        public string PassWord { get; set; }
    }
}