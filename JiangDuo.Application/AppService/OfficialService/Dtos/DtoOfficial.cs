using JiangDuo.Core.Models;
using Furion.DatabaseAccessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JiangDuo.Core.Enums;

namespace JiangDuo.Application.AppService.OfficialService.Dto
{
    [Manual]
    public class DtoOfficial : Official
    {
        /// <summary>
        /// 性别
        /// </summary>
        public string SexName => Sex.GetDescription();

    }
}
