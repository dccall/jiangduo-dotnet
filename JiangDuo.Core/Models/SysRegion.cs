using Furion.DatabaseAccessor;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JiangDuo.Core.Models
{
    [Table("sys_region")]
    public partial class SysRegion : IEntity
    {
        [Key]
        public long RegionId { get; set; }

        /// <summary>
        /// 地区名称
        /// </summary>
        [MaxLength(50)]
        public string RegionName { get; set; } = null!;

        /// <summary>
        /// 地区缩写
        /// </summary>
        [MaxLength(50)]
        public string RegionShortName { get; set; }

        /// <summary>
        /// 行政地区编号
        /// </summary>
        [MaxLength(50)]
        public string RegionCode { get; set; }

        /// <summary>
        /// 地区父id
        /// </summary>
        public long? RegionParentId { get; set; }

        /// <summary>
        /// 地区级别 1-省、自治区、直辖市 2-地级市、地区、自治州、盟 3-市辖区、县级市、县
        /// </summary>
        public int? RegionLevel { get; set; }
    }
}