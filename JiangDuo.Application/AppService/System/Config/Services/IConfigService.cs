using JiangDuo.Application.System.Config.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.System.Config.Services
{
    public interface IConfigService
    {
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public PagedList<ConfigDto> GetList(ConfigRequest model);
        /// <summary>
        /// 根据id查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ConfigDto> GetById(long id);
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Insert(DtoConfigForm model);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Update(DtoConfigForm model);
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
