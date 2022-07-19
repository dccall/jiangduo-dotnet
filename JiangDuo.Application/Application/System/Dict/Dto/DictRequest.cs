using JiangDuo.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
