using Furion.FriendlyException;
using JiangDuo.Core.Base;
using JiangDuo.Core.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JiangDuo.Core.Models
{
    /// <summary>
    /// 人大名单
    /// </summary>
    [Table("Official")]
    [Index(nameof(Name))]
    public partial class Official : BaseEntity
    {
        /// <summary>
        /// 姓名
        /// </summary>
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        /// <summary>
        /// 性别
        /// </summary>
        public SexEnum Sex { get; set; }

        /// <summary>
        /// 类别
        /// </summary>
        [MaxLength(20)]
        public string CategoryId { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 民族
        /// </summary>
        [MaxLength(50)]
        public string Nationality { get; set; }

        /// <summary>
        /// 文化程度
        /// </summary>
        [MaxLength(20)]
        public string CulturalLevel { get; set; }

        /// <summary>
        /// 党派
        /// </summary>
        [MaxLength(50)]
        public string Party { get; set; }

        /// <summary>
        /// 职务
        /// </summary>
        public long Post { get; set; }

        /// <summary>
        /// 身份证号码
        /// </summary>
        [MaxLength(18)]
        public string Idnumber { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [MaxLength(11)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 所属选区Id
        /// </summary>
        public long? SelectAreaId { get; set; }

        /// <summary>
        /// 所属村Id
        /// </summary>
        public long? VillageId { get; set; }

        /// <summary>
        /// 微信OpenId
        /// </summary>
        [MaxLength(50)]
        public string OpenId { get; set; }

        /// <summary>
        /// 个人履历
        /// </summary>
        public string PersonalResume { get; set; }

        /// <summary>
        /// 住址
        /// </summary>
        [MaxLength(300)]
        public string Address { get; set; }

        /// <summary>
        /// 政治面貌
        /// </summary>
        [MaxLength(50)]
        public string PoliticalOutlook { get; set; }

        /// <summary>
        /// 积分
        /// </summary>
        public long Score { get; set; }

        /// <summary>
        /// 人大结构Id
        /// </summary>
        //public long? OfficialsstructId { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public OfficialRoleEnum OfficialRole { get; set; }

        /// <summary>
        /// 肖像
        /// </summary>
        [MaxLength(255)]
        public string Avatar { get; set; }



        public void ValidationNullOrEmpty(int index = 0)
        {
            if (string.IsNullOrEmpty(this.Name))
            {
                throw Oops.Oh($"第{index}行姓名不能为空");
            }
            if (string.IsNullOrEmpty(this.CategoryId))
            {
                throw Oops.Oh($"第{index}行类别不能为空");
            }
            if (string.IsNullOrEmpty(this.Idnumber))
            {
                throw Oops.Oh($"第{index}行身份证号码不能为空");
            }
            if (string.IsNullOrEmpty(this.PhoneNumber))
            {
                throw Oops.Oh($"第{index}行手机号码不能为空");
            }
            if (this.Post == null)
            {
                throw Oops.Oh($"第{index}行职务不能为空");
            }
        }
    }
}