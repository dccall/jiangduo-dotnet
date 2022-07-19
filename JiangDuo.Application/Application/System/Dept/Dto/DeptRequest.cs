using JiangDuo.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.System.Dept.Dtos
{
    public class DeptRequest: BaseRequest
    {
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName { get; set; }
        /// <summary>
        /// 部门编号
        /// </summary>
        public string DeptCode { get; set; }

    }
}
