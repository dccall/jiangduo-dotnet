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
using JiangDuo.Application.AppService.ResidentService.Dto;
using Furion.FriendlyException;

namespace JiangDuo.Application.AppService.ResidentService.Services
{
    public class ResidentService:IResidentService, ITransient
    {
        private readonly ILogger<ResidentService> _logger;
        private readonly IRepository<Resident> _residentRepository;
        public ResidentService(ILogger<ResidentService> logger, IRepository<Resident> residentRepository)
        {
            _logger = logger;
            _residentRepository = residentRepository;
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="model">数据</param>
        /// <returns></returns>
        public PagedList<DtoResident> GetList(DtoResidentQuery model)
        {
            var query = _residentRepository.Where(x => !x.IsDeleted);
            query = query.Where(!string.IsNullOrEmpty(model.Name), x => x.Name.Contains(model.Name));

            //将数据映射到DtoResident中
            return query.OrderByDescending(s=>s.CreatedTime).ProjectToType<DtoResident>().ToPagedList(model.PageIndex, model.PageSize);
        }
        /// <summary>
        /// 根据编号查询详情
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<DtoResident> GetById(long id)
        {
            var entity = await _residentRepository.FindOrDefaultAsync(id);

            var dto = entity.Adapt<DtoResident>();

            return dto;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Insert(DtoResidentForm model)
        {

            var entity = model.Adapt<Resident>();
            entity.Id = YitIdHelper.NextId();
            entity.CreatedTime = DateTime.Now;
            entity.Creator = JwtHelper.GetAccountId();
            _residentRepository.Insert(entity);
            return await _residentRepository.SaveNowAsync();
        }
     
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(DtoResidentForm model)
        {
            //先根据id查询实体
            var entity = _residentRepository.FindOrDefault(model.Id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            //将模型数据映射给实体属性
            entity = model.Adapt(entity);
            entity.UpdatedTime = DateTime.Now;
            entity.Updater = JwtHelper.GetAccountId();
            _residentRepository.Update(entity);
            return await _residentRepository.SaveNowAsync();
        }
     
        /// <summary>
        /// 假删除
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<int> FakeDelete(long id)
        {
            var entity = _residentRepository.FindOrDefault(id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            entity.IsDeleted = true;
            return await _residentRepository.SaveNowAsync();
        }
        /// <summary>
        /// 批量假删除
        /// </summary>
        /// <param name="idList">编号</param>
        /// <returns></returns>
        public async Task<int> FakeDelete(List<long> idList)
        {
            var result = await _residentRepository.Context.BatchUpdate<Resident>()
                .Where(x => idList.Contains(x.Id))
                .Set(x => x.IsDeleted, x => true)
                .ExecuteAsync();
            return result;
        }
    

    }
}
