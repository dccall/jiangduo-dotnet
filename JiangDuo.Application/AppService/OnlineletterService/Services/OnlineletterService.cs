using JiangDuo.Core.Models;
using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Mapster;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yitter.IdGenerator;
using JiangDuo.Core.Utils;
using JiangDuo.Application.AppService.OnlineletterService.Dto;
using Furion.FriendlyException;
using JiangDuo.Application.AppService.WorkorderService.Dto;

namespace JiangDuo.Application.AppService.OnlineletterService.Services
{
    public class OnlineletterService:IOnlineletterService, ITransient
    {
        private readonly ILogger<OnlineletterService> _logger;
        private readonly IRepository<OnlineLetters> _onlineletterRepository;

        private readonly IRepository<Workorder> _workOrderRepository;
        public OnlineletterService(ILogger<OnlineletterService> logger, IRepository<OnlineLetters> onlineletterRepository, IRepository<Workorder> workOrderRepository)
        {
            _logger = logger;
            _onlineletterRepository = onlineletterRepository;
            _workOrderRepository = workOrderRepository;
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="model">数据</param>
        /// <returns></returns>
        public PagedList<DtoOnlineletter> GetList(DtoOnlineletterQuery model)
        {
            var query = _onlineletterRepository.Where(x => !x.IsDeleted);
            query = query.Where(model.BusinessId!=null, x => x.BusinessId==model.BusinessId);

            //将数据映射到DtoOnlineletter中
            return query.OrderByDescending(s=>s.CreatedTime).ProjectToType<DtoOnlineletter>().ToPagedList(model.PageIndex, model.PageSize);
        }
        /// <summary>
        /// 根据编号查询详情
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<DtoOnlineletter> GetById(long id)
        {
            var entity = await _onlineletterRepository.FindOrDefaultAsync(id);

            var dto = entity.Adapt<DtoOnlineletter>();

            if (dto.WorkOrderId != null)
            {
                var workOrderEntity = _workOrderRepository.FindOrDefault(dto.WorkOrderId);
                dto.WorkOrder = workOrderEntity.Adapt<DtoWorkOrder>();
            }


            return dto;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Insert(DtoOnlineletterForm model)
        {

            var entity = model.Adapt<OnlineLetters>();
            entity.Id = YitIdHelper.NextId();
            entity.CreatedTime = DateTime.Now;
            entity.Creator = JwtHelper.GetAccountId();
            _onlineletterRepository.Insert(entity);
            return await _onlineletterRepository.SaveNowAsync();
        }
     
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(DtoOnlineletterForm model)
        {
            //先根据id查询实体
            var entity = _onlineletterRepository.FindOrDefault(model.Id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            //将模型数据映射给实体属性
            entity = model.Adapt(entity);
            entity.UpdatedTime = DateTime.Now;
            entity.Updater = JwtHelper.GetAccountId();
            _onlineletterRepository.Update(entity);
            return await _onlineletterRepository.SaveNowAsync();
        }
     
        /// <summary>
        /// 假删除
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<int> FakeDelete(long id)
        {
            var entity = _onlineletterRepository.FindOrDefault(id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            entity.IsDeleted = true;
            return await _onlineletterRepository.SaveNowAsync();
        }
        /// <summary>
        /// 批量假删除
        /// </summary>
        /// <param name="idList">编号</param>
        /// <returns></returns>
        public async Task<int> FakeDelete(List<long> idList)
        {
            var result = await _onlineletterRepository.Context.BatchUpdate<Building>()
                .Where(x => idList.Contains(x.Id))
                .Set(x => x.IsDeleted, x => true)
                .ExecuteAsync();
            return result;
        }
    

    }
}
