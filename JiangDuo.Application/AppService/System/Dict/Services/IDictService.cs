using JiangDuo.Application.System.Dict.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JiangDuo.Application.System.Dict.Services
{
    public interface IDictService
    {
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public PagedList<DictDto> GetList(DictRequest model);

        //public List<MenuTreeDto> GetTreeMenu();
        /// <summary>
        /// 根据id查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<DictDto> GetById(long id);

        /// <summary>
        /// 根据名称查询详情
        /// </summary>
        /// <param name="dictName">dictName</param>
        /// <returns></returns>
        public Task<DictDto> GetByDictName(string dictName);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Insert(DtoDictForm model);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Update(DtoDictForm model);

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