using JiangDuo.Application.AppService.OnlineletterService.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.OnlineletterService.Services
{
    public interface IOnlineletterService
    {
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public PagedList<DtoOnlineletter> GetList(DtoOnlineletterQuery model);

        /// <summary>
        /// 根据id查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<DtoOnlineletter> GetById(long id);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Insert(DtoOnlineletterForm model);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Update(DtoOnlineletterForm model);

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