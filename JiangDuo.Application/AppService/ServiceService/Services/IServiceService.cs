using JiangDuo.Application.AppService.ServiceService.Dto;
using JiangDuo.Application.AppService.ServiceService.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.ServiceService.Services
{
    public interface IServiceService
    {
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public PagedList<DtoService> GetList(DtoServiceQuery model);

        /// <summary>
        /// 根据id查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<DtoService> GetById(long id);

        /// <summary>
        /// 根据服务/活动Id和日期 查询报名/预约情况
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<List<DtoParticipant>> GetServiceRegistList(DtoServiceSubscribeQuery model);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Insert(DtoServiceForm model);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Update(DtoServiceForm model);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<int> UpdateStatus(DtoUpdateServiceStatus model);

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