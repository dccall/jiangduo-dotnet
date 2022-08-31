using System.ComponentModel;

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