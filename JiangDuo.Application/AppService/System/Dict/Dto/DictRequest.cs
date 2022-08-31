using JiangDuo.Core.Base;

namespace JiangDuo.Application.System.Dict.Dto
{
    public class DictRequest : BaseRequest
    {
        /// <summary>
        /// 字典名称
        /// </summary>
        public string DictName { get; set; }
    }
}