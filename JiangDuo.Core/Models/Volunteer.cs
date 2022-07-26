using JiangDuo.Core.Base;
using JiangDuo.Core.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JiangDuo.Core.Models
{
    /// <summary>
    /// 志愿者名称
    /// </summary>
    [Table("Volunteer")]
    [Index(nameof(Name))]
    public partial class Volunteer:BaseEntity
    {

        /// <summary>
        /// 姓名
        /// </summary>
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public SexEnum Sex { get; set; }
        /// <summary>
        /// 身份证号码
        /// </summary>
        [MaxLength(18)]
        public string Idnumber { get; set; }
        /// <summary>
        /// 户籍
        /// </summary>
        [MaxLength(50)]
        public string Origin { get; set; }
        /// <summary>
        /// 民族
        /// </summary>
        [MaxLength(50)]
        public string Nationality { get; set; }
        /// <summary>
        /// 住址
        /// </summary>
        [MaxLength(255)]
        public string Address { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? Birthday { get; set; }
        /// <summary>
        /// 所属村Id
        /// </summary>
        public long? VillageId { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        [MaxLength(11)]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 微信openid
        /// </summary>
        [MaxLength(50)]
        public string OpenId { get; set; }
        /// <summary>
        /// 政治面貌
        /// </summary>
        [MaxLength(50)]
        public string PoliticalOutlook { get; set; }

        /// <summary>
        /// 所属选区Id
        /// </summary>
        public long? SelectAreaId { get; set; }
    }
}
