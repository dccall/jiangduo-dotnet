using JiangDuo.Application.AppService.WorkOrderService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JiangDuo.Application.AppService.WorkorderService.Dto;

namespace JiangDuo.Application.AppService.WorkOrderService.Services
{
    public interface IWorkOrderService
    {
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public PagedList<DtoWorkOrder> GetList(DtoWorkOrderQuery model);
        /// <summary>
        /// 根据id查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<DtoWorkOrder> GetById(long id);
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Insert(DtoWorkOrderForm model);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Update(DtoWorkOrderForm model);
        /// <summary>
        /// 假删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<int> FakeDelete(long id);
        //批量假删除
        public Task<int> FakeDelete(List<long> idList);

        /// <summary>
        /// 工单指派
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public  Task<string> WorkOrderAssign(DtoWorkOrderAssign model);
        /// <summary>
        /// 工单完成
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public  Task<string> WorkOrderCompleted(DtoWorkOrderCompletedHandel model);
        /// <summary>
        /// 工单完结（已完成待审核 工单【管理员】）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public  Task<string> WorkOrderEnd(DtoWorkOrderEndHandel model);
        /// <summary>
        /// 工单状态变更
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<string> WorkOrderHandel(DtoWorkOrderHandel model);
    }
}
