using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Furion.FriendlyException;
using JiangDuo.Application.AppService.OfficialsstructService.Dto;
using JiangDuo.Core.Models;
using JiangDuo.Core.Utils;
using Mapster;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yitter.IdGenerator;

namespace JiangDuo.Application.AppService.OfficialsstructService.Services
{
    public class OfficialsstructService : IOfficialsstructService, ITransient
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
            return query.OrderByDescending(s => s.CreatedTime).ProjectToType<DtoOfficialsstruct>().ToPagedList(model.PageIndex, model.PageSize);
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

            return dto;
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
            entity.CreatedTime = DateTime.Now;
            entity.Creator = JwtHelper.GetAccountId();
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
            entity.UpdatedTime = DateTime.Now;
            entity.Updater = JwtHelper.GetAccountId();
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
            var result = await _officialsstructRepository.Context.BatchUpdate<Officialsstruct>()
                .Where(x => idList.Contains(x.Id))
                .Set(x => x.IsDeleted, x => true)
                .ExecuteAsync();
            return result;
        }
    }
}