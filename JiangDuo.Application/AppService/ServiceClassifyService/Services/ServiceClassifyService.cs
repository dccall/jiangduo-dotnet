using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Furion.FriendlyException;
using JiangDuo.Application.AppService.ServiceClassifyService.Dto;
using JiangDuo.Core.Models;
using JiangDuo.Core.Utils;
using Mapster;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yitter.IdGenerator;

namespace JiangDuo.Application.AppService.ServiceClassifyService.Services
{
    public class ServiceClassifyService : IServiceClassifyService, ITransient
    {
        private readonly ILogger<ServiceClassifyService> _logger;
        private readonly IRepository<ServiceClassify> _serviceClassifyRepository;

        public ServiceClassifyService(ILogger<ServiceClassifyService> logger, IRepository<ServiceClassify> serviceClassifyRepository)
        {
            _logger = logger;
            _serviceClassifyRepository = serviceClassifyRepository;
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="model">数据</param>
        /// <returns></returns>
        public PagedList<DtoServiceClassify> GetList(DtoServiceClassifyQuery model)
        {
            var query = _serviceClassifyRepository.Where(x => !x.IsDeleted);
            query = query.Where(!string.IsNullOrEmpty(model.ClassifyName), x => x.ClassifyName.Contains(model.ClassifyName));

            //将数据映射到DtoServiceClassify中
            return query.OrderByDescending(s => s.CreatedTime).ProjectToType<DtoServiceClassify>().ToPagedList(model.PageIndex, model.PageSize);
        }

        /// <summary>
        /// 根据编号查询详情
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<DtoServiceClassify> GetById(long id)
        {
            var entity = await _serviceClassifyRepository.FindOrDefaultAsync(id);

            var dto = entity.Adapt<DtoServiceClassify>();

            return dto;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Insert(DtoServiceClassifyForm model)
        {
            var entity = model.Adapt<ServiceClassify>();
            entity.Id = YitIdHelper.NextId();
            entity.CreatedTime = DateTime.Now;
            entity.Creator = JwtHelper.GetAccountId();
            _serviceClassifyRepository.Insert(entity);
            return await _serviceClassifyRepository.SaveNowAsync();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(DtoServiceClassifyForm model)
        {
            //先根据id查询实体
            var entity = _serviceClassifyRepository.FindOrDefault(model.Id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            //将模型数据映射给实体属性
            entity = model.Adapt(entity);
            entity.UpdatedTime = DateTime.Now;
            entity.Updater = JwtHelper.GetAccountId();
            _serviceClassifyRepository.Update(entity);
            return await _serviceClassifyRepository.SaveNowAsync();
        }

        /// <summary>
        /// 假删除
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<int> FakeDelete(long id)
        {
            var entity = _serviceClassifyRepository.FindOrDefault(id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            entity.IsDeleted = true;
            return await _serviceClassifyRepository.SaveNowAsync();
        }

        /// <summary>
        /// 批量假删除
        /// </summary>
        /// <param name="idList">编号</param>
        /// <returns></returns>
        public async Task<int> FakeDelete(List<long> idList)
        {
            var result = await _serviceClassifyRepository.Context.BatchUpdate<ServiceClassify>()
                .Where(x => idList.Contains(x.Id))
                .Set(x => x.IsDeleted, x => true)
                .ExecuteAsync();
            return result;
        }
    }
}