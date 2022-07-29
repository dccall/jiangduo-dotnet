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
using JiangDuo.Application.AppService.ServiceService.Dto;
using Furion.FriendlyException;
using JiangDuo.Application.AppService.WorkorderService.Dto;

namespace JiangDuo.Application.AppService.ServiceService.Services
{
    public class ServiceService:IServiceService, ITransient
    {
        private readonly ILogger<ServiceService> _logger;
        private readonly IRepository<Core.Models.Service> _serviceRepository;
        private readonly IRepository<Workorder> _workOrderRepository;
        private readonly IRepository<Participant> _participantRepository;
        private readonly IRepository<Resident> _residentRepository;
        public ServiceService(ILogger<ServiceService> logger,
            IRepository<Participant> participantRepository,
            IRepository<Resident> residentRepository,
            IRepository<Core.Models.Service> serviceRepository, IRepository<Workorder> workOrderRepository)
        {
            _logger = logger;
            _serviceRepository = serviceRepository;
            _workOrderRepository = workOrderRepository;
            _participantRepository = participantRepository;
            _residentRepository = residentRepository;
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="model">数据</param>
        /// <returns></returns>
        public PagedList<DtoService> GetList(DtoServiceQuery model)
        {
            var query = _serviceRepository.Where(x => !x.IsDeleted);
            query = query.Where(!string.IsNullOrEmpty(model.ServiceName), x => x.ServiceName.Contains(model.ServiceName));

            //将数据映射到DtoService中
            return query.OrderByDescending(s=>s.CreatedTime).ProjectToType<DtoService>().ToPagedList(model.PageIndex, model.PageSize);
        }
        /// <summary>
        /// 根据编号查询详情
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<DtoService> GetById(long id)
        {
            var entity = await _serviceRepository.FindOrDefaultAsync(id);
            var dto = entity.Adapt<DtoService>();
            var idList = _participantRepository.Where(x => !x.IsDeleted && x.ServiceId == id).Select(x => x.ResidentId).ToList();

            //获取服务参与人
            dto.JoinList = _residentRepository.Where(x => idList.Contains(x.Id)).ToList();

            return dto;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Insert(DtoServiceForm model)
        {
            var entity = model.Adapt<Core.Models.Service>();
            entity.Id = YitIdHelper.NextId();
            entity.CreatedTime = DateTime.Now;
            entity.Creator = JwtHelper.GetAccountId();
            _serviceRepository.Insert(entity);
            return await _serviceRepository.SaveNowAsync();
        }
     
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(DtoServiceForm model)
        {
            //先根据id查询实体
            var entity = _serviceRepository.FindOrDefault(model.Id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            //将模型数据映射给实体属性
            entity = model.Adapt(entity);
            entity.UpdatedTime = DateTime.Now;
            entity.Updater = JwtHelper.GetAccountId();
            _serviceRepository.Update(entity);
            return await _serviceRepository.SaveNowAsync();
        }
     
        /// <summary>
        /// 假删除
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<int> FakeDelete(long id)
        {
            var entity = _serviceRepository.FindOrDefault(id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            entity.IsDeleted = true;
            return await _serviceRepository.SaveNowAsync();
        }
        /// <summary>
        /// 批量假删除
        /// </summary>
        /// <param name="idList">编号</param>
        /// <returns></returns>
        public async Task<int> FakeDelete(List<long> idList)
        {
            var result = await _serviceRepository.Context.BatchUpdate<Core.Models.Service>()
                .Where(x => idList.Contains(x.Id))
                .Set(x => x.IsDeleted, x => true)
                .ExecuteAsync();
            return result;
        }
    

    }
}
