using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Furion.FriendlyException;
using JiangDuo.Application.AppService.VenuedeviceService.Dto;
using JiangDuo.Core.Models;
using JiangDuo.Core.Utils;
using Mapster;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yitter.IdGenerator;

namespace JiangDuo.Application.AppService.VenuedeviceService.Services
{
    public class VenuedeviceService : IVenuedeviceService, ITransient
    {
        private readonly ILogger<VenuedeviceService> _logger;
        private readonly IRepository<Venuedevice> _venuedeviceRepository;
        private readonly IRepository<Regulation> _regulationRepository;
        private readonly IRepository<SysUploadFile> _uploadRepository;
        private readonly IRepository<Building> _buiildingRepository;
        private readonly IRepository<SelectArea> _selectAreaRepository;

        public VenuedeviceService(ILogger<VenuedeviceService> logger,
            IRepository<Regulation> regulationRepository,
            IRepository<SysUploadFile> uploadRepository,
            IRepository<Venuedevice> venuedeviceRepository,
            IRepository<SelectArea> selectAreaRepository,
            IRepository<Building> buiildingRepository)
        {
            _logger = logger;
            _venuedeviceRepository = venuedeviceRepository;
            _buiildingRepository = buiildingRepository;
            _regulationRepository = regulationRepository;
            _uploadRepository = uploadRepository;
            _selectAreaRepository = selectAreaRepository;
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="model">数据</param>
        /// <returns></returns>
        public PagedList<DtoVenuedevice> GetList(DtoVenuedeviceQuery model)
        {
            var query = _venuedeviceRepository.Where(x => !x.IsDeleted);
            query = query.Where(!string.IsNullOrEmpty(model.VenuedeviceName), x => x.Name.Contains(model.VenuedeviceName));
            query = query.Where(model.SelectAreaId != null, x => x.SelectAreaId == model.SelectAreaId);

            var query2 = from v in query
                         join b in _buiildingRepository.Entities on v.BuildingId equals b.Id into result1
                         from vb in result1.DefaultIfEmpty()
                         join s in _selectAreaRepository.Entities on v.SelectAreaId equals s.Id into result2
                         from vs in result2.DefaultIfEmpty()
                         join r in _regulationRepository.Entities on v.RegulationId equals r.Id into result3
                         from vr in result3.DefaultIfEmpty()

                         select new DtoVenuedevice
                         {
                             Id = v.Id,
                             Name = v.Name,
                             SelectAreaId = v.SelectAreaId,
                             SelectAreaName = vs.SelectAreaName,
                             BuildingId = v.BuildingId,
                             Images = v.Images,
                             RegulationId = v.RegulationId,
                             Remarks = v.Remarks,
                             Type = v.Type,
                             BuildingName = vb.BuildingName,
                             Regulation = vr,
                             CreatedTime = v.CreatedTime,
                             Creator = v.Creator,
                             UpdatedTime = v.UpdatedTime,
                             Updater = v.Updater,
                             IsDeleted = v.IsDeleted,
                         };
            var pageList = query2.OrderByDescending(s => s.CreatedTime).ToPagedList(model.PageIndex, model.PageSize);
            return pageList;
        }

        /// <summary>
        /// 根据编号查询详情
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<DtoVenuedevice> GetById(long id)
        {
            var query = _venuedeviceRepository.Where(x => !x.IsDeleted && x.Id == id);
            var query2 = from v in query
                         join b in _buiildingRepository.Entities on v.BuildingId equals b.Id into result1
                         from vb in result1.DefaultIfEmpty()
                         join s in _selectAreaRepository.Entities on v.SelectAreaId equals s.Id into result2
                         from vs in result2.DefaultIfEmpty()
                         join r in _regulationRepository.Entities on v.RegulationId equals r.Id into result3
                         from vr in result3.DefaultIfEmpty()

                         select new DtoVenuedevice
                         {
                             Id = v.Id,
                             Name = v.Name,
                             SelectAreaId = v.SelectAreaId,
                             SelectAreaName = vs.SelectAreaName,
                             BuildingId = v.BuildingId,
                             Images = v.Images,
                             RegulationId = v.RegulationId,
                             Remarks = v.Remarks,
                             Type = v.Type,
                             BuildingName = vb.BuildingName,
                             Regulation = vr,
                             CreatedTime = v.CreatedTime,
                             Creator = v.Creator,
                             UpdatedTime = v.UpdatedTime,
                             Updater = v.Updater,
                             IsDeleted = v.IsDeleted,
                         };

            var dto = query2.FirstOrDefault();
            if (dto != null)
            {
                if (!string.IsNullOrEmpty(dto.Images))
                {
                    var idList = dto.Images.Split(',').ToList();
                    dto.ImageList = _uploadRepository.Where(x => idList.Contains(x.FileId.ToString())).ToList();
                }
            }
            return dto;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Insert(DtoVenuedeviceForm model)
        {
            var entity = model.Adapt<Venuedevice>();
            entity.Id = YitIdHelper.NextId();
            entity.CreatedTime = DateTime.Now;
            entity.Creator = JwtHelper.GetAccountId();

            if (model.ImageList != null && model.ImageList.Any())
            {
                entity.Images = String.Join(",", model.ImageList.Select(x => x.FileId));
            }
            if (model.BuildingId != null)
            {
                var buiilding = _buiildingRepository.FindOrDefault(model.BuildingId);
                entity.SelectAreaId = buiilding?.SelectAreaId;
            }

            _venuedeviceRepository.Insert(entity);
            return await _venuedeviceRepository.SaveNowAsync();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(DtoVenuedeviceForm model)
        {
            //先根据id查询实体
            var entity = _venuedeviceRepository.FindOrDefault(model.Id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            //将模型数据映射给实体属性
            entity = model.Adapt(entity);
            entity.UpdatedTime = DateTime.Now;
            entity.Updater = JwtHelper.GetAccountId();
            if (model.ImageList != null && model.ImageList.Any())
            {
                entity.Images = String.Join(",", model.ImageList.Select(x => x.FileId));
            }
            if (model.BuildingId != null)
            {
                var buiilding = _buiildingRepository.FindOrDefault(model.BuildingId);
                entity.SelectAreaId = buiilding?.SelectAreaId;
            }
            _venuedeviceRepository.Update(entity);
            return await _venuedeviceRepository.SaveNowAsync();
        }

        /// <summary>
        /// 假删除
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<int> FakeDelete(long id)
        {
            var entity = _venuedeviceRepository.FindOrDefault(id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            entity.IsDeleted = true;
            return await _venuedeviceRepository.SaveNowAsync();
        }

        /// <summary>
        /// 批量假删除
        /// </summary>
        /// <param name="idList">编号</param>
        /// <returns></returns>
        public async Task<int> FakeDelete(List<long> idList)
        {
            var result = await _venuedeviceRepository.Context.BatchUpdate<Venuedevice>()
                .Where(x => idList.Contains(x.Id))
                .Set(x => x.IsDeleted, x => true)
                .ExecuteAsync();
            return result;
        }
    }
}