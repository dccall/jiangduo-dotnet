using Furion.DatabaseAccessor;
using JiangDuo.Core.Base;
using JiangDuo.Core.Enums;
using JiangDuo.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppletAppService.ResidentApplet.Dtos
{

    [Manual]
    public class DtoServiceInfo : Core.Models.Service
    {
        /// <summary>
        /// 状态
        /// </summary>
        public string StatusName => Status.GetDescription();

    }
}
