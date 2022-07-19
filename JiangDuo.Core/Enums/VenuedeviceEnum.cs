using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Core.Enums
{
    public enum VenuedeviceEnum
    {

        [Description("默认")]
        Normal = 0,
        [Description("场地")]
        Venue = 1,
        [Description("设备")]
        Device = 2
    }
}
