using Furion.DatabaseAccessor;
using JiangDuo.Core.Enums;
using JiangDuo.Core.Models;

namespace JiangDuo.Application.AppService.ResidentService.Dto
{
    [Manual]
    public class DtoResident : Resident
    {
        /// <summary>
        /// 状态
        /// </summary>
        public string StatusName => Status.GetDescription();

        /// <summary>
        /// 性别
        /// </summary>
        public string SexName => Sex.GetDescription();

        /// <summary>
        /// 所属村
        /// </summary>
        public string VillageName { get; set; }

        /// <summary>
        /// 输入选区Id
        /// </summary>
        public string SelectAreaName { get; set; }
    }
}