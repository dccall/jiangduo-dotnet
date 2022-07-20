using JiangDuo.Core.Base;
using JiangDuo.Core.Enums;
using Mapster;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JiangDuo.Application.User.Dtos;

/// <summary>
/// 用户列表参数
/// </summary>
public class DtoUserForm
{
    /// <summary>
    /// 主键
    /// </summary>
    public long? Id { get;  set; }
    /// <summary>
    /// 部门ID
    /// </summary>
    public long DeptId { get; set; }
    /// <summary>
    /// 用户账号
    /// </summary>
    [MaxLength(50)]
    public string UserName { get; set; } = null!;
    /// <summary>
    /// 用户昵称
    /// </summary>
    [MaxLength(50)]
    public string NickName { get; set; } = null!;
    /// <summary>
    /// 用户类型
    /// </summary>
    public UserType Type { get; set; }
    /// <summary>
    /// 用户邮箱
    /// </summary>
    [MaxLength(100)]
    public string Email { get; set; }
    /// <summary>
    /// 手机号码
    /// </summary>
    [MaxLength(11)]
    public string Phonenumber { get; set; }
    /// <summary>
    /// 用户性别（ 0未知 1男 2女）
    /// </summary>
    public UserSex Sex { get; set; }
    /// <summary>
    /// 头像地址
    /// </summary>
    [MaxLength(255)]
    public string Avatar { get; set; }
    /// <summary>
    /// 密码
    /// </summary>
    [MaxLength(100)]
    public string PassWord { get; set; }
    /// <summary>
    /// 帐号状态（0正常 1停用）
    /// </summary>
    public UserStatus Status { get; set; }
    /// <summary>
    /// 备注
    /// </summary>
    [MaxLength(500)]
    public string Remark { get; set; }


    /// <summary>
    /// 角色id集合
    /// </summary>
    public List<long> RoleIdList { get; set; }

}