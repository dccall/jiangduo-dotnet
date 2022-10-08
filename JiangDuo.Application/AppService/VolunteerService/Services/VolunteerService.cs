using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Furion.FriendlyException;
using JiangDuo.Application.AppService.VolunteerService.Dto;
using JiangDuo.Core.Models;
using JiangDuo.Core.Utils;
using Mapster;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yitter.IdGenerator;

namespace JiangDuo.Application.AppService.VolunteerService.Services
{
    public class VolunteerService : IVolunteerService, ITransient
    {
        private readonly ILogger<VolunteerService> _logger;
        private readonly IRepository<Volunteer> _volunteerRepository;
        private readonly IRepository<SelectArea> _selectAreaRepository;
        public VolunteerService(ILogger<VolunteerService> logger,
            IRepository<SelectArea> selectAreaRepository,
            IRepository<Volunteer> volunteerRepository)
        {
            _logger = logger;
            _volunteerRepository = volunteerRepository;
            _selectAreaRepository = selectAreaRepository;
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
            //不传或者传-1查询全部
            //query = query.Where(!(model.SelectAreaId == null || model.SelectAreaId == -1), x => x.SelectAreaId == model.SelectAreaId);

            if (!(model.SelectAreaId == null || model.SelectAreaId == -1))
            {
                //获取所有子节点选区id
                var areaIdList = (from x in _selectAreaRepository.AsQueryable(false).Where(x => !x.IsDeleted && x.Id == model.SelectAreaId.Value)
                                  join x2 in _selectAreaRepository.AsQueryable(false).Where(x => !x.IsDeleted )
                                  on x.Id equals x2.ParentId into result1
                                  from c in result1.DefaultIfEmpty()
                                  where c != null
                                  select c.Id).ToList();

                areaIdList.Add(model.SelectAreaId.Value);

                query = query.Where(x => areaIdList.Contains(x.SelectAreaId.Value));

            }

            //将数据映射到DtoVolunteer中
            return query.OrderByDescending(s => s.CreatedTime).ProjectToType<DtoVolunteer>().ToPagedList(model.PageIndex, model.PageSize);
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
            entity.CreatedTime = DateTime.Now;
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
            entity.UpdatedTime = DateTime.Now;
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
            var result = await _volunteerRepository.Context.BatchUpdate<Volunteer>()
                .Where(x => idList.Contains(x.Id))
                .Set(x => x.IsDeleted, x => true)
                .ExecuteAsync();
            return result;
        }
    }
}