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
using Furion.FriendlyException;

namespace JiangDuo.Application.AppService.BuildingService.Services
{
    public class BuildingService : IBuildingService, ITransient
    {
        private readonly ILogger<BuildingService> _logger;
        private readonly IRepository<Building> _buiildingRepository;
        private readonly IRepository<SysUploadFile> _uploadRepository;
        public BuildingService(ILogger<BuildingService> logger, IRepository<Building> buiildingRepository, IRepository<SysUploadFile> uploadRepository)
        {
            _logger = logger;
            _buiildingRepository = buiildingRepository;
            _uploadRepository = uploadRepository;
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="model">数据</param>
        /// <returns></returns>
        public PagedList<DtoBuilding> GetList(DtoBuildingQuery model)
        {
            var query = _buiildingRepository.Where(x => !x.IsDeleted);
            query = query.Where(!string.IsNullOrEmpty(model.BuildingName), x => x.BuildingName.Contains(model.BuildingName));
            //不传或者传-1查询全部
            query = query.Where(!(model.SelectAreaId == null||model.SelectAreaId == -1), x => x.SelectAreaId == model.SelectAreaId);

            //将数据映射到DtoBuilding中
           return query.OrderByDescending(s => s.CreatedTime).ProjectToType<DtoBuilding>().ToPagedList(model.PageIndex, model.PageSize);
        }
        /// <summary>
        /// 根据编号查询详情
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<DtoBuilding> GetById(long id)
        {
            var entity = await _buiildingRepository.FindOrDefaultAsync(id);

            var dto = entity.Adapt<DtoBuilding>();

            if (dto!=null&&!string.IsNullOrEmpty(dto.Images))
            {
                var idList = dto.Images.Split(',').ToList();
                dto.ImageList = _uploadRepository.Where(x => idList.Contains(x.FileId.ToString())).ToList();
            }

            return dto;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Insert(DtoBuildingForm model)
        {
            var entity = model.Adapt<Building>();
            entity.Id = YitIdHelper.NextId();
            entity.CreatedTime = DateTime.Now;
            entity.Creator = JwtHelper.GetAccountId();

            if (model.ImageList != null && model.ImageList.Any())
            {
                entity.Images = String.Join(",", model.ImageList.Select(x => x.FileId));
            }

            _buiildingRepository.Insert(entity);
            return await _buiildingRepository.SaveNowAsync();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(DtoBuildingForm model)
        {
            //先根据id查询实体
            var entity = _buiildingRepository.FindOrDefault(model.Id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            //将模型数据映射给实体属性
            entity = model.Adapt(entity);
            entity.UpdatedTime = DateTime.Now;
            entity.Updater = JwtHelper.GetAccountId();

            if (model.ImageList != null && model.ImageList.Any())
            {
                entity.Images = String.Join(",", model.ImageList.Select(x => x.FileId));
            }

            _buiildingRepository.Update(entity);
            return await _buiildingRepository.SaveNowAsync();
        }

        /// <summary>
        /// 假删除
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<int> FakeDelete(long id)
        {
            var entity = _buiildingRepository.FindOrDefault(id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            entity.IsDeleted = true;
            return await _buiildingRepository.SaveNowAsync();
        }
        /// <summary>
        /// 批量假删除
        /// </summary>
        /// <param name="idList">编号</param>
        /// <returns></returns>
        public async Task<int> FakeDelete(List<long> idList)
        {
            var result = await _buiildingRepository.Context.BatchUpdate<Building>()
                .Where(x => idList.Contains(x.Id))
                .Set(x => x.IsDeleted, x => true)
                .ExecuteAsync();
            return result;
        }


    }
}
