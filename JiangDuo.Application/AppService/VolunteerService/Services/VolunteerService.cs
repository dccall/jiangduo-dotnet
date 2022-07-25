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
using JiangDuo.Application.AppService.VolunteerService.Dto;
using Furion.FriendlyException;

namespace JiangDuo.Application.AppService.VolunteerService.Services
{
    public class VolunteerService:IVolunteerService, ITransient
    {
        private readonly ILogger<VolunteerService> _logger;
        private readonly IRepository<Volunteer> _volunteerRepository;
        public VolunteerService(ILogger<VolunteerService> logger, IRepository<Volunteer> volunteerRepository)
        {
            _logger = logger;
            _volunteerRepository = volunteerRepository;
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="model">数据</param>
        /// <returns></returns>
        public PagedList<DtoVolunteer> GetList(DtoVolunteerQuery model)
        {
            var query = _volunteerRepository.Where(x => !x.IsDeleted);
            query = query.Where(!string.IsNullOrEmpty(model.Name), x => x.Name.Contains(model.Name));

            //将数据映射到DtoVolunteer中
            return query.OrderByDescending(s=>s.CreatedTime).ProjectToType<DtoVolunteer>().ToPagedList(model.PageIndex, model.PageSize);
        }
        /// <summary>
        /// 根据编号查询详情
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<DtoVolunteer> GetById(long id)
        {
            var entity = await _volunteerRepository.FindOrDefaultAsync(id);

            var dto = entity.Adapt<DtoVolunteer>();

            return dto;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Insert(DtoVolunteerForm model)
        {

            var entity = model.Adapt<Volunteer>();
            entity.Id = YitIdHelper.NextId();
            entity.CreatedTime = DateTimeOffset.UtcNow;
            entity.Creator = JwtHelper.GetAccountId();
            _volunteerRepository.Insert(entity);
            return await _volunteerRepository.SaveNowAsync();
        }
     
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(DtoVolunteerForm model)
        {
            //先根据id查询实体
            var entity = _volunteerRepository.FindOrDefault(model.Id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            //将模型数据映射给实体属性
            entity = model.Adapt(entity);
            entity.UpdatedTime = DateTimeOffset.UtcNow;
            entity.Updater = JwtHelper.GetAccountId();
            _volunteerRepository.Update(entity);
            return await _volunteerRepository.SaveNowAsync();
        }
     
        /// <summary>
        /// 假删除
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<int> FakeDelete(long id)
        {
            var entity = _volunteerRepository.FindOrDefault(id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            entity.IsDeleted = true;
            return await _volunteerRepository.SaveNowAsync();
        }
        /// <summary>
        /// 批量假删除
        /// </summary>
        /// <param name="idList">编号</param>
        /// <returns></returns>
        public async Task<int> FakeDelete(List<long> idList)
        {
            var result = await _volunteerRepository.Context.BatchUpdate<Building>()
                .Where(x => idList.Contains(x.Id))
                .Set(x => x.IsDeleted, x => true)
                .ExecuteAsync();
            return result;
        }
    

    }
}
