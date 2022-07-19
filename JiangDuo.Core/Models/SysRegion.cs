using System;
using System.Collections.Generic;

namespace JiangDuo.Core.Models
{
    public partial class SysRegion
    {
        public long RegionId { get; set; }
        public string RegionName { get; set; } = null!;
        public string RegionShortName { get; set; }
        public string RegionCode { get; set; }
        public long? RegionParentId { get; set; }
        public int? RegionLevel { get; set; }
    }
}
