using JiangDuo.Application.System.Notice.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.System.Notice.Services
{
    public interface INoticeService
    {
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public PagedList<NoticeDto> GetList(NoticeRequest model);
        //public List<MenuTreeDto> GetTreeMenu();
        /// <summary>
        /// 根据id查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<NoticeDto> GetById(long id);
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Insert(DtoNoticeForm model);
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Insert(List<DtoNoticeForm> model);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Update(DtoNoticeForm model);
        /// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Update(List<DtoNoticeForm> model);
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
