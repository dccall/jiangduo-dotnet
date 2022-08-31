using System.ComponentModel.DataAnnotations;

namespace JiangDuo.Application.AppService.OfficialsstructService.Dto
{
    public class DtoOfficialsstructForm
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long? Id { get; set; }

        /// <summary>
        /// 人大结构名称
        /// </summary>
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        /// <summary>
        /// 父级
        /// </summary>
        public long? ParentId { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [MaxLength(50)]
        public string Remarks { get; set; }
    }
}