using JiangDuo.Core.Base;

namespace JiangDuo.Application.AppService.ResidentService.Dto
{
    public class DtoResidentQuery : BaseRequest
    {
        /// <summary>
        /// 所属村Id
        /// </summary>
        public long? VillageId { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; } = null!;
    }
}