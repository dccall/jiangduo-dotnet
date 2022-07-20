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
using JiangDuo.Application.AppService.NewsclassifyService.Dto;
using Furion.FriendlyException;

namespace JiangDuo.Application.AppService.NewsclassifyService.Services
{
    public class NewsclassifyService:INewsclassifyService, ITransient
    {
        private readonly ILogger<NewsclassifyService> _logger;
        private readonly IRepository<Newsclassify> _newsclassifyRepository;
        public NewsclassifyService(ILogger<NewsclassifyService> logger, IRepository<Newsclassify> newsclassifyRepository)
        {
            _logger = logger;
            _newsclassifyRepository = newsclassifyRepository;
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="model">数据</param>
        /// <returns></returns>
        public PagedList<DtoNewsclassify> GetList(DtoNewsclassifyQuery model)
        {
            var query = _newsclassifyRepository.Where(x => !x.IsDeleted);
            query = query.Where(!string.IsNullOrEmpty(model.ClassifyName), x => x.ClassifyName.Contains(model.ClassifyName));

            //将数据映射到DtoNewsclassify中
            return query.OrderBy(s=>s.CreatedTime).ProjectToType<DtoNewsclassify>().ToPagedList(model.PageIndex, model.PageSize);
        }
        /// <summary>
        /// 根据编号查询详情
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<DtoNewsclassify> GetById(long id)
        {
            var entity = await _newsclassifyRepository.FindOrDefaultAsync(id);

            var dto = entity.Adapt<DtoNewsclassify>();

            return await Task.FromResult(dto);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Insert(DtoNewsclassifyForm model)
        {

            var entity = model.Adapt<Newsclassify>();
            entity.Id = YitIdHelper.NextId();
            entity.CreatedTime = DateTimeOffset.UtcNow;
            entity.Creator = JwtHelper.GetUserId();
            _newsclassifyRepository.Insert(entity);
            return await _newsclassifyRepository.SaveNowAsync();
        }
     
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(DtoNewsclassifyForm model)
        {
            //先根据id查询实体
            var entity = _newsclassifyRepository.FindOrDefault(model.Id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            //将模型数据映射给实体属性
            entity = model.Adapt(entity);
            entity.UpdatedTime = DateTimeOffset.UtcNow;
            entity.Updater = JwtHelper.GetUserId();
            _newsclassifyRepository.Update(entity);
            return await _newsclassifyRepository.SaveNowAsync();
        }
     
        /// <summary>
        /// 假删除
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<int> FakeDelete(long id)
        {
            var entity = _newsclassifyRepository.FindOrDefault(id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            entity.IsDeleted = true;
            return await _newsclassifyRepository.SaveNowAsync();
        }
        /// <summary>
        /// 批量假删除
        /// </summary>
        /// <param name="idList">编号</param>
        /// <returns></returns>
        public async Task<int> FakeDelete(List<long> idList)
        {
            var result = await _newsclassifyRepository.Context.BatchUpdate<Building>()
                .Set(x => x.IsDeleted, x => true)
                .Where(x => idList.Contains(x.Id))
                .ExecuteAsync();
            return result;
        }
    

    }
}
