using Furion.DataValidation;
using System.ComponentModel.DataAnnotations;

namespace JiangDuo.Application.AppletAppService.ResidentApplet.Dtos
{
    public class DtoAccountCertified
    {
        /// <summary>
        /// 姓名
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "姓名")]
        public string Name { get; set; } = null!;

        /// <summary>
        /// 身份证号码
        /// </summary>
        [DataValidation(ValidationTypes.IDCard, ErrorMessage = "请输入正确的身份证号码")]
        [Display(Name = "身份证号码")]
        public string Idnumber { get; set; }

        /// <summary>
        /// 所属村Id
        /// </summary>
        public long VillageId { get; set; }
    }
}