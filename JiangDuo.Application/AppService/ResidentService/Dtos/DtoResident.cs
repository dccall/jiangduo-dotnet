using JiangDuo.Core.Models;
using Furion.DatabaseAccessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JiangDuo.Core.Enums;

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
