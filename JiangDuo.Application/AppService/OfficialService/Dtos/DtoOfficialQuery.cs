using JiangDuo.Core.Base;
using JiangDuo.Core.Enums;

namespace JiangDuo.Application.AppService.OfficialService.Dto
{
    public class DtoOfficialQuery : BaseRequest
    {
        /// <summary>
        /// 所属选区Id
        /// </summary>
        public long? SelectAreaId { get; set; }

        /// <summary>
        /// 职务
        /// </summary>
        public string Post { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public OfficialType? Type { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        public string CategoryId { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// 人大角色
        /// </summary>
        public OfficialRoleEnum? OfficialRole { get; set; }
    }
}