using Furion.DatabaseAccessor;
using JiangDuo.Core.Enums;
using JiangDuo.Core.Models;

namespace JiangDuo.Application.AppService.VolunteerService.Dto
{
    [Manual]
    public class DtoVolunteer : Volunteer
    {
        /// <summary>
        /// 性别
        /// </summary>
        public string SexName => Sex.GetDescription();
    }
}