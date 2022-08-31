using JiangDuo.Application.AppService.QrcodeService.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.QrcodeService.Services
{
    public interface IQrcodeService
    {
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public PagedList<DtoQrcode> GetList(DtoQrcodeQuery model);

        /// <summary>
        /// 根据id查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<DtoQrcode> GetById(long id);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Insert(DtoQrcodeForm model);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Update(DtoQrcodeForm model);

        /// <summary>
        /// 假删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<int> FakeDelete(long id);

        /// <summary>
        /// 批量假删除
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        public Task<int> FakeDelete(List<long> idList);
    }
}