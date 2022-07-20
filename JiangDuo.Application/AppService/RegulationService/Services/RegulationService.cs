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
using JiangDuo.Application.AppService.RegulationService.Dto;
using Furion.FriendlyException;

namespace JiangDuo.Application.AppService.RegulationService.Services
{
    public class RegulationService:IRegulationService, ITransient
    {
        private readonly ILogger<RegulationService> _logger;
        private readonly IRepository<Regulation> _regulationRepository;
        public RegulationService(ILogger<RegulationService> logger, IRepository<Regulation> regulationRepository)
        {
            _logger = logger;
            _regulationRepository = regulationRepository;
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="model">数据</param>
        /// <returns></returns>
        public PagedList<DtoRegulation> GetList(DtoRegulationQuery model)
        {
            var query = _regulationRepository.Where(x => !x.IsDeleted);
            query = query.Where(!string.IsNullOrEmpty(model.Name), x => x.Name.Contains(model.Name));

            //将数据映射到DtoRegulation中
            return query.OrderBy(s=>s.CreatedTime).ProjectToType<DtoRegulation>().ToPagedList(model.PageIndex, model.PageSize);
        }
        /// <summary>
        /// 根据编号查询详情
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<DtoRegulation> GetById(long id)
        {
            var entity = await _regulationRepository.FindOrDefaultAsync(id);

            var dto = entity.Adapt<DtoRegulation>();

            return dto;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Insert(DtoRegulationForm model)
        {

            var entity = model.Adapt<Regulation>();
            entity.Id = YitIdHelper.NextId();
            entity.CreatedTime = DateTimeOffset.UtcNow;
            entity.Creator = JwtHelper.GetUserId();
            _regulationRepository.Insert(entity);
            return await _regulationRepository.SaveNowAsync();
        }
     
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(DtoRegulationForm model)
        {
            //先根据id查询实体
            var entity = _regulationRepository.FindOrDefault(model.Id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            //将模型数据映射给实体属性
            entity = model.Adapt(entity);
            entity.UpdatedTime = DateTimeOffset.UtcNow;
            entity.Updater = JwtHelper.GetUserId();
            _regulationRepository.Update(entity);
            return await _regulationRepository.SaveNowAsync();
        }
     
        /// <summary>
        /// 假删除
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<int> FakeDelete(long id)
        {
            var entity = _regulationRepository.FindOrDefault(id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            entity.IsDeleted = true;
            return await _regulationRepository.SaveNowAsync();
        }
        /// <summary>
        /// 批量假删除
        /// </summary>
        /// <param name="idList">编号</param>
        /// <returns></returns>
        public async Task<int> FakeDelete(List<long> idList)
        {
            var result = await _regulationRepository.Context.BatchUpdate<Building>()
                .Set(x => x.IsDeleted, x => true)
                .Where(x => idList.Contains(x.Id))
                .ExecuteAsync();
            return result;
        }
    

    }
}
