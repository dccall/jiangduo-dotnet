using Furion;
using Furion.DatabaseAccessor;
using Furion.DatabaseAccessor.Extensions;
using Furion.DependencyInjection;
using JiangDuo.Application.AppService.BusinessService.Services;
using JiangDuo.Application.AppService.QueryStatistics.Dtos;
using JiangDuo.Core.Enums;
using JiangDuo.Core.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.QueryStatistics.Services
{
    public class QueryStatisticsService : IQueryStatisticsService, ITransient
    {
        private readonly ILogger<QueryStatisticsService> _logger;
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
        public QueryStatisticsService(ILogger<QueryStatisticsService> logger,
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
            var list = _selectAreaRepository.Where(x => !x.IsDeleted).Select(x => new DtoSelectAreaInfo { 
                 SelectArea=x,
                 OfficialCount=_officialRepository.Entities.Where(s=>!s.IsDeleted&&s.SelectAreaId==x.Id).Count(),
                 ResidentCount=_residentRepository.Entities.Where(s=>!s.IsDeleted&&s.SelectAreaId==x.Id).Count(),
                 VillageCount=_villageRepository.Entities.Where(s=>!s.IsDeleted&&s.SelectAreaId==x.Id).Count(),
            }).ToList();
            return list;
        }

        /// <summary>
        /// 获取基本数据总数统计
        /// </summary>
        /// <returns></returns>
        public DtoTotal GetBaseTotalCount()
        {
            DtoTotal dto=new DtoTotal();
            dto.TotalCount = _selectAreaRepository.Where(x => !x.IsDeleted).Count();
            dto.AreaTotalCount = _selectAreaRepository.Where(x => !x.IsDeleted&&x.SelectAreaType== SelectAreaTypeEnum.SelectArea).Count();
            dto.TownTotalCount = _selectAreaRepository.Where(x => !x.IsDeleted && x.SelectAreaType == SelectAreaTypeEnum.Town).Count();
            dto.VillageTotalCount = _villageRepository.Where(x => !x.IsDeleted).Count();
            dto.ResidentTotalCount = _residentRepository.Where(x => !x.IsDeleted).Count();
            dto.OfficialTotalCount = _officialRepository.Where(x => !x.IsDeleted).Count();
            dto.AreaOfficialCount = _officialRepository.Where(x => !x.IsDeleted&& x.CategoryId==1).Count();
            dto.TownOfficialCount = _officialRepository.Where(x => !x.IsDeleted && x.CategoryId == 2).Count();

            return dto;
        }
        /// <summary>
        /// 获取用户需求业务分类统计
        /// </summary>
        /// <returns></returns>
        public List<DtoBusinessStatistics> GetBusinessStatistics()
        {
            var totalCount= _publicSentimentRepository.Where(x => !x.IsDeleted).Count();
            var list = _businessRepository.Where(x => !x.IsDeleted).Select(x => new DtoBusinessStatistics()
            {
                 BusinessName=x.Name,
                 Count= _publicSentimentRepository.Entities.Where(s=>!s.IsDeleted&&s.BusinessId==x.Id).Count(),
                 Percentage =_publicSentimentRepository.Entities.Where(s=>!s.IsDeleted&&s.BusinessId==x.Id).Count()/ totalCount*100,
            }).ToList();
            return list;
        }
        /// <summary>
        /// 获取用户需求数量统计
        /// </summary>
        /// <returns></returns>
        public DtoPublicSentimentStatistics GetPublicSentimentStatistics()
        {
            DtoPublicSentimentStatistics dto=new DtoPublicSentimentStatistics();
            dto.TotalCount= _publicSentimentRepository.Where(x => !x.IsDeleted).Count();
            dto.NotProcessedCount= _publicSentimentRepository.Where(x => !x.IsDeleted&& x.Status== PublicSentimentStatus.NotProcessed).Count();
            dto.InProgressCount= _publicSentimentRepository.Where(x => !x.IsDeleted&& x.Status== PublicSentimentStatus.InProgress).Count();
            dto.FeedbackCount= _publicSentimentRepository.Where(x => !x.IsDeleted&& x.Status== PublicSentimentStatus.Feedback).Count();
            return dto;
        }
        /// <summary>
        /// 获取用户需求数量统计(日)
        /// </summary>
        /// <returns></returns>
        public List<DtoYearMonthDayStatistics> GetPublicSentimentDayStatistics(DtoYearMonthDayStatisticsQuery model)
        {

            var query= _publicSentimentRepository.Where(x => !x.IsDeleted);
            query=query.Where(model.StartTime != null, x => model.StartTime >= x.CreatedTime);
            query = query.Where(model.EndTime != null, x => x.CreatedTime<= model.EndTime);

            //            var sql = @"
            //select 
            //count(0) as count,
            //DATE_FORMAT(CreatedTime,'%Y-%m-%d') as date
            //from publicsentiment
            //group by DATE_FORMAT(CreatedTime,'%Y-%m-%d')
            //";
            var list = query.OrderBy(x=>x.CreatedTime).GroupBy(x =>new {year= x.CreatedTime.Year,month=x.CreatedTime.Month,day=x.CreatedTime.Day }).Select(x => new DtoYearMonthDayStatistics
            {
                Date = x.Key.year+"-"+ x.Key.month + "-" + x.Key.day,
                Count = x.Count()
            }).ToList();
            return list;
        }

        /// <summary>
        /// 获取用户需求数量统计(月)
        /// </summary>
        /// <returns></returns>
        public List<DtoYearMonthDayStatistics> GetPublicSentimentMonthStatistics(DtoYearMonthDayStatisticsQuery model)
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
        public List<DtoYearMonthDayStatistics> GetPublicSentimentYearStatistics(DtoYearMonthDayStatisticsQuery model)
        {

            var query = _publicSentimentRepository.Where(x => !x.IsDeleted);
            query = query.Where(model.StartTime != null, x => model.StartTime >= x.CreatedTime);
            query = query.Where(model.EndTime != null, x => x.CreatedTime <= model.EndTime);
            var list = query.OrderBy(x => x.CreatedTime).GroupBy(x => new { year = x.CreatedTime.Year }).Select(x => new DtoYearMonthDayStatistics
            {
                Date = x.Key.year+"",
                Count = x.Count()
            }).ToList();
            return list;
        }

        /// <summary>
        /// 获取服务数量统计
        /// </summary>
        /// <returns></returns>
        public DtoServiceTotal GetServiceTotalStatistics()
        {
            //var AllList = new List<ServiceStatusEnum>() {
            //     ServiceStatusEnum.Audit, //待审核
            //     ServiceStatusEnum.Audited,//审核通过
            //     ServiceStatusEnum.AuditFailed,//审核未通过
            //     ServiceStatusEnum.Published,//已发布
            //     ServiceStatusEnum.Ended,//已结束
            //};
            DtoServiceTotal dto=new DtoServiceTotal();
            dto.TotalCount= _serviceRepository.Where(x => !x.IsDeleted && x.Status != ServiceStatusEnum.Normal).Count();
            dto.AuditCount= _serviceRepository.Where(x => !x.IsDeleted && x.Status == ServiceStatusEnum.Audit).Count();
            dto.AuditedCount= _serviceRepository.Where(x => !x.IsDeleted && x.Status == ServiceStatusEnum.Audited).Count();
            dto.AuditFailedCount= _serviceRepository.Where(x => !x.IsDeleted && x.Status == ServiceStatusEnum.AuditFailed).Count();
            dto.PublishedCount= _serviceRepository.Where(x => !x.IsDeleted && x.Status == ServiceStatusEnum.Published).Count();
            dto.EndCount= _serviceRepository.Where(x => !x.IsDeleted && x.Status == ServiceStatusEnum.Ended).Count();

            return dto;
        }
        /// <summary>
        /// 获取预约服务数量统计
        /// </summary>
        /// <returns></returns>
        public DtoReserveTotal GetReserveTotalStatistics()
        {
            DtoReserveTotal dto = new DtoReserveTotal();
            dto.TotalCount = _reserveRepository.Where(x => !x.IsDeleted && x.Status !=  ReserveStatus.Normal).Count();
            dto.AuditCount = _reserveRepository.Where(x => !x.IsDeleted && x.Status == ReserveStatus.Audit).Count();
            dto.AuditedCount = _reserveRepository.Where(x => !x.IsDeleted && x.Status == ReserveStatus.Audited).Count();
            dto.AuditFailedCount = _reserveRepository.Where(x => !x.IsDeleted && x.Status == ReserveStatus.AuditFailed).Count();
            dto.CompletedCount = _reserveRepository.Where(x => !x.IsDeleted && x.Status == ReserveStatus.Completed).Count();
            return dto;
        }
    }
}
