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
    public class WorkOrderService : IWorkOrderService, ITransient
    {
        private readonly ILogger<WorkOrderService> _logger;
        private readonly IRepository<Workorder> _workOrderRepository;
        private readonly IRepository<Reserve> _reserveRepository;
        private readonly IRepository<Core.Models.Service> _serviceRepository;
        private readonly IRepository<OnlineLetters> _onlineletterRepository;
        private readonly IRepository<Volunteer> _volunteerRepository;
        private readonly IRepository<Reservevolunteer> _workOrderVolunteerRepository;
        private readonly IRepository<SysUploadFile> _uploadRepository;
        private readonly IRepository<Workorderlog> _workOrderLog;
        private readonly IRepository<Resident> _residentRepository;
        private readonly IRepository<Official> _officialRepository;
        private readonly IRepository<SysUser> _userRepository;
        private readonly IRepository<Workorderfeedback> _workorderfeedbackRepository;
        private readonly IRepository<SelectArea> _selectAreaRepository;
        private readonly IRepository<PublicSentiment> _publicSentimentRepository;
        private readonly IRepository<SysUploadFile> _uploadFileRepository;
        public WorkOrderService(ILogger<WorkOrderService> logger, IRepository<Workorder> workOrderRepository,
            IRepository<Reserve> reserveRepository,
            IRepository<Core.Models.Service> serviceRepository,
            IRepository<OnlineLetters> onlineletterRepository,
            IRepository<Reservevolunteer> workOrderVolunteerRepository,
            IRepository<Volunteer> volunteerRepository,
            IRepository<SysUploadFile> uploadRepository,
            IRepository<Workorderlog> workOrderLog,
            IRepository<Resident> residentRepository,
            IRepository<Official> officialRepository,
            IRepository<SysUser> userRepository,
            IRepository<Workorderfeedback> workorderfeedbackRepository,
            IRepository<SelectArea> selectAreaRepository,
            IRepository<PublicSentiment> publicSentimentRepository,
            IRepository<SysUploadFile> uploadFileRepository
            )
        {
            _logger = logger;
            _workOrderRepository = workOrderRepository;
            _reserveRepository = reserveRepository;
            _serviceRepository = serviceRepository;
            _onlineletterRepository = onlineletterRepository;
            _volunteerRepository = volunteerRepository;
            _workOrderVolunteerRepository = workOrderVolunteerRepository;
            _uploadRepository = uploadRepository;
            _workOrderLog = workOrderLog;
            _residentRepository = residentRepository;
            _officialRepository = officialRepository;
            _userRepository = userRepository;
            _workorderfeedbackRepository = workorderfeedbackRepository;
            _selectAreaRepository = selectAreaRepository;
            _publicSentimentRepository = publicSentimentRepository;
            _uploadFileRepository = uploadFileRepository;
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="model">数据</param>
        /// <returns></returns>
        public PagedList<DtoWorkOrder> GetList([FromQuery] DtoWorkOrderQuery model)
        {
            var query = _workOrderRepository.Where(x => !x.IsDeleted);
            query = query.Where(!string.IsNullOrEmpty(model.WorkOrderNo), x => x.WorkOrderNo == model.WorkOrderNo);
            query = query.Where(model.WorkorderType != null, x => x.WorkorderType == model.WorkorderType);
            query = query.Where(model.WorkorderSource != null, x => x.WorkorderSource == model.WorkorderSource);
            query = query.Where(model.Status != null, x => x.Status == model.Status);
            query = query.Where(model.StartTime != null, x => x.CreatedTime >= model.StartTime);
            query = query.Where(model.EndTime != null, x => x.CreatedTime <= model.EndTime);

            var pageList = query.OrderByDescending(s => s.CreatedTime).ProjectToType<DtoWorkOrder>().ToPagedList(model.PageIndex, model.PageSize);

            if (pageList.Items.Count() > 0)
            {
                var idList = pageList.Items.Select(x => x.SelectAreaId).Distinct().ToList();
                var list = _selectAreaRepository.Where(x => idList.Contains(x.Id)).ToList();
                foreach (var item in pageList.Items)
                {
                    var entity = list.Where(x => x.Id == item.SelectAreaId).FirstOrDefault();
                    item.SelectAreaName = entity?.SelectAreaName;
                }
            }
            //将数据映射到DtoWorkOrder中
            return query.OrderByDescending(s => s.CreatedTime).ProjectToType<DtoWorkOrder>().ToPagedList(model.PageIndex, model.PageSize);
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

            if (!string.IsNullOrEmpty(dto.Attachments))
            {
                var idList = dto.Attachments.Split(',').ToList();
                dto.AttachmentsList = _uploadFileRepository.Where(x => idList.Contains(x.FileId.ToString())).ToList();
            }
            //工单日志
            dto.Workorderlogs = _workOrderLog.Where(x => x.WordOrderId == dto.Id).OrderByDescending(x => x.LogTime).ToList();
            //工单反馈信息
            dto.WorkorderfeedbackList = _workorderfeedbackRepository.Where(x => x.WordOrderId == dto.Id).OrderByDescending(x => x.CreatedTime).ToList();

            return dto;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Insert(DtoWorkOrderForm model)
        {
            var account = JwtHelper.GetAccountInfo();
            var entityWorkOrder = model.Adapt<Workorder>();
            entityWorkOrder.Id = YitIdHelper.NextId();
            entityWorkOrder.WorkOrderNo = GetWorkOrderNo();
            entityWorkOrder.OriginatorId = account.Id; //发起人是自己
            entityWorkOrder.OriginatorName = GetPersonnelName(entityWorkOrder.OriginatorId);
            entityWorkOrder.Status = WorkorderStatusEnum.NotProcessed;//待处理
            entityWorkOrder.CreatedTime = DateTime.Now;
            entityWorkOrder.Creator = account.Id;
            if (model.AttachmentsList != null && model.AttachmentsList.Any())
            {
                entityWorkOrder.Attachments = String.Join(",", model.AttachmentsList.Select(x => x.FileId));
            }
            _workOrderRepository.Insert(entityWorkOrder);

            var publicSentimentEntity = await _publicSentimentRepository.FindOrDefaultAsync(model.PublicSentimentId);
            if (publicSentimentEntity != null)
            {
                //将关联的需求状态改为进行中
                publicSentimentEntity.Status = PublicSentimentStatus.InProgress;
                _publicSentimentRepository.UpdateInclude(publicSentimentEntity, new[] { nameof(publicSentimentEntity.Status) });
            }

            AddWordOrderLog(entityWorkOrder.Id, "工单创建");

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

            if (model.AttachmentsList != null && model.AttachmentsList.Any())
            {
                entity.Attachments = String.Join(",", model.AttachmentsList.Select(x => x.FileId));
            }

            _workOrderRepository.Update(entity);
            AddWordOrderLog(entity.Id, "工单修改");
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




        /// <summary>
        /// 工单指派
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> WorkOrderAssign(DtoWorkOrderAssign model)
        {
            var workOrderEntity = await _workOrderRepository.FindOrDefaultAsync(model.WorkOrderId);
            if (workOrderEntity == null)
            {
                throw Oops.Oh("工单不存在");
            }
            workOrderEntity.RecipientId = model.RecipientId;
            workOrderEntity.RecipientName = GetPersonnelName(model.RecipientId);
            if (workOrderEntity.Status == WorkorderStatusEnum.NotProcessed)
            {
                workOrderEntity.StartTime = DateTime.Now; //工单开始时间
                workOrderEntity.Status = WorkorderStatusEnum.InProgress; //进行中
            }

            await _workOrderRepository.UpdateNowAsync(workOrderEntity);

            //添加日志
            AddWordOrderLog(workOrderEntity.Id, "工单指派给了" + workOrderEntity.RecipientName);
            return "已指派";
        }
        /// <summary>
        /// 工单完成
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> WorkOrderCompleted(DtoWorkOrderCompletedHandel model)
        {
            var workOrderEntity = await _workOrderRepository.FindOrDefaultAsync(model.WordOrderId);
            if (workOrderEntity == null)
            {
                throw Oops.Oh("工单不存在");
            }
            workOrderEntity.Status = WorkorderStatusEnum.Completed;//完成待审核
            await _workOrderRepository.UpdateNowAsync(workOrderEntity);

            //添加反馈内容
            AddWordOrderFeedback(workOrderEntity.Id, model.FeedbackContent, workOrderEntity.Status);

            //添加日志
            AddWordOrderLog(workOrderEntity.Id, "完成工单");

            return "已完成";
        }
        /// <summary>
        /// 工单完结（已完成待审核 工单【管理员】）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> WorkOrderEnd(DtoWorkOrderEndHandel model)
        {
            var workOrderEntity = await _workOrderRepository.FindOrDefaultAsync(model.WordOrderId);
            if (workOrderEntity == null)
            {
                throw Oops.Oh("工单不存在");
            }
            workOrderEntity.Status = WorkorderStatusEnum.End;//已完结
            workOrderEntity.OverTime = DateTime.Now;//已完结
            await _workOrderRepository.UpdateNowAsync(workOrderEntity);

            //添加反馈内容
            AddWordOrderFeedback(workOrderEntity.Id, model.FeedbackContent, workOrderEntity.Status);
            //添加日志
            AddWordOrderLog(workOrderEntity.Id, "工单完结");

            //处理关联的用户需求，如果完结时自动给予反馈
            var psEntity = _publicSentimentRepository.FindOrDefault(workOrderEntity.PublicSentimentId);
            if (psEntity != null)
            {
                var account = JwtHelper.GetAccountInfo();
                psEntity.FeedbackPersonId = account.Id;//反馈人Id
                psEntity.FeedbackPerson = account.Name;//反馈人
                psEntity.FeedbackContent = model.FeedbackContent;//反馈内容
                psEntity.FeedbackTime = DateTime.Now;//反馈时间
                psEntity.Status = PublicSentimentStatus.Feedback;
            }

            return "已完结";
        }
        public Task<string> WorkOrderHandel(DtoWorkOrderHandel model)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 添加工单日志
        /// </summary>
        /// <param name="workOrderId"></param>
        /// <param name="content"></param>
        /// <param name="status"></param>
        private void AddWordOrderFeedback(long workOrderId, string content, WorkorderStatusEnum status)
        {
            var account = JwtHelper.GetAccountInfo();
            Workorderfeedback entity = new Workorderfeedback();
            entity.Id = YitIdHelper.NextId();
            entity.WordOrderId = workOrderId;
            entity.HandlerName = account.Name;
            entity.HandlerTime = DateTime.Now;
            entity.HandlerContent = content;
            entity.HandlerStatus = status;
            entity.CreatedTime = DateTime.Now;
            entity.Creator = account.Id;
            _workorderfeedbackRepository.InsertNow(entity);
        }

        /// <summary>
        /// 添加工单日志
        /// </summary>
        /// <param name="workOrderId"></param>
        /// <param name="content"></param>
        private void AddWordOrderLog(long workOrderId, string content)
        {
            var account = JwtHelper.GetAccountInfo();
            Workorderlog logEntity = new Workorderlog();
            logEntity.Id = YitIdHelper.NextId();
            logEntity.LogTime = DateTime.Now;
            logEntity.WordOrderId = workOrderId;
            logEntity.LogContent = content;
            logEntity.CreatedTime = DateTime.Now;
            logEntity.Creator = account.Id;
            logEntity.Operator = account.Name;
            _workOrderLog.InsertNow(logEntity);
        }

        private string GetPersonnelName(long? id)
        {
            if (id == null)
            {
                return "";
            }
            //var residentEntity=  _residentRepository.FindOrDefault(id);
            //if (residentEntity != null)
            //{
            //    return residentEntity.Name;
            //}
            var officialEntity = _officialRepository.FindOrDefault(id);
            if (officialEntity != null)
            {
                return officialEntity.Name;
            }
            var sysUserEntity = _userRepository.FindOrDefault(id);
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
