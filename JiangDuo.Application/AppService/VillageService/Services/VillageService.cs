using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Furion.FriendlyException;
using JiangDuo.Application.AppService.VillageService.Dto;
using JiangDuo.Core.Enums;
using JiangDuo.Core.Models;
using JiangDuo.Core.Utils;
using Mapster;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yitter.IdGenerator;

namespace JiangDuo.Application.AppService.VillageService.Services
{
    public class VillageService : IVillageService, ITransient
    {
        private readonly ILogger<VillageService> _logger;
        private readonly IRepository<Village> _villageRepository;
        private readonly IRepository<Official> _officialRepository;
        private readonly IRepository<SelectArea> _selectAreaRepository;
        public VillageService(ILogger<VillageService> logger,
            IRepository<Official> officialRepository,
            IRepository<SelectArea> selectAreaRepository,
            IRepository<Village> villageRepository)
        {
            _logger = logger;
            _villageRepository = villageRepository;

            _officialRepository = officialRepository;
            _selectAreaRepository = selectAreaRepository;
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="model">数据</param>
        /// <returns></returns>
        public PagedList<DtoVillage> GetList(DtoVillageQuery model)
        {
            var query = _villageRepository.Where(x => !x.IsDeleted);
            query = query.Where(!string.IsNullOrEmpty(model.Name), x => x.Name.Contains(model.Name));

            if(!(model.SelectAreaId == null || model.SelectAreaId == -1))
            {
                //获取所有子节点选区id
                var areaIdList = (from x in _selectAreaRepository.AsQueryable(false).Where(x => !x.IsDeleted&&x.Id== model.SelectAreaId.Value)
                                  join x2 in _selectAreaRepository.AsQueryable(false).Where(x => !x.IsDeleted )
                                  on x.Id equals x2.ParentId into result1
                                  from c in result1.DefaultIfEmpty() where c!=null
                                  select c.Id).ToList();

                areaIdList.Add(model.SelectAreaId.Value);

                query = query.Where(x => areaIdList.Contains(x.SelectAreaId.Value));

            }

            //query = query.Where(!(model.SelectAreaId == null || model.SelectAreaId == -1), x => x.SelectAreaId == model.SelectAreaId);

            var query2 = query.Select(x => new DtoVillage()
            {
                Id = x.Id,
                Name = x.Name,
                SelectAreaId = model.SelectAreaId,
                GrossArea=x.GrossArea,
                Population=x.Population,
                //OfficialCount= _officialRepository.AsQueryable(false).Where(o=>!o.IsDeleted&&o.VillageId==x.Id).Count(),
                CreatedTime = x.CreatedTime,
                Creator = x.Creator,
                IsDeleted = x.IsDeleted,
                UpdatedTime = x.UpdatedTime,
                Updater = x.Updater,
               
            });


            //将数据映射到DtoVillage中
            return query.OrderByDescending(s => s.CreatedTime).ProjectToType<DtoVillage>().ToPagedList(model.PageIndex, model.PageSize);
        }

        /// <summary>
        /// 根据编号查询详情
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<DtoVillage> GetById(long id)
        {
            var entity = await _villageRepository.FindOrDefaultAsync(id);

            var dto = entity.Adapt<DtoVillage>();
            //dto.OfficialCount = _officialRepository.AsQueryable(false).Where(o => !o.IsDeleted && o.VillageId == dto.Id).Count();

            return dto;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Insert(DtoVillageForm model)
        {
            var entity = model.Adapt<Village>();
            entity.Id = YitIdHelper.NextId();
            entity.CreatedTime = DateTime.Now;
            entity.Creator = JwtHelper.GetAccountId();
            _villageRepository.Insert(entity);
            return await _villageRepository.SaveNowAsync();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(DtoVillageForm model)
        {
            //先根据id查询实体
            var entity = _villageRepository.FindOrDefault(model.Id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            //将模型数据映射给实体属性
            entity = model.Adapt(entity);
            entity.UpdatedTime = DateTime.Now;
            entity.Updater = JwtHelper.GetAccountId();
            _villageRepository.Update(entity);
            return await _villageRepository.SaveNowAsync();
        }

        /// <summary>
        /// 假删除
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<int> FakeDelete(long id)
        {
            var entity = _villageRepository.FindOrDefault(id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            entity.IsDeleted = true;
            return await _villageRepository.SaveNowAsync();
        }

        /// <summary>
        /// 批量假删除
        /// </summary>
        /// <param name="idList">编号</param>
        /// <returns></returns>
        public async Task<int> FakeDelete(List<long> idList)
        {
            var result = await _villageRepository.Context.BatchUpdate<Village>()
                .Where(x => idList.Contains(x.Id))
                .Set(x => x.IsDeleted, x => true)
                .ExecuteAsync();
            return result;
        }
    }
}