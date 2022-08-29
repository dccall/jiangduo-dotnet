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
using JiangDuo.Application.AppService.VolunteerService.Dto;
using JiangDuo.Application.AppService.VenuedeviceService.Services;

namespace JiangDuo.Application.AppService.ReserveService.Services
{
    public class ReserveService : IReserveService, ITransient
    {
        private readonly ILogger<ReserveService> _logger;
        private readonly IRepository<Reserve> _reserveRepository;
        private readonly IRepository<Workorder> _workOrderRepository;
        private readonly IRepository<Venuedevice> _venuedeviceRepository;
        private readonly IRepository<Reservevolunteer> _reservevolunteerRepository;
        private readonly IRepository<Volunteer> _volunteerRepository;
        private readonly IVenuedeviceService _venuedeviceService;

        public ReserveService(ILogger<ReserveService> logger, IRepository<Reserve> reserveRepository,
            IRepository<Venuedevice> venuedeviceRepository,
            IRepository<Reservevolunteer> reservevolunteerRepository,
            IRepository<Volunteer> volunteerRepository,
            IVenuedeviceService venuedeviceService,
            IRepository<Workorder> workOrderRepository)
        {
            _logger = logger;
            _reserveRepository = reserveRepository;
            _workOrderRepository = workOrderRepository;
            _venuedeviceRepository = venuedeviceRepository;
            _reservevolunteerRepository = reservevolunteerRepository;
            _volunteerRepository =volunteerRepository;
            _venuedeviceService = venuedeviceService;
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
            query = query.Where(!(model.SelectAreaId == null|| model.SelectAreaId==-1), x => x.SelectAreaId == model.SelectAreaId);
            query = query.Where(model.Status != null, x => x.Status == model.Status);
            query = query.Where(model.Creator != null, x => x.Creator == model.Creator);
            query = query.Where(model.StartTime != null, x => x.ReserveDate >= model.StartTime);
            query = query.Where(model.EndTime != null, x => x.ReserveDate <= model.EndTime);
            var query2 = from x in query
                         join venuedevice in _venuedeviceRepository.Entities on x.VenueDeviceId equals venuedevice.Id into result1
                         from rv in result1.DefaultIfEmpty()
                         orderby x.CreatedTime descending
                         select new DtoReserve()
                         {
                             Id = x.Id,
                             Theme = x.Theme,
                             Number = x.Number,
                             ReserveDate = x.ReserveDate,
                             StartTime = x.StartTime,
                             EndTime = x.EndTime,
                             MeetingResults = x.MeetingResults,
                             Remarks = x.Remarks,
                             VenueDeviceId = x.VenueDeviceId,
                             VenueDeviceName = rv.Name,
                             AuditFindings = x.AuditFindings,
                             WorkOrderId = x.WorkOrderId,
                             IsDeleted = x.IsDeleted,
                             Status = x.Status,
                             UpdatedTime = x.UpdatedTime,
                             Updater = x.Updater,
                             Creator = x.Creator,
                             SelectAreaId = x.SelectAreaId,
                             CreatedTime = x.CreatedTime
                         };
            return query2.ToPagedList(model.PageIndex, model.PageSize);
        }
        /// <summary>
        /// 根据编号查询详情
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<DtoReserve> GetById(long id)
        {
            var query = _reserveRepository.Where(x => x.Id == id);
            var dto = query.Join(_venuedeviceRepository.Entities, x => x.VenueDeviceId, y => y.Id, (x, y) => new DtoReserve()
            {
                Id = x.Id,
                Theme = x.Theme,
                Number = x.Number,
                ReserveDate = x.ReserveDate,
                StartTime = x.StartTime,
                EndTime = x.EndTime,
                MeetingResults = x.MeetingResults,
                Remarks = x.Remarks,
                VenueDeviceId = x.VenueDeviceId,
                VenueDeviceName = y.Name,
                AuditFindings = x.AuditFindings,
                WorkOrderId = x.WorkOrderId,
                IsDeleted = x.IsDeleted,
                Status = x.Status,
                UpdatedTime = x.UpdatedTime,
                Updater = x.Updater,
                Creator = x.Creator,
                SelectAreaId = x.SelectAreaId,
                CreatedTime = x.CreatedTime,
            }).FirstOrDefault();
            //志愿者
            var volunteerList=  _reservevolunteerRepository.Where(x => x.ReserveId == id)
                .Join(_volunteerRepository.Entities, x => x.VolunteerId, y => y.Id, (x, y) =>y).ProjectToType<DtoVolunteer>().ToList();
            dto.VolunteerList = volunteerList;

            if (dto.VenueDeviceId != 0)
            {
                dto.Venuedevice = await _venuedeviceService.GetById(dto.VenueDeviceId);
            }
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

            List<Reservevolunteer> addList = new List<Reservevolunteer>();
            if (model.VolunteerList != null)
            {
                foreach (var item in model.VolunteerList)
                {
                    addList.Add(new Reservevolunteer()
                    {
                        Id = YitIdHelper.NextId(),
                        VolunteerId = item.Id,
                        ReserveId = entity.Id,
                    });
                }
            }
            _reservevolunteerRepository.Insert(addList);
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

            var volunteer= _reservevolunteerRepository.Where(x => x.ReserveId == model.Id).ToList();
            _reservevolunteerRepository.Delete(volunteer);

            List<Reservevolunteer> addList = new List<Reservevolunteer>();
            if (model.VolunteerList != null)
            {
                foreach (var item in model.VolunteerList)
                {
                    addList.Add(new Reservevolunteer()
                    {
                        Id = YitIdHelper.NextId(),
                        VolunteerId = item.Id,
                        ReserveId = entity.Id,
                    });
                }
            }
            _reservevolunteerRepository.Insert(addList);

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
