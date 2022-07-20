using JiangDuo.Core.Base;
using JiangDuo.Core;
using JiangDuo.Core.Enums;
using JiangDuo.Core.Models;
using Furion.DatabaseAccessor;
using Furion.DataValidation;
using Mapster;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JiangDuo.Application.User.Dtos;

/// <summary>
/// 用户数据传输对象
/// </summary>
[Manual]

public class DtoUser : SysUser
{
    /// <summary>
    /// 用户账号
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [Display(Name = "用户账号")]
    public new string UserName { get; set; }
    /// <summary>
    /// 用户邮箱
    /// </summary>
    [DataValidation(ValidationTypes.EmailAddress)]
    [Display(Name = "邮箱")]
    public new string Email { get; set; }
    /// <summary>
    /// 手机号码
    /// </summary>
    [DataValidation(ValidationTypes.PhoneOrTelNumber)]
    [Display(Name = "手机号码")]
    public new string Phonenumber { get; set; }
    /// <summary>
    /// 密码
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [Display(Name = "密码")]
    public new string PassWord { get; set; }
    /// <summary>
    /// 帐号状态（0正常 1停用）
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [Display(Name = "状态")]
    public new UserStatus? Status { get; set; }

    /// <summary>
    /// 角色id集合
    /// </summary>
    public List<long> RoleIdList { get; set; }
    /// <summary>
    /// 状态名称
    /// </summary>
    public string StatusName => Status.GetDescription();
    /// <summary>
    /// 性别名称
    /// </summary>
    public string SexName => Status.GetDescription();
    /// <summary>
    /// 用户类型名称
    /// </summary>
    public string UserTypeName => Type.GetDescription();


}