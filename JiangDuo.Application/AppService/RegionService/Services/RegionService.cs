using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Furion.FriendlyException;
using JiangDuo.Application.AppService.RegionService.Dto;
using JiangDuo.Core.Models;
using Mapster;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yitter.IdGenerator;

namespace JiangDuo.Application.AppService.RegionService.Services
{
    public class RegionService : IRegionService, ITransient
    {
        private readonly ILogger<RegionService> _logger;
        private readonly IRepository<SysRegion> _regionRepository;

        public RegionService(ILogger<RegionService> logger, IRepository<SysRegion> regionRepository)
        {
            _logger = logger;
            _regionRepository = regionRepository;
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="model">数据</param>
        /// <returns></returns>
        public PagedList<DtoRegion> GetList(DtoRegionQuery model)
        {
            var query = _regionRepository.AsQueryable();
            query = query.Where(model.RegionLevel != null, x => x.RegionLevel == model.RegionLevel);
            query = query.Where(model.RegionParentId != null, x => x.RegionParentId == model.RegionParentId);
            //将数据映射到DtoRegion中
            return query.OrderByDescending(s => s.RegionName).ProjectToType<DtoRegion>().ToPagedList(model.PageIndex, model.PageSize);
        }

        /// <summary>
        /// 根据编号查询详情
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<DtoRegion> GetById(long id)
        {
            var entity = await _regionRepository.FindOrDefaultAsync(id);

            var dto = entity.Adapt<DtoRegion>();

            return dto;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Insert(DtoRegionForm model)
        {
            var entity = model.Adapt<SysRegion>();
            entity.RegionId = YitIdHelper.NextId();
            _regionRepository.Insert(entity);
            return await _regionRepository.SaveNowAsync();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(DtoRegionForm model)
        {
            //先根据id查询实体
            var entity = _regionRepository.FindOrDefault(model.Id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            //将模型数据映射给实体属性
            entity = model.Adapt(entity);
            _regionRepository.Update(entity);
            return await _regionRepository.SaveNowAsync();
        }

        /// <summary>
        /// 假删除
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<int> FakeDelete(long id)
        {
            var entity = _regionRepository.FindOrDefault(id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            return await _regionRepository.SaveNowAsync();
        }

        /// <summary>
        /// 批量假删除
        /// </summary>
        /// <param name="idList">编号</param>
        /// <returns></returns>
        public async Task<int> FakeDelete(List<long> idList)
        {
            var result = await _regionRepository.Context.BatchUpdate<Building>()
                .Where(x => idList.Contains(x.Id))
                .Set(x => x.IsDeleted, x => true)
                .ExecuteAsync();
            return result;
        }
    }
}