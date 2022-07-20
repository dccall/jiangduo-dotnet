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
using JiangDuo.Application.AppService.SelectAreaService.Dto;
using Furion.FriendlyException;

namespace JiangDuo.Application.AppService.SelectAreaService.Services
{
    public class SelectAreaService:ISelectAreaService, ITransient
    {
        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILogger<SelectAreaService> _logger;
        /// <summary>
        /// SelectArea仓储
        /// </summary>
        private readonly IRepository<SelectArea> _configRepository;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志</param>
        /// <param name="configRepository">部门仓储</param>
        public SelectAreaService(ILogger<SelectAreaService> logger, IRepository<SelectArea> configRepository)
        {
            _logger = logger;
            _configRepository = configRepository;
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="model">数据</param>
        /// <returns></returns>
        public PagedList<DtoSelectArea> GetList(DtoSelectAreaQuery model)
        {
            var query = _configRepository.Where(x => !x.IsDeleted);
            query = query.Where(!string.IsNullOrEmpty(model.SelectAreaName), x => x.SelectAreaName.Contains(model.SelectAreaName));

            //将数据映射到DtoSelectArea中
            return query.OrderBy(s=>s.CreatedTime).ProjectToType<DtoSelectArea>().ToPagedList(model.PageIndex, model.PageSize);
        }
        /// <summary>
        /// 根据编号查询详情
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<DtoSelectArea> GetById(long id)
        {
            var entity = await _configRepository.FindOrDefaultAsync(id);

            var dto = entity.Adapt<DtoSelectArea>();

            return await Task.FromResult(dto);
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
            entity.CreatedTime = DateTimeOffset.UtcNow;
            entity.Creator = JwtHelper.GetUserId();
            _configRepository.Insert(entity);
            return await _configRepository.SaveNowAsync();
        }
     
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(DtoSelectAreaForm model)
        {
            //先根据id查询实体
            var entity = _configRepository.FindOrDefault(model.Id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            //将模型数据映射给实体属性
            entity = model.Adapt(entity);
            entity.UpdatedTime = DateTimeOffset.UtcNow;
            entity.Updater = JwtHelper.GetUserId();
            _configRepository.Update(entity);
            return await _configRepository.SaveNowAsync();
        }
     
        /// <summary>
        /// 假删除
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<int> FakeDelete(long id)
        {
            var entity = _configRepository.FindOrDefault(id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            entity.IsDeleted = true;
            return await _configRepository.SaveNowAsync();
        }
        /// <summary>
        /// 批量假删除
        /// </summary>
        /// <param name="idList">编号</param>
        /// <returns></returns>
        public async Task<int> FakeDelete(List<long> idList)
        {
            var result = await _configRepository.Context.BatchUpdate<SelectArea>()
                .Set(x => x.IsDeleted, x => true)
                .Where(x => idList.Contains(x.Id))
                .ExecuteAsync();
            return result;
        }
    

    }
}
