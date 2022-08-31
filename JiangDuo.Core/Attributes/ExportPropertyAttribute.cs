using System;

namespace JiangDuo.Core.Attributes
{
    public class ExportPropertyAttribute : Attribute
    {
        public int MaxLength { get; set; }
        public bool IsRequired { get; set; }
        public bool CanExport { get; set; }
        public string Name { get; set; }
        public EntityPropertyFormat Format { get; set; }

        public ExportPropertyAttribute(string name)
        {
            Name = name;
            MaxLength = 0;
            IsRequired = false;
            CanExport = true;
            Format = EntityPropertyFormat.Normal;
        }
    }

    public enum EntityPropertyFormat
    {
        Normal = 0,
        Tel = 1
    }
}