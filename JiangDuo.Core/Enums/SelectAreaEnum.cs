using System.ComponentModel;

namespace JiangDuo.Core.Enums
{
    public enum SelectAreaTypeEnum
    {
        [Description("默认")]
        Normal = 0,

        [Description("区选区")]
        SelectArea = 1,

        [Description("镇选区")]
        Town = 2,

        [Description("其它")]
        Other = 3
    }
}