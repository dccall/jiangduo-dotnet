using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Furion.FriendlyException;
using JiangDuo.Application.AppService.OfficialService.Dto;
using JiangDuo.Core.Models;
using JiangDuo.Core.Utils;
using Mapster;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yitter.IdGenerator;

namespace JiangDuo.Application.AppService.OfficialService.Services
{
    public class OfficialService : IOfficialService, ITransient
    {
        private readonly ILogger<OfficialService> _logger;
        private readonly IRepository<Official> _officialRepository;
        private readonly IRepository<SysUploadFile> _uploadRepository;

        public OfficialService(ILogger<OfficialService> logger,
            IRepository<SysUploadFile> uploadRepository,
            IRepository<Official> officialRepository)
        {
            _logger = logger;
            _officialRepository = officialRepository;
            _uploadRepository = uploadRepository;
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="model">数据</param>
        /// <returns></returns>
        public PagedList<DtoOfficial> GetList(DtoOfficialQuery model)
        {
            var query = _officialRepository.Where(x => !x.IsDeleted);
            query = query.Where(!string.IsNullOrEmpty(model.Name), x => x.Name.Contains(model.Name));
            //不传或者传-1查询全部
            query = query.Where(!(model.SelectAreaId == null || model.SelectAreaId == -1), x => x.SelectAreaId == model.SelectAreaId);
            query = query.Where(model.OfficialRole != null, x => x.OfficialRole == model.OfficialRole);
            query = query.Where(!(model.CategoryId == null || model.CategoryId == -1), x => x.CategoryId == model.CategoryId);

            //将数据映射到DtoOfficial中
            return query.OrderByDescending(s => s.CreatedTime).ProjectToType<DtoOfficial>().ToPagedList(model.PageIndex, model.PageSize);
        }

        /// <summary>
        /// 根据编号查询详情
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<DtoOfficial> GetById(long id)
        {
            var entity = await _officialRepository.FindOrDefaultAsync(id);

            var dto = entity.Adapt<DtoOfficial>();

            if (dto != null && !string.IsNullOrEmpty(dto.Avatar))
            {
                var idList = dto.Avatar.Split(',').ToList();
                dto.AvatarList = _uploadRepository.Where(x => idList.Contains(x.FileId.ToString())).ToList();
            }

            return dto;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Insert(DtoOfficialForm model)
        {
            var entity = model.Adapt<Official>();
            entity.Id = YitIdHelper.NextId();
            entity.CreatedTime = DateTime.Now;
            entity.Creator = JwtHelper.GetAccountId();

            if (model.AvatarList != null && model.AvatarList.Any())
            {
                entity.Avatar = String.Join(",", model.AvatarList.Select(x => x.FileId));
            }

            _officialRepository.Insert(entity);
            return await _officialRepository.SaveNowAsync();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(DtoOfficialForm model)
        {
            //先根据id查询实体
            var entity = _officialRepository.FindOrDefault(model.Id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            //将模型数据映射给实体属性
            entity = model.Adapt(entity);
            entity.UpdatedTime = DateTime.Now;
            entity.Updater = JwtHelper.GetAccountId();
            if (model.AvatarList != null && model.AvatarList.Any())
            {
                entity.Avatar = String.Join(",", model.AvatarList.Select(x => x.FileId));
            }
            _officialRepository.Update(entity);
            return await _officialRepository.SaveNowAsync();
        }

        /// <summary>
        /// 假删除
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<int> FakeDelete(long id)
        {
            var entity = _officialRepository.FindOrDefault(id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            entity.IsDeleted = true;
            return await _officialRepository.SaveNowAsync();
        }

        /// <summary>
        /// 批量假删除
        /// </summary>
        /// <param name="idList">编号</param>
        /// <returns></returns>
        public async Task<int> FakeDelete(List<long> idList)
        {
            var result = await _officialRepository.Context.BatchUpdate<Official>()
                .Where(x => idList.Contains(x.Id))
                .Set(x => x.IsDeleted, x => true)
                .ExecuteAsync();
            return result;
        }
    }
}