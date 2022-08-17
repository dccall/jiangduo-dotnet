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
using JiangDuo.Application.AppService.ServiceService.Dtos;
using JiangDuo.Core.Enums;
using JiangDuo.Application.AppService.VenuedeviceService.Services;

namespace JiangDuo.Application.AppService.ServiceService.Services
{
    public class ServiceService : IServiceService, ITransient
    {
        private readonly ILogger<ServiceService> _logger;
        private readonly IRepository<Core.Models.Service> _serviceRepository;
        private readonly IRepository<Workorder> _workOrderRepository;
        private readonly IRepository<Participant> _participantRepository;
        private readonly IRepository<Resident> _residentRepository;
        private readonly IRepository<Venuedevice> _venuedeviceRepository;
        private readonly IVenuedeviceService _venuedeviceService;
        private readonly IRepository<Official> _officialRepository;
        private readonly IRepository<SysUploadFile> _uploadFileRepository;
        public ServiceService(ILogger<ServiceService> logger,
            IRepository<Participant> participantRepository,
            IRepository<Resident> residentRepository,
             IRepository<Official> officialRepository,
             IRepository<Venuedevice> venuedeviceRepository,
             IRepository<SysUploadFile> uploadFileRepository,
             IVenuedeviceService venuedeviceService,
        IRepository<Core.Models.Service> serviceRepository, IRepository<Workorder> workOrderRepository)
        {
            _logger = logger;
            _serviceRepository = serviceRepository;
            _workOrderRepository = workOrderRepository;
            _participantRepository = participantRepository;
            _residentRepository = residentRepository;
            _venuedeviceRepository = venuedeviceRepository;
            _officialRepository = officialRepository;
            _uploadFileRepository = uploadFileRepository;
            _venuedeviceService = venuedeviceService;
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
            query = query.Where(!(model.SelectAreaId == null || model.SelectAreaId == -1), x => x.SelectAreaId == model.SelectAreaId);
            query = query.Where(model.ServiceType != null, x => x.ServiceType == model.ServiceType);
            query = query.Where(model.ServiceClassifyId != null, x => x.ServiceClassifyId == model.ServiceClassifyId.Value);
            query = query.Where(model.Status != null, x => x.Status == model.Status);
            query = query.Where(model.Creator != null, x => x.Creator == model.Creator);
            if (model.Status==null&& model.PageSource == 0)
            {
                var statusList=new List<ServiceStatusEnum>() { 
                 ServiceStatusEnum.Normal,
                 ServiceStatusEnum.Audit, //待审核
                 ServiceStatusEnum.AuditFailed,//审核未通过
                };
                query = query.Where(x=> statusList.Contains(x.Status.Value));
            }
            if (model.Status == null && model.PageSource == 1)
            {
                var statusList = new List<ServiceStatusEnum>() {
                 ServiceStatusEnum.Audited,//审核通过
                 ServiceStatusEnum.Published,//发布
                 ServiceStatusEnum.Ended,//结束
                };
                query = query.Where(x => statusList.Contains(x.Status.Value));
            }


            var query2 = from service in query
                         join official in _officialRepository.Entities on service.OfficialsId equals official.Id  into result1
                         from so in result1.DefaultIfEmpty()
                         join venuedevice in _venuedeviceRepository.Entities on service.VenueDeviceId equals venuedevice.Id into result2
                         from sv in result2.DefaultIfEmpty()
                         orderby  service.CreatedTime descending
                         select new DtoService()
                         {
                             Id = service.Id,
                             ServiceType = service.ServiceType,
                             Address = service.Address,
                             GroupOriented = service.GroupOriented,
                             OfficialsId = service.OfficialsId,
                             OfficialsName = so.Name,
                             PlanEndTime = service.PlanEndTime,
                             PlanNumber = service.PlanNumber,
                             VillagesRange = service.VillagesRange,
                             PlanStartTime = service.PlanStartTime,
                             ServiceClassifyId = service.ServiceClassifyId,
                             ServiceName = service.ServiceName,
                             Attachments = service.Attachments,
                             Remarks = service.Remarks,
                             VenueDeviceId = service.VenueDeviceId,
                             VenueDeviceName = sv.Name,
                             AuditFindings = service.AuditFindings,
                             IsDeleted = service.IsDeleted,
                             Status = service.Status,
                             UpdatedTime = service.UpdatedTime,
                             Updater = service.Updater,
                             Creator = service.Creator,
                             SelectAreaId = service.SelectAreaId,
                             CreatedTime = service.CreatedTime
                         };

            return query2.ToPagedList(model.PageIndex, model.PageSize);
        }
        /// <summary>
        /// 根据编号查询详情
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<DtoService> GetById(long id)
        {

            var query = from service in _serviceRepository.Where(x => x.Id == id)
                        join official in _officialRepository.Entities on service.OfficialsId equals official.Id into result1
                        from so in result1.DefaultIfEmpty()
                        join venuedevice in _venuedeviceRepository.Entities on service.VenueDeviceId equals venuedevice.Id into result2
                        from sv in result2.DefaultIfEmpty()
                        where service.Id == id
                        select new DtoService()
                        {
                            Id = service.Id,
                            ServiceType = service.ServiceType,
                            Address = service.Address,
                            GroupOriented = service.GroupOriented,
                            OfficialsId = service.OfficialsId,
                            OfficialsName = so.Name,
                            PlanEndTime = service.PlanEndTime,
                            PlanNumber = service.PlanNumber,
                            VillagesRange = service.VillagesRange,
                            PlanStartTime = service.PlanStartTime,
                            ServiceClassifyId = service.ServiceClassifyId,
                            ServiceName = service.ServiceName,
                            Attachments = service.Attachments,
                            Remarks = service.Remarks,
                            VenueDeviceId = service.VenueDeviceId,
                            VenueDeviceName = sv.Name,
                            AuditFindings = service.AuditFindings,
                            IsDeleted = service.IsDeleted,
                            Status = service.Status,
                            UpdatedTime = service.UpdatedTime,
                            Updater = service.Updater,
                            Creator = service.Creator,
                            SelectAreaId = service.SelectAreaId,
                            CreatedTime = service.CreatedTime
                        };
            var dto = query.FirstOrDefault();
            var result = _participantRepository
                .Where(x => !x.IsDeleted && x.ServiceId == id)
                .Join(_residentRepository.Where(x => !x.IsDeleted), x => x.ResidentId, y => y.Id, (x, y) => new DtoJoinServiceResident()
                {
                    Resident = y,
                    RegistTime = x.RegistTime,
                    StartTime = x.StartTime,
                    EndTime = x.EndTime,
                }).ToList();
            //获取服务参与人
            dto.JoinServiceResident = result;

            if (!string.IsNullOrEmpty(dto.Attachments))
            {
                var idList = dto.Attachments.Split(',').ToList();
                dto.AttachmentsFiles = _uploadFileRepository.Where(x => idList.Contains(x.FileId.ToString())).ToList();
            }
            if (dto.VenueDeviceId != null)
            {
                dto.Venuedevice = await _venuedeviceService.GetById(dto.VenueDeviceId.Value);
            }

            return await Task.FromResult(dto);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Insert(DtoServiceForm model)
        {
            var account = JwtHelper.GetAccountInfo();
            var entity = model.Adapt<Core.Models.Service>();
            entity.Id = YitIdHelper.NextId();
            entity.CreatedTime = DateTime.Now;
            entity.Creator = account.Id;
            entity.Status = ServiceStatusEnum.Audit;

            if (model.AttachmentsFiles != null && model.AttachmentsFiles.Any())
            {
                entity.Attachments = String.Join(",", model.AttachmentsFiles.Select(x => x.FileId));
            }

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
            var account = JwtHelper.GetAccountInfo();
            //将模型数据映射给实体属性
            entity = model.Adapt(entity);
            entity.UpdatedTime = DateTime.Now;
            entity.Updater = account.Id;

            if (model.AttachmentsFiles != null && model.AttachmentsFiles.Any())
            {
                entity.Attachments = String.Join(",", model.AttachmentsFiles.Select(x => x.FileId));
            }
            _serviceRepository.Update(entity);
            return await _serviceRepository.SaveNowAsync();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> UpdateStatus(DtoUpdateServiceStatus model)
        {
            //先根据id查询实体
            var entity = _serviceRepository.FindOrDefault(model.ServiceId);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            entity.Status = model.Status;
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
            //if (entity.Status != ServiceStatusEnum.AuditFailed)
            //{
            //    throw Oops.Oh("当前状态无法删除");
            //}
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
