using Furion.DatabaseAccessor;
using JiangDuo.Core.Enums;
using JiangDuo.Core.Models;

namespace JiangDuo.Application.System.DictItem.Dto
{
    [Manual]
    public class DictItemDto : SysDictItem
    {
        /// <summary>
        /// 状态名称
        /// </summary>
        public string StatusName => Status.GetDescription();
    }
}