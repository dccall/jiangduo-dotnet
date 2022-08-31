using JiangDuo.Application.AppService.SelectAreaService.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.SelectAreaService.Services
{
    public interface ISelectAreaService
    {
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public PagedList<DtoSelectArea> GetList(DtoSelectAreaQuery model);

        /// <summary>
        /// 根据id查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<DtoSelectArea> GetById(long id);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Insert(DtoSelectAreaForm model);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Update(DtoSelectAreaForm model);

        /// <summary>
        /// 假删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<int> FakeDelete(long id);

        //批量假删除
        public Task<int> FakeDelete(List<long> idList);
    }
}