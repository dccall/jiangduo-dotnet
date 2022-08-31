using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Furion.FriendlyException;
using JiangDuo.Application.AppService.ResidentService.Dto;
using JiangDuo.Core.Models;
using JiangDuo.Core.Utils;
using Mapster;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yitter.IdGenerator;

namespace JiangDuo.Application.AppService.ResidentService.Services
{
    public class ResidentService : IResidentService, ITransient
    {
        private readonly ILogger<ResidentService> _logger;
        private readonly IRepository<Resident> _residentRepository;
        private readonly IRepository<Village> _villageRepository;
        private readonly IRepository<SelectArea> _selectAreaRepository;

        public ResidentService(ILogger<ResidentService> logger,
            IRepository<SelectArea> selectAreaRepository,
            IRepository<Resident> residentRepository, IRepository<Village> villageRepository)
        {
            _logger = logger;
            _residentRepository = residentRepository;
            _villageRepository = villageRepository;
            _selectAreaRepository = selectAreaRepository;
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="model">数据</param>
        /// <returns></returns>
        public PagedList<DtoResident> GetList(DtoResidentQuery model)
        {
            var query = _residentRepository.Where(x => !x.IsDeleted);
            query = query.Where(!string.IsNullOrEmpty(model.Name), x => x.Name.Contains(model.Name));

            //不传或者传-1查询全部
            query = query.Where(!(model.VillageId == null || model.VillageId == -1), x => x.VillageId == model.VillageId);

            var query2 = from x in query
                         join v in _villageRepository.Entities on x.VillageId equals v.Id into result1
                         from xv in result1.DefaultIfEmpty()
                         join s in _selectAreaRepository.Entities on x.SelectAreaId equals s.Id into result2
                         from xs in result2.DefaultIfEmpty()
                         select new DtoResident()
                         {
                             Id = x.Id,
                             SelectAreaId = x.SelectAreaId,
                             SelectAreaName = xs.SelectAreaName,
                             VillageId = x.VillageId,
                             VillageName = xv.Name,
                             Address = x.Address,
                             Age = x.Age,
                             Birthday = x.Birthday,
                             Idnumber = x.Idnumber,
                             Name = x.Name,
                             Nationality = x.Nationality,
                             Origin = x.Origin,
                             PhoneNumber = x.PhoneNumber,
                             OpenId = x.OpenId,
                             PoliticalOutlook = x.PoliticalOutlook,
                             Sex = x.Sex,
                             Status = x.Status,
                             IsDeleted = x.IsDeleted,
                             Creator = x.Creator,
                             CreatedTime = x.CreatedTime,
                             UpdatedTime = x.UpdatedTime,
                             Updater = x.Updater,
                         };

            //将数据映射到DtoResident中
            return query2.OrderByDescending(s => s.CreatedTime).ToPagedList(model.PageIndex, model.PageSize);
        }

        /// <summary>
        /// 根据编号查询详情
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<DtoResident> GetById(long id)
        {
            var query = _residentRepository.Where(x => x.Id == id);

            var query2 = from x in query
                         join v in _villageRepository.Entities on x.VillageId equals v.Id into result1
                         from xv in result1.DefaultIfEmpty()
                         join s in _selectAreaRepository.Entities on x.SelectAreaId equals s.Id into result2
                         from xs in result2.DefaultIfEmpty()
                         select new DtoResident()
                         {
                             Id = x.Id,
                             SelectAreaId = x.SelectAreaId,
                             SelectAreaName = xs.SelectAreaName,
                             VillageId = x.VillageId,
                             VillageName = xv.Name,
                             Address = x.Address,
                             Age = x.Age,
                             Birthday = x.Birthday,
                             Idnumber = x.Idnumber,
                             Name = x.Name,
                             Nationality = x.Nationality,
                             Origin = x.Origin,
                             PhoneNumber = x.PhoneNumber,
                             OpenId = x.OpenId,
                             PoliticalOutlook = x.PoliticalOutlook,
                             Sex = x.Sex,
                             Status = x.Status,
                             IsDeleted = x.IsDeleted,
                             Creator = x.Creator,
                             CreatedTime = x.CreatedTime,
                             UpdatedTime = x.UpdatedTime,
                             Updater = x.Updater,
                         };

            var dto = query2.FirstOrDefault();

            return dto;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Insert(DtoResidentForm model)
        {
            var entity = model.Adapt<Resident>();
            entity.Id = YitIdHelper.NextId();
            entity.CreatedTime = DateTime.Now;
            entity.Creator = JwtHelper.GetAccountId();

            if (entity.VillageId != null)
            {
                var village = _villageRepository.FindOrDefault(entity.VillageId);
                entity.SelectAreaId = village?.SelectAreaId;
            }

            _residentRepository.Insert(entity);
            return await _residentRepository.SaveNowAsync();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(DtoResidentForm model)
        {
            //先根据id查询实体
            var entity = _residentRepository.FindOrDefault(model.Id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            //将模型数据映射给实体属性
            entity = model.Adapt(entity);
            entity.UpdatedTime = DateTime.Now;
            entity.Updater = JwtHelper.GetAccountId();

            if (entity.VillageId != null)
            {
                var village = _villageRepository.FindOrDefault(entity.VillageId);
                entity.SelectAreaId = village?.SelectAreaId;
            }
            _residentRepository.Update(entity);
            return await _residentRepository.SaveNowAsync();
        }

        /// <summary>
        /// 假删除
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<int> FakeDelete(long id)
        {
            var entity = _residentRepository.FindOrDefault(id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            entity.IsDeleted = true;
            return await _residentRepository.SaveNowAsync();
        }

        /// <summary>
        /// 批量假删除
        /// </summary>
        /// <param name="idList">编号</param>
        /// <returns></returns>
        public async Task<int> FakeDelete(List<long> idList)
        {
            var result = await _residentRepository.Context.BatchUpdate<Resident>()
                .Where(x => idList.Contains(x.Id))
                .Set(x => x.IsDeleted, x => true)
                .ExecuteAsync();
            return result;
        }
    }
}