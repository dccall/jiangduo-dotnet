using JiangDuo.Core.Base;

namespace JiangDuo.Application.System.Dept.Dtos
{
    public class DeptRequest : BaseRequest
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