using Furion.DatabaseAccessor;
using JiangDuo.Core.Models;

namespace JiangDuo.Application.System.Dept.Dtos
{
    [Manual]
    public class DeptDto : SysDept
    {
        public new string DeptName { get; set; }
        public string ParentName { get; set; }
    }
}