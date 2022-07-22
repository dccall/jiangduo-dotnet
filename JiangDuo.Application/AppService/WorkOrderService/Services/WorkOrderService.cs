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
using JiangDuo.Application.AppService.WorkOrderService.Dto;
using Furion.FriendlyException;
using JiangDuo.Application.AppService.WorkorderService.Dto;
using Microsoft.AspNetCore.Mvc;
using JiangDuo.Core.Enums;
using JiangDuo.Application.AppService.ReserveService.Dto;
using JiangDuo.Application.AppService.ServiceService.Dto;
using JiangDuo.Application.AppService.OnlineletterService.Dto;
using JiangDuo.Application.AppService.VolunteerService.Dto;

namespace JiangDuo.Application.AppService.WorkOrderService.Services
{
    public class WorkOrderService:IWorkOrderService, ITransient
    {
        private readonly ILogger<WorkOrderService> _logger;
        private readonly IRepository<Workorder> _workOrderRepository;
        private readonly IRepository<Reserve> _reserveRepository;
        private readonly IRepository<Service> _serviceRepository;
        private readonly IRepository<OnlineLetters> _onlineletterRepository;
        private readonly IRepository<Volunteer> _volunteerRepository;
        private readonly IRepository<Workordervolunteer> _workOrderVolunteerRepository;
        
        public WorkOrderService(ILogger<WorkOrderService> logger, IRepository<Workorder> workOrderRepository,
            IRepository<Reserve> reserveRepository,
            IRepository<Service> serviceRepository,
            IRepository<OnlineLetters> onlineletterRepository,
            IRepository<Workordervolunteer> workOrderVolunteerRepository,
            IRepository<Volunteer> volunteerRepository
            )
        {
            _logger = logger;
            _workOrderRepository = workOrderRepository;
            _reserveRepository = reserveRepository;
            _serviceRepository= serviceRepository;
            _onlineletterRepository = onlineletterRepository;
            _volunteerRepository = volunteerRepository;
            _workOrderVolunteerRepository = workOrderVolunteerRepository;
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="model">数据</param>
        /// <returns></returns>
        public PagedList<DtoWorkOrder> GetList([FromQuery] DtoWorkOrderQuery model)
        {
            var query = _workOrderRepository.Where(x => !x.IsDeleted);
            query = query.Where(model.WorkorderType != null, x => x.WorkorderType == model.WorkorderType);
            query = query.Where(model.Status!=null, x => x.Status == model.Status);
            query = query.Where(model.StartTime!=null, x => x.StartTime>= model.StartTime);
            query = query.Where(model.EndTime!=null, x => x.StartTime <= model.EndTime);


            //将数据映射到DtoWorkOrder中
            return query.OrderByDescending(s=>s.CreatedTime).ProjectToType<DtoWorkOrder>().ToPagedList(model.PageIndex, model.PageSize);
        }
        /// <summary>
        /// 根据id查询详情
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public async Task<DtoWorkOrder> GetById(long id)
        {
            var entity = await _workOrderRepository.FindOrDefaultAsync(id);

            var dto = entity.Adapt<DtoWorkOrder>();
            if (dto.RelationId!=null&& dto.WorkorderType == WorkorderTypeEnum.Reserve)
            {
                var reserveEntity= _reserveRepository.FindOrDefault(dto.RelationId);
                dto.Reserve= reserveEntity.Adapt<DtoReserveForm>();
            }
            if (dto.RelationId != null && dto.WorkorderType == WorkorderTypeEnum.Reserve)
            {
                var serviceEntity = _serviceRepository.FindOrDefault(dto.RelationId);
                dto.Service = serviceEntity.Adapt<DtoServiceForm>();
            }
            if (dto.RelationId != null && dto.WorkorderType == WorkorderTypeEnum.Reserve)
            {
                var onlineletterEntity = _onlineletterRepository.FindOrDefault(dto.RelationId);
                dto.OnlineLetters = onlineletterEntity.Adapt<DtoOnlineletterForm>();
            }
            //获取志愿者信息
            var volunteerIdList= _workOrderVolunteerRepository.Where(x => !x.IsDeleted && x.WordOrderId == dto.Id).Select(x=>x.VolunteerId).ToList();
            dto.Volunteers = _volunteerRepository.Where(x => !x.IsDeleted && volunteerIdList.Contains(x.Id)).ProjectToType<DtoVolunteer>().ToList();

            return dto;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Insert(DtoWorkOrderForm model)
        {
            var entityWorkOrder = model.Adapt<Workorder>();
            entityWorkOrder.Id = YitIdHelper.NextId();
            entityWorkOrder.WorkOrderNo = GetWorkOrderNo();
            entityWorkOrder.CreatedTime = DateTimeOffset.UtcNow;
            entityWorkOrder.Creator = JwtHelper.GetUserId();
            // 引用的业务id
            var relationId = YitIdHelper.NextId();
            //有事好商量（预约）
            if (model.WorkorderType== WorkorderTypeEnum.Reserve)
            {
                var reserveEntity=  model.Reserve.Adapt<Reserve>();
                reserveEntity.Id = relationId;//与工单表关联
                reserveEntity.CreatedTime = DateTimeOffset.UtcNow;
                reserveEntity.Creator = JwtHelper.GetUserId();
                reserveEntity.WorkOrderId = entityWorkOrder.Id;
                _reserveRepository.Insert(reserveEntity);
            }
            //一老一少（服务活动）
            if (model.WorkorderType == WorkorderTypeEnum.Service)
            {
                var reviceEntity = model.Service.Adapt<Service>();
                reviceEntity.Id = relationId;//与工单表关联
                reviceEntity.CreatedTime = DateTimeOffset.UtcNow;
                reviceEntity.Creator = JwtHelper.GetUserId();
                reviceEntity.WorkOrderId = entityWorkOrder.Id;
                _serviceRepository.Insert(reviceEntity);
            }
            //码上说马上办
            if (model.WorkorderType == WorkorderTypeEnum.OnlineLetters&& model.Reserve!=null)
            {
                var onlineLettersEntity = model.OnlineLetters.Adapt<OnlineLetters>();
                onlineLettersEntity.Id = relationId;//与工单表关联
                onlineLettersEntity.CreatedTime = DateTimeOffset.UtcNow;
                onlineLettersEntity.Creator = JwtHelper.GetUserId();
                onlineLettersEntity.WorkOrderId = entityWorkOrder.Id;
                _onlineletterRepository.Insert(onlineLettersEntity);
            }

            entityWorkOrder.RelationId = relationId; //业务表关联Id
            _workOrderRepository.Insert(entityWorkOrder);
            return await _workOrderRepository.SaveNowAsync();
        }
     
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(DtoWorkOrderForm model)
        {
            //先根据id查询实体
            var entity = _workOrderRepository.FindOrDefault(model.Id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            //将模型数据映射给实体属性
            entity = model.Adapt(entity);
            entity.UpdatedTime = DateTimeOffset.UtcNow;
            entity.Updater = JwtHelper.GetUserId();
            _workOrderRepository.Update(entity);
            return await _workOrderRepository.SaveNowAsync();
        }
     
        /// <summary>
        /// 假删除
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<int> FakeDelete(long id)
        {
            var entity = _workOrderRepository.FindOrDefault(id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            entity.IsDeleted = true;
            return await _workOrderRepository.SaveNowAsync();
        }
        /// <summary>
        /// 批量假删除
        /// </summary>
        /// <param name="idList">编号</param>
        /// <returns></returns>
        public async Task<int> FakeDelete(List<long> idList)
        {
            var result = await _workOrderRepository.Context.BatchUpdate<Building>()
                .Where(x => idList.Contains(x.Id))
                .Set(x => x.IsDeleted, x => true)
                .ExecuteAsync();
            return result;
        }


        public async Task<int> WorkOrderOrderAssign(long id)
        {
            return 1;
        }

        private string GetWorkOrderNo()
        {
            return "";
        }
    }
}
