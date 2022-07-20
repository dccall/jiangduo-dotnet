using JiangDuo.Core;
using JiangDuo.Core.Enums;
using JiangDuo.Core.Models;
using Furion.DatabaseAccessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.System.Dept.Dtos
{
    [Manual]
    public class DeptDto:SysDept
    {
        public new string DeptName { get; set; }
        public string ParentName { get; set; }
    }
}
