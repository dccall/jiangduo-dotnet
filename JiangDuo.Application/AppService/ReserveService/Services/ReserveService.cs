using JiangDuo.Application.System.Config.Dto;
using JiangDuo.Application.Tools;
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
using JiangDuo.Application.AppService.BuildingService.Dto;
using JiangDuo.Application.AppService.ReserveService.Dto;
using Furion.FriendlyException;
using JiangDuo.Application.AppService.WorkorderService.Dto;
using JiangDuo.Core.Enums;

namespace JiangDuo.Application.AppService.ReserveService.Services
{
    public class ReserveService:IReserveService, ITransient
    {
        private readonly ILogger<ReserveService> _logger;
        private readonly IRepository<Reserve> _reserveRepository;
        private readonly IRepository<Workorder> _workOrderRepository;
        public ReserveService(ILogger<ReserveService> logger, IRepository<Reserve> reserveRepository, IRepository<Workorder> workOrderRepository)
        {
            _logger = logger;
            _reserveRepository = reserveRepository;
            _workOrderRepository = workOrderRepository;
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="model">数据</param>
        /// <returns></returns>
        public PagedList<DtoReserve> GetList(DtoReserveQuery model)
        {
            var query = _reserveRepository.Where(x => !x.IsDeleted);
            query = query.Where(!string.IsNullOrEmpty(model.Theme), x => x.Theme.Contains(model.Theme));

            //将数据映射到DtoReserve中
            return query.OrderByDescending(s=>s.CreatedTime).ProjectToType<DtoReserve>().ToPagedList(model.PageIndex, model.PageSize);
        }
        /// <summary>
        /// 根据编号查询详情
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<DtoReserve> GetById(long id)
        {
            var entity = await _reserveRepository.FindOrDefaultAsync(id);
            var dto = entity.Adapt<DtoReserve>();
            //志愿者

            return dto;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Insert(DtoReserveForm model)
        {
            var entity = model.Adapt<Reserve>();
            entity.Id = YitIdHelper.NextId();
            entity.CreatedTime = DateTime.Now;
            entity.Creator = JwtHelper.GetAccountId();
            entity.Status = ReserveStatus.Audit;//审核中
            _reserveRepository.Insert(entity);
            return await _reserveRepository.SaveNowAsync();
        }
     
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(DtoReserveForm model)
        {
            //先根据id查询实体
            var entity = _reserveRepository.FindOrDefault(model.Id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            //将模型数据映射给实体属性
            entity = model.Adapt(entity);
            entity.UpdatedTime = DateTime.Now;
            entity.Updater = JwtHelper.GetAccountId();
            _reserveRepository.Update(entity);
            return await _reserveRepository.SaveNowAsync();
        }
        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> UpdateStatus(DtoReserveQueryStatus model)
        {
            //先根据id查询实体
            var entity = _reserveRepository.FindOrDefault(model.ReserveId);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            entity.Status = model.Status;
            _reserveRepository.Update(entity);
            return await _reserveRepository.SaveNowAsync();
        }
        /// <summary>
        /// 假删除
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<int> FakeDelete(long id)
        {
            var entity = _reserveRepository.FindOrDefault(id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            entity.IsDeleted = true;
            return await _reserveRepository.SaveNowAsync();
        }
        /// <summary>
        /// 批量假删除
        /// </summary>
        /// <param name="idList">编号</param>
        /// <returns></returns>
        public async Task<int> FakeDelete(List<long> idList)
        {
            var result = await _reserveRepository.Context.BatchUpdate<Reserve>()
                .Where(x => idList.Contains(x.Id))
                .Set(x => x.IsDeleted, x => true)
                .ExecuteAsync();
            return result;
        }
    

    }
}
