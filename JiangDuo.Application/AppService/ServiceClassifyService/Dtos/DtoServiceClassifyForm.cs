using System.ComponentModel.DataAnnotations;

namespace JiangDuo.Application.AppService.ServiceClassifyService.Dto
{
    public class DtoServiceClassifyForm
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long? Id { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        [MaxLength(50)]
        public string ClassifyName { get; set; }
    }
}