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
using JiangDuo.Application.AppService.PublicSentimentService.Dto;
using Furion.FriendlyException;

namespace JiangDuo.Application.AppService.PublicSentimentService.Services
{
    public class PublicSentimentService:IPublicSentimentService, ITransient
    {
        private readonly ILogger<PublicSentimentService> _logger;
        private readonly IRepository<PublicSentiment> _publicSentimentRepository;

        
        public PublicSentimentService(ILogger<PublicSentimentService> logger, IRepository<PublicSentiment> publicSentimentRepository)
        {
            _logger = logger;
            _publicSentimentRepository = publicSentimentRepository;
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="model">数据</param>
        /// <returns></returns>
        public PagedList<DtoPublicSentiment> GetList(DtoPublicSentimentQuery model)
        {
            var query = _publicSentimentRepository.Where(x => !x.IsDeleted);
            query = query.Where(model.BusinessId!=null, x => x.BusinessId==model.BusinessId);

            //将数据映射到DtoPublicSentiment中
            return query.OrderBy(s=>s.CreatedTime).ProjectToType<DtoPublicSentiment>().ToPagedList(model.PageIndex, model.PageSize);
        }
        /// <summary>
        /// 根据id查询详情
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public async Task<DtoPublicSentiment> GetById(long id)
        {
            var entity = await _publicSentimentRepository.FindOrDefaultAsync(id);

            var dto = entity.Adapt<DtoPublicSentiment>();

            return dto;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Insert(DtoPublicSentimentForm model)
        {
            var account = JwtHelper.GetAccountInfo();
            var entity = model.Adapt<PublicSentiment>();
            entity.Id = YitIdHelper.NextId();
            entity.CreatedTime = DateTime.Now;
            entity.Creator = account.Id;
            entity.SelectAreaId = account.SelectAreaId;
            _publicSentimentRepository.Insert(entity);
            return await _publicSentimentRepository.SaveNowAsync();
        }
     
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(DtoPublicSentimentForm model)
        {
            //先根据id查询实体
            var entity = _publicSentimentRepository.FindOrDefault(model.Id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            //将模型数据映射给实体属性
            entity = model.Adapt(entity);
            entity.UpdatedTime = DateTime.Now;
            entity.Updater = JwtHelper.GetAccountId();
            _publicSentimentRepository.Update(entity);
            return await _publicSentimentRepository.SaveNowAsync();
        }
     
        /// <summary>
        /// 假删除
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<int> FakeDelete(long id)
        {
            var entity = _publicSentimentRepository.FindOrDefault(id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            entity.IsDeleted = true;
            return await _publicSentimentRepository.SaveNowAsync();
        }
        /// <summary>
        /// 批量假删除
        /// </summary>
        /// <param name="idList">编号</param>
        /// <returns></returns>
        public async Task<int> FakeDelete(List<long> idList)
        {
            var result = await _publicSentimentRepository.Context.BatchUpdate<PublicSentiment>()
                .Where(x => idList.Contains(x.Id))
                .Set(x => x.IsDeleted, x => true)
                .ExecuteAsync();
            return result;
        }

    

    }
}
