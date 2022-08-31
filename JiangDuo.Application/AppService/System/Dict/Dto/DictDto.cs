using Furion.DatabaseAccessor;
using JiangDuo.Core.Enums;
using JiangDuo.Core.Models;

namespace JiangDuo.Application.System.Dict.Dto
{
    [Manual]
    public class DictDto : SysDict
    {
        /// <summary>
        /// 状态名称
        /// </summary>
        public string StatusName => Status.GetDescription();
    }
}