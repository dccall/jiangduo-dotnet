using JiangDuo.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.System.Post.Dto
{
    public class PostRequest: RequestBase
    {
        /// <summary>
        ///  职位名称
        /// </summary>
        public string PostName { get; set; }
    }
}
