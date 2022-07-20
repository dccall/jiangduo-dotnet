using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.Account.Dtos
{
    /// <summary>
    /// 修改密码参数
    /// </summary>
    public class DtoUpdatePassword
    {
        /// <summary>
        /// 原密码
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "原密码")]
        public string OldPassWord { get; set; }
        /// <summary>
        /// 新密码
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "新密码")]
        public string NewPassWord { get; set; }
        /// <summary>
        /// 确认密码
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "确认密码")]
        public string ConfirmPassWord { get; set; }
    }
}
