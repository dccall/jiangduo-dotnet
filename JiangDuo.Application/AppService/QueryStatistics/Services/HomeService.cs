using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using JiangDuo.Application.AppService.OfficialService.Dto;
using JiangDuo.Application.AppService.QueryStatistics.Dtos;
using JiangDuo.Application.AppService.ReserveService.Dto;
using JiangDuo.Application.AppService.ServiceService.Dto;
using JiangDuo.Application.Tools;
using JiangDuo.Core.Base;
using JiangDuo.Core.Enums;
using JiangDuo.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.QueryStatistics.Services
{
    public class HomeService : IHomeService, ITransient
    {
        private readonly ILogger<HomeService> _logger;
        private readonly IRepository<Core.Models.Service> _serviceRepository;
        private readonly IRepository<Workorder> _workOrderRepository;
        private readonly IRepository<Participant> _participantRepository;
        private readonly IRepository<Resident> _residentRepository;
        private readonly IRepository<Official> _officialRepository;
        private readonly IRepository<Reserve> _reserveRepository;
        private readonly IRepository<Venuedevice> _venuedeviceRepository;
        private readonly IRepository<SelectArea> _selectAreaRepository;
        private readonly IRepository<Village> _villageRepository;
        private readonly IRepository<Business> _businessRepository;
        private readonly IRepository<PublicSentiment> _publicSentimentRepository;

        public HomeService(ILogger<HomeService> logger,
            IRepository<Resident> residentRepository,
            IRepository<Core.Models.Service> serviceRepository,
            IRepository<Workorder> workOrderRepository,
            IRepository<Official> officialRepository,
            IRepository<Reserve> reserveRepository,
            IRepository<SelectArea> selectAreaRepository,
            IRepository<Venuedevice> venuedeviceRepositor,
            IRepository<Village> villageRepository,
            IRepository<Business> businessRepository,
            IRepository<PublicSentiment> publicSentimentRepository,
            IRepository<Participant> participantRepository)
        {
            _logger = logger;
            _serviceRepository = serviceRepository;
            _workOrderRepository = workOrderRepository;
            _participantRepository = participantRepository;
            _residentRepository = residentRepository;
            _officialRepository = officialRepository;
            _reserveRepository = reserveRepository;
            _venuedeviceRepository = venuedeviceRepositor;
            _selectAreaRepository = selectAreaRepository;
            _villageRepository = villageRepository;
            _businessRepository = businessRepository;
            _publicSentimentRepository = publicSentimentRepository;
        }

        /// <summary>
        /// 获取选区信息
        /// </summary>
        /// <returns></returns>
        public List<DtoSelectAreaInfo> GetSelectAreaInfoList()
        {
            var list = _selectAreaRepository.Where(x => !x.IsDeleted).Select(x => new DtoSelectAreaInfo
            {
                SelectArea = x,
                OfficialCount = _officialRepository.Entities.Where(s => !s.IsDeleted && s.SelectAreaId== x.Id).Count(),
                ResidentCount = _residentRepository.Entities.Where(s => !s.IsDeleted && s.SelectAreaId == x.Id).Count(),
                VillageCount = _villageRepository.Entities.Where(s => !s.IsDeleted && s.SelectAreaId == x.Id).Count(),
            }).ToList();
            return list;
        }

        /// <summary>
        /// 获取基本数据总数统计
        /// </summary>
        /// <returns></returns>
        public DtoTotal GetBaseTotalCount()
        {
            DtoTotal dto = new DtoTotal();


            //查询所有村的人数总和
            dto.TotalPopulation = (from x in _selectAreaRepository.Entities
                                   join y in _villageRepository.Entities on x.Id equals y.SelectAreaId
                                   where !x.IsDeleted && !y.IsDeleted
                                   select y.Population).Sum();

            //查询所有村的面积总和
            dto.TotalGrossArea = (from x in _selectAreaRepository.Entities
                                  join y in _villageRepository.Entities on x.Id equals y.SelectAreaId
                                  where !x.IsDeleted && !y.IsDeleted
                                  select y.GrossArea).Sum();


            dto.TotalCount = _selectAreaRepository.Where(x => !x.IsDeleted).Count();
            dto.AreaTotalCount = _selectAreaRepository.Where(x => !x.IsDeleted && x.SelectAreaType == SelectAreaTypeEnum.SelectArea).Count();
            dto.TownTotalCount = _selectAreaRepository.Where(x => !x.IsDeleted && x.SelectAreaType == SelectAreaTypeEnum.Town).Count();
            dto.VillageTotalCount = _villageRepository.Where(x => !x.IsDeleted).Count();
            dto.ResidentTotalCount = _residentRepository.Where(x => !x.IsDeleted).Count();
            dto.OfficialTotalCount = _officialRepository.Where(x => !x.IsDeleted).Count();
            //dto.AreaOfficialCount = _officialRepository.Where(x => !x.IsDeleted && x.Type== OfficialType.Area).Count();
            //dto.TownOfficialCount = _officialRepository.Where(x => !x.IsDeleted && x.Type== OfficialType.Town).Count();
            dto.AreaOfficialCount = _officialRepository.Where(x => !x.IsDeleted && x.Type.Contains("区")).Count();
            dto.TownOfficialCount = _officialRepository.Where(x => !x.IsDeleted && x.Type.Contains("镇")).Count();
            return dto;
        }

        /// <summary>
        /// 获取用户需求业务分类统计
        /// </summary>
        /// <returns></returns>
        public List<DtoBusinessStatistics> GetBusiness()
        {
            var totalCount = _publicSentimentRepository.Where(x => !x.IsDeleted).Count();
            var list = _businessRepository.Where(x => !x.IsDeleted).Select(x => new DtoBusinessStatistics()
            {
                BusinessName = x.Name,
                Count = _publicSentimentRepository.Entities.Where(s => !s.IsDeleted && s.BusinessId == x.Id).Count(),
                Percentage = _publicSentimentRepository.Entities.Where(s => !s.IsDeleted && s.BusinessId == x.Id).Count() / totalCount * 100,
            }).ToList();
            return list;
        }

        /// <summary>
        /// 获取用户需求数量统计
        /// </summary>
        /// <returns></returns>
        public DtoPublicSentimentStatisticsQuery GetPublicSentiment()
        {
            DtoPublicSentimentStatisticsQuery dto = new DtoPublicSentimentStatisticsQuery();
            dto.TotalCount = _publicSentimentRepository.Where(x => !x.IsDeleted).Count();
            dto.NotProcessedCount = _publicSentimentRepository.Where(x => !x.IsDeleted && x.Status == PublicSentimentStatus.NotProcessed).Count();
            dto.InProgressCount = _publicSentimentRepository.Where(x => !x.IsDeleted && x.Status == PublicSentimentStatus.InProgress).Count();
            dto.FeedbackCount = _publicSentimentRepository.Where(x => !x.IsDeleted && x.Status == PublicSentimentStatus.Feedback).Count();
            return dto;
        }

        /// <summary>
        /// 获取用户需求数量统计(日)
        /// </summary>
        /// <returns></returns>
        public List<DtoYearMonthDayStatistics> GetPublicSentimentDay(DtoYearMonthDayStatisticsQuery model)
        {
            var query = _publicSentimentRepository.Where(x => !x.IsDeleted);
            query = query.Where(model.StartTime != null, x => model.StartTime >= x.CreatedTime);
            query = query.Where(model.EndTime != null, x => x.CreatedTime <= model.EndTime);

            //            var sql = @"
            //select
            //count(0) as count,
            //DATE_FORMAT(CreatedTime,'%Y-%m-%d') as date
            //from publicsentiment
            //group by DATE_FORMAT(CreatedTime,'%Y-%m-%d')
            //";
            var list = query.OrderBy(x => x.CreatedTime).GroupBy(x => new { year = x.CreatedTime.Year, month = x.CreatedTime.Month, day = x.CreatedTime.Day }).Select(x => new DtoYearMonthDayStatistics
            {
                Date = x.Key.year + "-" + x.Key.month + "-" + x.Key.day,
                Count = x.Count()
            }).ToList();
            return list;
        }

        /// <summary>
        /// 获取用户需求数量统计(月)
        /// </summary>
        /// <returns></returns>
        public List<DtoYearMonthDayStatistics> GetPublicSentimentMonth(DtoYearMonthDayStatisticsQuery model)
        {
            var query = _publicSentimentRepository.Where(x => !x.IsDeleted);
            query = query.Where(model.StartTime != null, x => model.StartTime >= x.CreatedTime);
            query = query.Where(model.EndTime != null, x => x.CreatedTime <= model.EndTime);
            var list = query.OrderBy(x => x.CreatedTime).GroupBy(x => new { year = x.CreatedTime.Year, month = x.CreatedTime.Month }).Select(x => new DtoYearMonthDayStatistics
            {
                Date = x.Key.year + "-" + x.Key.month,
                Count = x.Count()
            }).ToList();
            return list;
        }

        /// <summary>
        /// 获取用户需求数量统计(年)
        /// </summary>
        /// <returns></returns>
        public List<DtoYearMonthDayStatistics> GetPublicSentimentYear(DtoYearMonthDayStatisticsQuery model)
        {
            var query = _publicSentimentRepository.Where(x => !x.IsDeleted);
            query = query.Where(model.StartTime != null, x => model.StartTime >= x.CreatedTime);
            query = query.Where(model.EndTime != null, x => x.CreatedTime <= model.EndTime);
            var list = query.OrderBy(x => x.CreatedTime).GroupBy(x => new { year = x.CreatedTime.Year }).Select(x => new DtoYearMonthDayStatistics
            {
                Date = x.Key.year + "",
                Count = x.Count()
            }).ToList();
            return list;
        }

        /// <summary>
        /// 获取服务数量统计
        /// </summary>
        /// <returns></returns>
        public DtoServiceTotal GetServiceTotal()
        {
            //var AllList = new List<ServiceStatusEnum>() {
            //     ServiceStatusEnum.Audit, //待审核
            //     ServiceStatusEnum.Audited,//审核通过
            //     ServiceStatusEnum.AuditFailed,//审核未通过
            //     ServiceStatusEnum.Published,//已发布
            //     ServiceStatusEnum.Ended,//已结束
            //};
            DtoServiceTotal dto = new DtoServiceTotal();
            dto.TotalCount = _serviceRepository.Where(x => !x.IsDeleted && x.Status != ServiceStatusEnum.Normal).Count();
            dto.AuditCount = _serviceRepository.Where(x => !x.IsDeleted && x.Status == ServiceStatusEnum.Audit).Count();
            dto.AuditedCount = _serviceRepository.Where(x => !x.IsDeleted && x.Status == ServiceStatusEnum.Audited).Count();
            dto.AuditFailedCount = _serviceRepository.Where(x => !x.IsDeleted && x.Status == ServiceStatusEnum.AuditFailed).Count();
            dto.PublishedCount = _serviceRepository.Where(x => !x.IsDeleted && x.Status == ServiceStatusEnum.Published).Count();
            dto.EndCount = _serviceRepository.Where(x => !x.IsDeleted && x.Status == ServiceStatusEnum.Ended).Count();

            return dto;
        }

        /// <summary>
        /// 获取预约服务数量统计
        /// </summary>
        /// <returns></returns>
        public DtoReserveTotal GetReserveTotal()
        {
            DtoReserveTotal dto = new DtoReserveTotal();
            dto.TotalCount = _reserveRepository.Where(x => !x.IsDeleted && x.Status != ReserveStatus.Normal).Count();
            dto.AuditCount = _reserveRepository.Where(x => !x.IsDeleted && x.Status == ReserveStatus.Audit).Count();
            dto.AuditedCount = _reserveRepository.Where(x => !x.IsDeleted && x.Status == ReserveStatus.Audited).Count();
            dto.AuditFailedCount = _reserveRepository.Where(x => !x.IsDeleted && x.Status == ReserveStatus.AuditFailed).Count();
            dto.CompletedCount = _reserveRepository.Where(x => !x.IsDeleted && x.Status == ReserveStatus.Completed).Count();
            dto.OverCount = _reserveRepository.Where(x => !x.IsDeleted && x.Status == ReserveStatus.Over).Count();
            
            return dto;
        }

        /// <summary>
        /// 获取有事好商量预约数量（30天）
        /// </summary>
        /// <returns></returns>
        public List<DtoReserveCount> GetReserveCount()
        {
            var startDate = DateTime.Now;
            var endDate = startDate.AddDays(30);
            var query = _reserveRepository.AsQueryable().Where(x => !x.IsDeleted && x.Status == ReserveStatus.Audited);
            //两个时间比较，取反。包含时间范围
            query = query.Where(x => !(x.StartTime.Date >= endDate.Date && x.EndTime.Date <= startDate.Date));
            var list = query.ToList();
            List<DtoReserveCount> resultList = new List<DtoReserveCount>();
            while (startDate <= endDate)
            {
                resultList.Add(new DtoReserveCount()
                {
                    Date = startDate.Date.ToString("yyyy-MM-dd"),
                    Count = list.Where(x => x.StartTime.Date <= startDate.Date && startDate.Date <= x.EndTime.Date).Count()
                });
                startDate = startDate.AddDays(1);
            }
            return resultList;
        }

        /// <summary>
        /// 获取活动预约数量（30天）
        /// </summary>
        /// <returns></returns>
        public List<DtoServiceCount> GetServiceCount()
        {
            var startDate = DateTime.Now;
            var endDate = startDate.AddDays(30);
            var query = _serviceRepository.AsQueryable().Where(x => !x.IsDeleted && x.Status == ServiceStatusEnum.Published);
            //两个时间比较，取反。包含时间范围
            query = query.Where(x => !(x.PlanStartTime.Value.Date >= endDate.Date && x.PlanEndTime.Value.Date <= startDate.Date));
            var list = query.ToList();
            List<DtoServiceCount> resultList = new List<DtoServiceCount>();
            while (startDate <= endDate)
            {
                resultList.Add(new DtoServiceCount()
                {
                    Date = startDate.Date.ToString("yyyy-MM-dd"),
                    Count = list.Where(x => x.PlanStartTime.Value.Date <= startDate.Date && startDate.Date <= x.PlanEndTime.Value.Date).Count()
                });
                startDate = startDate.AddDays(1);
            }
            return resultList;
        }

        /// <summary>
        /// 获取有事好商量预约场地列表
        /// </summary>
        /// <returns></returns>
        public PagedList<DtoReserve> GetReserveVenuedevice(DtoReserveVenuedeviceQuery model)
        {
            var query = _reserveRepository.AsQueryable().Where(x => !x.IsDeleted && x.Status == ReserveStatus.Audited);
            query = query.Where(model.ReserveDate != null, x => x.StartTime.Date <= model.ReserveDate.Value.Date && model.ReserveDate.Value.Date <= x.EndTime.Date);

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
                             CreatedTime = x.CreatedTime,
                         };
            return query2.ToPagedList(model.PageIndex, model.PageSize);
        }

        /// <summary>
        /// 获取活动预约场地列表
        /// </summary>
        /// <returns></returns>
        public PagedList<DtoService> GetServiceVenuedevice(DtoReserveVenuedeviceQuery model)
        {
            var query = _serviceRepository.AsQueryable().Where(x => !x.IsDeleted && x.Status == ServiceStatusEnum.Published);
            query = query.Where(model.ReserveDate != null, x => x.PlanStartTime.Value.Date <= model.ReserveDate.Value.Date && model.ReserveDate.Value.Date <= x.PlanEndTime.Value.Date);

            var query2 = from service in query
                         join official in _officialRepository.Entities on service.OfficialsId equals official.Id into result1
                         from so in result1.DefaultIfEmpty()
                         join venuedevice in _venuedeviceRepository.Entities on service.VenueDeviceId equals venuedevice.Id into result2
                         from sv in result2.DefaultIfEmpty()
                         orderby service.CreatedTime descending
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
        /// 人大完结工单排名
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<PagedList<DtoOfficial>> OfficialRanking(BaseRequest model)
        {
            var query = _officialRepository.Where(x => !x.IsDeleted);

            var query2 = query.Select(x => new DtoOfficial()
            {
                Id = x.Id,
                Address = x.Address,
                OfficialRole = x.OfficialRole,
                //OfficialsstructId = x.OfficialsstructId,
                Nationality = x.Nationality,
                Name = x.Name,
                Party = x.Party,
                Idnumber = x.Idnumber,
                OpenId = x.OpenId,
                Avatar = x.Avatar,
                Birthday = x.Birthday,
                CategoryId = x.CategoryId,
                CulturalLevel = x.CategoryId,
                PersonalResume = x.PersonalResume,
                Post = x.Post,
                Score = x.Score,
                Sex = x.Sex,
                //VillageId = x.VillageId,
                PhoneNumber = x.PhoneNumber,
                PoliticalOutlook = x.PoliticalOutlook,

                OverOrderCount = _workOrderRepository.AsQueryable(false).Where(w => w.RecipientId == x.Id && w.Status == WorkorderStatusEnum.End).Count(),

                IsDeleted = x.IsDeleted,
                UpdatedTime = x.UpdatedTime,
                Updater = x.Updater,
                Creator = x.Creator,
                SelectAreaId = x.SelectAreaId,
                CreatedTime = x.CreatedTime
            }).OrderByDescending(x => x.OverOrderCount);

            //将数据映射到DtoOfficial中
            return await query2.ToPagedListAsync(model.PageIndex, model.PageSize);
        }

        /// <summary>
        /// 人大每月工单数量
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<PagedList<DtoOfficialOrderCount>> OfficialOrderCount(DtoOfficialOrderCountQuery model)
        {
            var month = model.Month == null ? DateTime.Now : model.Month.Value;
            var lastMonth = month.AddMonths(-1);
            var list = await _officialRepository.AsQueryable(false).Where(x => !x.IsDeleted).Select(x => new DtoOfficialOrderCount()
            {
                Date = month.ToString("yyyy-MM"),
                Name = x.Name,
                OverOrderCount = _workOrderRepository.AsQueryable(false)
                 .Where(w => w.RecipientId == x.Id
                 && w.Status == WorkorderStatusEnum.End
                 && w.OverTime.Value.Year == month.Year
                 && w.OverTime.Value.Month == month.Month)
                 .Count(),
                LastMonthOrderCount = _workOrderRepository.AsQueryable(false)
                 .Where(w => w.RecipientId == x.Id
                 && w.Status == WorkorderStatusEnum.End
                 && w.OverTime.Value.Year == lastMonth.Year
                 && w.OverTime.Value.Month == lastMonth.Month)
                 .Count()
            }).OrderByDescending(x => x.OverOrderCount).ToPagedListAsync(model.PageIndex, model.PageSize);

            return list;
        }


        /// <summary>
        /// 人大每月工单数量
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IActionResult> ExportOfficialOrderCount(DtoOfficialOrderCountQuery model)
        {
            var pageList = await OfficialOrderCount(model);
            var list = pageList.Items.ToList();
            return ExcelHelp.ExportExcel("人大每月工单数量.xlsx", list);
        }
        /// <summary>
        /// 工单类型 总数量
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public async Task<List<DtoOrderTypeCount>> OrderTypeCount(DateTime? month)
        {
            month= month == null ? DateTime.Now : month;

            var reserveCount = _workOrderRepository.AsQueryable(false)
                .Where(x => !x.IsDeleted
                && x.WorkorderType == WorkorderTypeEnum.Reserve
                && x.Status == WorkorderStatusEnum.End
                && x.OverTime.Value.Year == month.Value.Year
                && x.OverTime.Value.Month == month.Value.Month
                )
                .Count();
            var serviceCount = _workOrderRepository.AsQueryable(false)
                    .Where(x => !x.IsDeleted
                    && x.WorkorderType == WorkorderTypeEnum.Service
                    && x.Status == WorkorderStatusEnum.End
                     && x.OverTime.Value.Year == month.Value.Year
                    && x.OverTime.Value.Month == month.Value.Month
                    )
                    .Count();
            var onlineCount = _workOrderRepository.AsQueryable(false)
                   .Where(x => !x.IsDeleted
                   && x.WorkorderType == WorkorderTypeEnum.OnlineLetters
                    && x.OverTime.Value.Year == month.Value.Year
                    && x.OverTime.Value.Month == month.Value.Month
                   && x.Status == WorkorderStatusEnum.End)
                   .Count();


            List<DtoOrderTypeCount> list = new List<DtoOrderTypeCount>();
            list.Add(new DtoOrderTypeCount() {Date= month.Value.ToString("yyyy-MM"), Type = WorkorderTypeEnum.Reserve, OverOrderCount = reserveCount });
            list.Add(new DtoOrderTypeCount() { Date = month.Value.ToString("yyyy-MM"), Type = WorkorderTypeEnum.Service, OverOrderCount = serviceCount });
            list.Add(new DtoOrderTypeCount() { Date = month.Value.ToString("yyyy-MM"), Type = WorkorderTypeEnum.OnlineLetters, OverOrderCount = onlineCount });

            return list;
        }

        /// <summary>
        /// 导出 工单类型 总数量
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public async Task<IActionResult> ExportOrderTypeCount(DateTime? month)
        {
            var list = await OrderTypeCount(month);
            return ExcelHelp.ExportExcel("工单类型总数量.xlsx", list);
        }

    }
}