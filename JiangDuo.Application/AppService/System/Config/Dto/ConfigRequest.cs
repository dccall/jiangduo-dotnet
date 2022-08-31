using JiangDuo.Core.Base;

namespace JiangDuo.Application.System.Config.Dto
{
    public class ConfigRequest : BaseRequest
    {
        /// <summary>
        /// 参数名称
        /// </summary>
        public string ConfigName { get; set; }
    }
}