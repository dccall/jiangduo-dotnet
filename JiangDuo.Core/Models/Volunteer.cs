using JiangDuo.Core.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JiangDuo.Core.Models
{
    /// <summary>
    /// 志愿者名称
    /// </summary>
    [Table("Volunteer")]
    public partial class Volunteer:BaseEntity
    {

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public int Sex { get; set; }
        /// <summary>
        /// 身份证号码
        /// </summary>
        public string Idnumber { get; set; }
        /// <summary>
        /// 户籍
        /// </summary>
        public string Origin { get; set; }
        /// <summary>
        /// 民族
        /// </summary>
        public string Nationality { get; set; }
        /// <summary>
        /// 住址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTimeOffset? Birthday { get; set; }
        /// <summary>
        /// 所属村Id
        /// </summary>
        public long? VillageId { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 微信openid
        /// </summary>
        public string OpenId { get; set; }
        /// <summary>
        /// 政治面貌
        /// </summary>
        public string PoliticalOutlook { get; set; }
    }
}
