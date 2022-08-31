using JiangDuo.Core.Base;
using System.ComponentModel.DataAnnotations;

namespace JiangDuo.Application.AppService.ServiceClassifyService.Dto
{
    public class DtoServiceClassifyQuery : BaseRequest
    {
        /// <summary>
        /// 分类名称
        /// </summary>
        [MaxLength(50)]
        public string ClassifyName { get; set; }
    }
}