using JiangDuo.Application.System.DictItem.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.System.DictItem.Services
{
    public interface IDictItemService
    {
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public PagedList<DictItemDto> GetList(DictItemRequest model);
        //public List<MenuTreeDto> GetTreeMenu();
        /// <summary>
        /// 根据id查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<DictItemDto> GetById(long id);
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Insert(DtoDictItemForm model);
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Insert(List<DtoDictItemForm> model);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Update(DtoDictItemForm model);
        /// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Update(List<DtoDictItemForm> model);
        /// <summary>
        /// 假删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<int> FakeDelete(long id);
        //批量假删除
        public Task<int> FakeDelete(List<long> idList);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> Delete(long id);
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        Task<int> Delete(List<long> idList);


    }
}
