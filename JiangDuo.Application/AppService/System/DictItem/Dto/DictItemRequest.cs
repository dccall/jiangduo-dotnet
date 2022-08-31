using JiangDuo.Core.Base;
using JiangDuo.Core.Enums;

namespace JiangDuo.Application.System.DictItem.Dto
{
    public class DictItemRequest : BaseRequest
    {
        /// <summary>
        /// 字典id
        /// </summary>
        public long? DictId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public DictStatus? Status { get; set; }
    }
}