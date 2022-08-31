using JiangDuo.Core.Base;
using JiangDuo.Core.Enums;
using JiangDuo.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JiangDuo.Application.AppService.OfficialService.Dto
{
    public class DtoOfficialForm : BaseRequest
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long? Id { get; set; }

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
        public long? CategoryId { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 名族
        /// </summary>
        [MaxLength(50)]
        public string Nationality { get; set; }

        /// <summary>
        /// 文化程度
        /// </summary>
        public long? CulturalLevel { get; set; }

        /// <summary>
        /// 党派
        /// </summary>
        [MaxLength(50)]
        public string Party { get; set; }

        /// <summary>
        /// 职务
        /// </summary>
        [MaxLength(50)]
        public string Post { get; set; }

        /// <summary>
        /// 身份证号码
        /// </summary>
        [MaxLength(18)]
        public string Idnumber { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 所属选区Id
        /// </summary>
        public long? SelectAreaId { get; set; }

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
        public int Score { get; set; }

        /// <summary>
        /// 人大结构Id
        /// </summary>
        public long? OfficialsstructId { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public OfficialRoleEnum OfficialRole { get; set; }

        /// <summary>
        /// 肖像
        /// </summary>
        [MaxLength(255)]
        public string Avatar { get; set; }

        /// <summary>
        /// 肖像
        /// </summary>

        public List<SysUploadFile> AvatarList { get; set; } = new List<SysUploadFile>();
    }
}