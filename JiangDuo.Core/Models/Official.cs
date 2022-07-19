using Furion.DatabaseAccessor;
using JiangDuo.Core.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JiangDuo.Core.Models
{
    /// <summary>
    /// 人大名单
    /// </summary>
    [Table("Official")]
    public partial class Official : BaseEntity
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// 性别
        /// </summary>
        public int Sex { get; set; }
       /// <summary>
       /// 类别
       /// </summary>
        public long? CategoryId { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTimeOffset? Birthday { get; set; }
        /// <summary>
        /// 名族
        /// </summary>
        public string Nationality { get; set; }
        /// <summary>
        /// 文化程度
        /// </summary>
        public long? CulturalLevel { get; set; }
        /// <summary>
        /// 党派
        /// </summary>
        public string Party { get; set; }
        /// <summary>
        /// 职务
        /// </summary>
        public string Post { get; set; }
        /// <summary>
        /// 身份证号码
        /// </summary>
        public string Idnumber { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 所属选区Id
        /// </summary>
        public long? AreaId { get; set; }
        /// <summary>
        /// 微信OpenId
        /// </summary>
        public string OpenId { get; set; }
        /// <summary>
        /// 个人履历
        /// </summary>
        public string PersonalResume { get; set; }
    }
}
