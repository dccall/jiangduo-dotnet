using System.ComponentModel.DataAnnotations;

namespace JiangDuo.Application.AppService.NewsclassifyService.Dto
{
    public class DtoNewsclassifyForm
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long? Id { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        [MaxLength(50)]
        public string ClassifyName { get; set; } = null!;
    }
}