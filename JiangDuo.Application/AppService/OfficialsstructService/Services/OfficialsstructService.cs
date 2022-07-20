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
using JiangDuo.Application.AppService.OfficialsstructService.Dto;
using Furion.FriendlyException;

namespace JiangDuo.Application.AppService.OfficialsstructService.Services
{
    public class OfficialsstructService:IOfficialsstructService, ITransient
    {
        private readonly ILogger<OfficialsstructService> _logger;
        private readonly IRepository<Officialsstruct> _officialsstructRepository;
        public OfficialsstructService(ILogger<OfficialsstructService> logger, IRepository<Officialsstruct> officialsstructRepository)
        {
            _logger = logger;
            _officialsstructRepository = officialsstructRepository;
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="model">数据</param>
        /// <returns></returns>
        public PagedList<DtoOfficialsstruct> GetList(DtoOfficialsstructQuery model)
        {
            var query = _officialsstructRepository.Where(x => !x.IsDeleted);
            query = query.Where(!string.IsNullOrEmpty(model.Name), x => x.Name.Contains(model.Name));

            //将数据映射到DtoOfficialsstruct中
            return query.OrderBy(s=>s.CreatedTime).ProjectToType<DtoOfficialsstruct>().ToPagedList(model.PageIndex, model.PageSize);
        }
        /// <summary>
        /// 根据编号查询详情
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<DtoOfficialsstruct> GetById(long id)
        {
            var entity = await _officialsstructRepository.FindOrDefaultAsync(id);

            var dto = entity.Adapt<DtoOfficialsstruct>();

            return await Task.FromResult(dto);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Insert(DtoOfficialsstructForm model)
        {

            var entity = model.Adapt<Officialsstruct>();
            entity.Id = YitIdHelper.NextId();
            entity.CreatedTime = DateTimeOffset.UtcNow;
            entity.Creator = JwtHelper.GetUserId();
            _officialsstructRepository.Insert(entity);
            return await _officialsstructRepository.SaveNowAsync();
        }
     
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(DtoOfficialsstructForm model)
        {
            //先根据id查询实体
            var entity = _officialsstructRepository.FindOrDefault(model.Id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            //将模型数据映射给实体属性
            entity = model.Adapt(entity);
            entity.UpdatedTime = DateTimeOffset.UtcNow;
            entity.Updater = JwtHelper.GetUserId();
            _officialsstructRepository.Update(entity);
            return await _officialsstructRepository.SaveNowAsync();
        }
     
        /// <summary>
        /// 假删除
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<int> FakeDelete(long id)
        {
            var entity = _officialsstructRepository.FindOrDefault(id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            entity.IsDeleted = true;
            return await _officialsstructRepository.SaveNowAsync();
        }
        /// <summary>
        /// 批量假删除
        /// </summary>
        /// <param name="idList">编号</param>
        /// <returns></returns>
        public async Task<int> FakeDelete(List<long> idList)
        {
            var result = await _officialsstructRepository.Context.BatchUpdate<Building>()
                .Set(x => x.IsDeleted, x => true)
                .Where(x => idList.Contains(x.Id))
                .ExecuteAsync();
            return result;
        }
    

    }
}
