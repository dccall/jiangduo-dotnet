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
using JiangDuo.Application.AppService.BusinessService.Dto;
using Furion.FriendlyException;

namespace JiangDuo.Application.AppService.BusinessService.Services
{
    public class BusinessService:IBusinessService, ITransient
    {
        private readonly ILogger<BusinessService> _logger;
        private readonly IRepository<Business> _businessRepository;
        public BusinessService(ILogger<BusinessService> logger, IRepository<Business> businessRepository)
        {
            _logger = logger;
            _businessRepository = businessRepository;
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="model">数据</param>
        /// <returns></returns>
        public PagedList<DtoBusiness> GetList(DtoBusinessQuery model)
        {
            var query = _businessRepository.Where(x => !x.IsDeleted);
            query = query.Where(!string.IsNullOrEmpty(model.Name), x => x.Name.Contains(model.Name));

            //将数据映射到DtoBusiness中
            return query.OrderByDescending(s=>s.CreatedTime).ProjectToType<DtoBusiness>().ToPagedList(model.PageIndex, model.PageSize);
        }
        /// <summary>
        /// 根据id查询详情
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public async Task<DtoBusiness> GetById(long id)
        {
            var entity = await _businessRepository.FindOrDefaultAsync(id);

            var dto = entity.Adapt<DtoBusiness>();

            return dto;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Insert(DtoBusinessForm model)
        {

            var entity = model.Adapt<Business>();
            entity.Id = YitIdHelper.NextId();
            entity.CreatedTime = DateTime.Now;
            entity.Creator = JwtHelper.GetAccountId();
            _businessRepository.Insert(entity);
            return await _businessRepository.SaveNowAsync();
        }
     
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(DtoBusinessForm model)
        {
            //先根据id查询实体
            var entity = _businessRepository.FindOrDefault(model.Id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            //将模型数据映射给实体属性
            entity = model.Adapt(entity);
            entity.UpdatedTime = DateTime.Now;
            entity.Updater = JwtHelper.GetAccountId();
            _businessRepository.Update(entity);
            return await _businessRepository.SaveNowAsync();
        }
     
        /// <summary>
        /// 假删除
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<int> FakeDelete(long id)
        {
            var entity = _businessRepository.FindOrDefault(id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            entity.IsDeleted = true;
            return await _businessRepository.SaveNowAsync();
        }
        /// <summary>
        /// 批量假删除
        /// </summary>
        /// <param name="idList">编号</param>
        /// <returns></returns>
        public async Task<int> FakeDelete(List<long> idList)
        {
            var result = await _businessRepository.Context.BatchUpdate<Business>()
                .Where(x => idList.Contains(x.Id))
                .Set(x => x.IsDeleted, x => true)
                .ExecuteAsync();
            return result;
        }
    

    }
}
