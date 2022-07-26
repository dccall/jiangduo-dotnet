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
        private readonly IRepository<SysUploadFile> _uploadRepository;
        private readonly IRepository<Workorderlog> _workOrderLog;
        private readonly IRepository<Resident> _residentRepository;
        private readonly IRepository<Official> _officialRepository;
        private readonly IRepository<SysUser> _userRepository;
        public WorkOrderService(ILogger<WorkOrderService> logger, IRepository<Workorder> workOrderRepository,
            IRepository<Reserve> reserveRepository,
            IRepository<Service> serviceRepository,
            IRepository<OnlineLetters> onlineletterRepository,
            IRepository<Workordervolunteer> workOrderVolunteerRepository,
            IRepository<Volunteer> volunteerRepository,
            IRepository<SysUploadFile> uploadRepository,
            IRepository<Workorderlog> workOrderLog,
            IRepository<Resident> residentRepository,
            IRepository<Official> officialRepository,
            IRepository<SysUser> userRepository
            )
        {
            _logger = logger;
            _workOrderRepository = workOrderRepository;
            _reserveRepository = reserveRepository;
            _serviceRepository= serviceRepository;
            _onlineletterRepository = onlineletterRepository;
            _volunteerRepository = volunteerRepository;
            _workOrderVolunteerRepository = workOrderVolunteerRepository;
            _uploadRepository = uploadRepository;
            _workOrderLog = workOrderLog;
            _residentRepository = residentRepository;
            _officialRepository = officialRepository;
            _userRepository = userRepository;
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
                if (!string.IsNullOrEmpty(dto.OnlineLetters.Attachments))
                {
                    //附件处理
                    var fileIdList = dto.OnlineLetters.Attachments.Split(",").ToList();
                    dto.OnlineLetters.AttachmentsFiles = _uploadRepository.Where(x => fileIdList.Contains(x.FileId.ToString())).ToList();
                }
               
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
            entityWorkOrder.OriginatorId = JwtHelper.GetAccountId(); //发起人是自己
            entityWorkOrder.OriginatorName = GetPersonnelName(entityWorkOrder.OriginatorId);
            entityWorkOrder.ReceiverName = GetPersonnelName(entityWorkOrder.ReceiverId);
            entityWorkOrder.Status = WorkorderStatusEnum.NotProcessed;//待处理
            entityWorkOrder.CreatedTime = DateTime.Now;
            entityWorkOrder.Creator = JwtHelper.GetAccountId();
            // 引用的业务id
            var relationId = YitIdHelper.NextId();
            //有事好商量（预约）
            if (model.WorkorderType== WorkorderTypeEnum.Reserve)
            {
                var reserveEntity=  model.Reserve.Adapt<Reserve>();
                reserveEntity.Id = relationId;//与工单表关联
                reserveEntity.CreatedTime = DateTime.Now;
                reserveEntity.Creator = JwtHelper.GetAccountId();
                reserveEntity.WorkOrderId = entityWorkOrder.Id;
                _reserveRepository.Insert(reserveEntity);

                entityWorkOrder.Content = reserveEntity.Theme;//工单内容
            }
            //一老一少（服务活动）
            if (model.WorkorderType == WorkorderTypeEnum.Service)
            {
                var reviceEntity = model.Service.Adapt<Service>();
                reviceEntity.Id = relationId;//与工单表关联
                reviceEntity.CreatedTime = DateTime.Now;
                reviceEntity.Creator = JwtHelper.GetAccountId();
                reviceEntity.WorkOrderId = entityWorkOrder.Id;
                _serviceRepository.Insert(reviceEntity);

                entityWorkOrder.Content = reviceEntity.ServiceName;//工单内容
            }
            //码上说马上办
            if (model.WorkorderType == WorkorderTypeEnum.OnlineLetters&& model.Reserve!=null)
            {
                var onlineLettersEntity = model.OnlineLetters.Adapt<OnlineLetters>();
                onlineLettersEntity.Id = relationId;//与工单表关联
                onlineLettersEntity.CreatedTime = DateTime.Now;
                onlineLettersEntity.Creator = JwtHelper.GetAccountId();
                onlineLettersEntity.WorkOrderId = entityWorkOrder.Id;
                //附件处理
                var fileIdList= model.OnlineLetters.AttachmentsFiles.Select(x => x.FileId).ToList();
                onlineLettersEntity.Attachments=String.Join(",", fileIdList);
                _onlineletterRepository.Insert(onlineLettersEntity);

                entityWorkOrder.Content = onlineLettersEntity.Content;//工单内容
            }

            entityWorkOrder.RelationId = relationId; //业务表关联Id
            _workOrderRepository.Insert(entityWorkOrder);

            AddWordOrderLog(entityWorkOrder.Id,"工单创建");


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
            entity.UpdatedTime = DateTime.Now;
            entity.Updater = JwtHelper.GetAccountId();
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
            var result = await _workOrderRepository.Context.BatchUpdate<Workorder>()
                .Where(x => idList.Contains(x.Id))
                .Set(x => x.IsDeleted, x => true)
                .ExecuteAsync();
            return result;
        }


        public async Task<string> WorkOrderOrderAssign(DtoWorkOrderAssign model)
        {
            var workOrderEntity= await _workOrderRepository.FindOrDefaultAsync(model.Id);
            if (workOrderEntity == null)
            {
                throw Oops.Oh("工单不存在");
            }
            workOrderEntity.ReceiverId = model.ReceiverId;
            workOrderEntity.ReceiverName =GetPersonnelName(model.ReceiverId);
            workOrderEntity.Status = WorkorderStatusEnum.InProgress; //进行中
           
            //添加日志
            AddWordOrderLog(workOrderEntity.Id, "工单指派给了" + workOrderEntity.ReceiverName);

            return "已指派";
        }


        /// <summary>
        /// 添加工单日志
        /// </summary>
        /// <param name="workOrderId"></param>
        /// <param name="content"></param>
        private void AddWordOrderLog(long workOrderId,string content)
        {
            Workorderlog logEntity=new Workorderlog();
            logEntity.Id = YitIdHelper.NextId();
            logEntity.LogTime =DateTime.Now;
            logEntity.WordOrderId = workOrderId;
            logEntity.LogContent = content;

            _workOrderLog.InsertNow(logEntity);
        }

        private string GetPersonnelName(long? id)
        {
            if (id == null)
            {
                return "";
            }
            var residentEntity=  _residentRepository.FindOrDefault(id);
            if (residentEntity != null)
            {
                return residentEntity.Name;
            }
            var officialEntity = _officialRepository.FindOrDefault(id);
            if (officialEntity != null)
            {
                return officialEntity.Name;
            }
            var sysUserEntity=_userRepository.FindOrDefault(id);
            if (sysUserEntity != null)
            {
                return sysUserEntity.NickName;
            }
            return "";

        }
        private string GetWorkOrderNo()
        {
            return DateTime.UtcNow.ToString("yyyyMMddHHmmssfff");
        }
    }
}
