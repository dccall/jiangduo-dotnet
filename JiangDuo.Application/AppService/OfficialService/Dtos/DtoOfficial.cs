using Furion.DatabaseAccessor;
using JiangDuo.Core.Enums;
using JiangDuo.Core.Models;
using System.Collections.Generic;

namespace JiangDuo.Application.AppService.OfficialService.Dto
{
    [Manual]
    public class DtoOfficial : Official
    {
        /// <summary>
        /// 性别
        /// </summary>
        public string SexName => Sex.GetDescription();

        /// <summary>
        /// 角色名称
        /// </summary>
        public string OfficialRoleName => OfficialRole.GetDescription();

        /// <summary>
        /// 肖像
        /// </summary>
        public List<SysUploadFile> AvatarList { get; set; } = new List<SysUploadFile>();
    }
}