using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Furion.FriendlyException;
using JiangDuo.Application.AppService.SelectAreaService.Dto;
using JiangDuo.Core.Models;
using JiangDuo.Core.Utils;
using Mapster;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yitter.IdGenerator;

namespace JiangDuo.Application.AppService.SelectAreaService.Services
{
    public class SelectAreaService : ISelectAreaService, ITransient
    {
        private readonly ILogger<SelectAreaService> _logger;
        private readonly IRepository<SelectArea> _selectAreaRepository;
        private readonly IRepository<Village> _villageRepository;

        public SelectAreaService(ILogger<SelectAreaService> logger,
            IRepository<Village> villageRepository,
            IRepository<SelectArea> selectAreaRepository)
        {
            _logger = logger;
            _villageRepository = villageRepository;
            _selectAreaRepository = selectAreaRepository;
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="model">数据</param>
        /// <returns></returns>
        public PagedList<DtoSelectArea> GetList(DtoSelectAreaQuery model)
        {
            var query = _selectAreaRepository.AsQueryable(false).Where(x => !x.IsDeleted);
            query = query.Where(!string.IsNullOrEmpty(model.SelectAreaName), x => x.SelectAreaName.Contains(model.SelectAreaName));
            query = query.Where(model.SelectAreaType != null, x => x.SelectAreaType == model.SelectAreaType);

            var query2 = query.Select(x => new DtoSelectArea()
            {
                Id = x.Id,
                SelectAreaName = x.SelectAreaName,
                SelectAreaRange = x.SelectAreaRange,
                Area = x.Area,
                City = x.City,
                ParentId = x.ParentId,
                Province = x.Province,
                SelectAreaType = x.SelectAreaType,
                CreatedTime = x.CreatedTime,
                Creator = x.Creator,
                IsDeleted = x.IsDeleted,
                UpdatedTime = x.UpdatedTime,
                Updater = x.Updater,
                GrossArea = _villageRepository.AsQueryable(false).Where(v => !v.IsDeleted && v.SelectAreaId == x.Id).Select(y => y.GrossArea).Sum(),
                Population = _villageRepository.AsQueryable(false).Where(v => !v.IsDeleted && v.SelectAreaId == x.Id).Select(y => y.Population).Sum(),

            });

            //将数据映射到DtoSelectArea中
            return query2.OrderBy(s => s.SelectAreaName).ToPagedList(model.PageIndex, model.PageSize);
        }

        /// <summary>
        /// 根据编号查询详情
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<DtoSelectArea> GetById(long id)
        {
            var entity = await _selectAreaRepository.FindOrDefaultAsync(id);
            entity.GrossArea = _villageRepository.AsQueryable(false).Where(v => !v.IsDeleted && v.SelectAreaId == entity.Id).Select(y => y.GrossArea).Sum();
            entity.Population = _villageRepository.AsQueryable(false).Where(v => !v.IsDeleted && v.SelectAreaId == entity.Id).Select(y => y.Population).Sum();

            var dto = entity.Adapt<DtoSelectArea>();

            return dto;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Insert(DtoSelectAreaForm model)
        {
            var entity = model.Adapt<SelectArea>();
            entity.Id = YitIdHelper.NextId();
            entity.CreatedTime = DateTime.Now;
            entity.Creator = JwtHelper.GetAccountId();
            _selectAreaRepository.Insert(entity);
            return await _selectAreaRepository.SaveNowAsync();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(DtoSelectAreaForm model)
        {
            //先根据id查询实体
            var entity = _selectAreaRepository.FindOrDefault(model.Id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            //将模型数据映射给实体属性
            entity = model.Adapt(entity);
            entity.UpdatedTime = DateTime.Now;
            entity.Updater = JwtHelper.GetAccountId();
            _selectAreaRepository.Update(entity);
            return await _selectAreaRepository.SaveNowAsync();
        }

        /// <summary>
        /// 假删除
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<int> FakeDelete(long id)
        {
            var entity = _selectAreaRepository.FindOrDefault(id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            entity.IsDeleted = true;
            return await _selectAreaRepository.SaveNowAsync();
        }

        /// <summary>
        /// 批量假删除
        /// </summary>
        /// <param name="idList">编号</param>
        /// <returns></returns>
        public async Task<int> FakeDelete(List<long> idList)
        {
            var result = await _selectAreaRepository.Context.BatchUpdate<SelectArea>()
                .Where(x => idList.Contains(x.Id))
                .Set(x => x.IsDeleted, x => true)
                .ExecuteAsync();
            return result;
        }
    }
}