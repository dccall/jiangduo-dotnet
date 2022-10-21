using Furion.FriendlyException;
using JiangDuo.Core.Enums;
using Npoi.Mapper.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.OfficialService.Dtos;

public class DtoOfficialImport
{
    ///// <summary>
    ///// 编号
    ///// </summary>
    //[Column("编号")]
    //public string Id { get; set; }
    /// <summary>
    /// 姓名
    /// </summary>
    [Column("姓名")]
    public string Name { get; set; }

    /// <summary>
    /// 性别
    /// </summary>
    [Ignore]
    public SexEnum Sex
    {
        get
        {

            if (!string.IsNullOrEmpty(SexName) )
            {
                return SexName == "男" ? SexEnum.Male : SexEnum.Female;
            }
            return SexEnum.Male;
        }
    }

    /// <summary>
    /// 性别
    /// </summary>
    [Column("性别:男|女")]
    public string SexName { get; set; }

    ///// <summary>
    ///// 类型
    ///// </summary>
    //[Ignore]
    //public OfficialType Type
    //{
    //    get
    //    {

    //        if (!string.IsNullOrEmpty(TypeName) )
    //        {
    //            return TypeName.Contains("区") ? OfficialType.Area : OfficialType.Town;
    //        }
    //        return OfficialType.Normal;
    //    }
    //}

    /// <summary>
    /// 类型
    /// </summary>
    [Column("类型:区代表,镇代表")]
    public string Type { get; set; }

    /// <summary>
    /// 类别
    /// </summary>
    [Column("类别")]
    public string CategoryId { get; set; }

    /// <summary>
    /// 生日
    /// </summary>
    [Column("生日", CustomFormat = "yyyy-MM-dd")]
    public DateTime? Birthday { get; set; }

    /// <summary>
    /// 民族
    /// </summary>
    [Column("民族")]
    public string Nationality { get; set; }

    /// <summary>
    /// 文化程度
    /// </summary>
    [Column("文化程度")]
    public string CulturalLevel { get; set; }

    /// <summary>
    /// 党派
    /// </summary>
    [Column("党派")]
    public string Party { get; set; }

    /// <summary>
    /// 职务
    /// </summary>
    [Column("职务")]
    public string Post { get; set; }

    /// <summary>
    /// 身份证号码
    /// </summary>
    [Column("身份证号码")]
    public string Idnumber { get; set; }

    /// <summary>
    /// 手机号码
    /// </summary>
    [Column("手机号码")]
    public string PhoneNumber { get; set; }

    /// <summary>
    /// 所属选区Id
    /// </summary>
    [Column("所属选区Id")]
    public string SelectAreaId { get; set; }

    /// <summary>
    /// 个人履历
    /// </summary>
    [Column("个人履历")]
    public string PersonalResume { get; set; }

    /// <summary>
    /// 住址
    /// </summary>
    [Column("住址")]
    public string Address { get; set; }

    /// <summary>
    /// 政治面貌
    /// </summary>
    [Column("政治面貌")]
    public string PoliticalOutlook { get; set; }


    ///// <summary>
    ///// 头像ID
    ///// </summary>
    //[Column("头像ID")]
    //public string Avatar { get; set; }
}
