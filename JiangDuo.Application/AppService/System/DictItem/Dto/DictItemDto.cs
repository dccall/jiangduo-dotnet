using JiangDuo.Core.Enums;
using JiangDuo.Core.Models;
using Furion.DatabaseAccessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.System.DictItem.Dto
{
    [Manual]
    public class DictItemDto:SysDictItem
    {
        /// <summary>
        /// 状态名称
        /// </summary>
        public string StatusName => Status.GetDescription();
    }
}
