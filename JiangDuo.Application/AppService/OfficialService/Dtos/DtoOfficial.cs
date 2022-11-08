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
        /// 类型
        /// </summary>
        public string TypeName {
            get {
                if (!string.IsNullOrEmpty(Type))
                {
                    string type = Type;
                    type = type.Replace("1", "区");
                    type = type.Replace("2", "镇");
                    return type;
                }
                return "";
            }
        }

        /// <summary>
        /// 父级选区名称
        /// </summary>
        public string ParentAreaName { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string OfficialRoleName => OfficialRole.GetDescription();

        /// <summary>
        /// 肖像
        /// </summary>
        public List<SysUploadFile> AvatarList { get; set; } = new List<SysUploadFile>();


        /// <summary>
        /// 完结工单数量
        /// </summary>
        public int OverOrderCount { get; set; } = 0;
    }
}