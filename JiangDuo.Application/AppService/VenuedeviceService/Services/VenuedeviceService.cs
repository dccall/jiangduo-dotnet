﻿using JiangDuo.Application.System.Config.Dto;
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
using JiangDuo.Application.AppService.VenuedeviceService.Dto;
using Furion.FriendlyException;

namespace JiangDuo.Application.AppService.VenuedeviceService.Services
{
    public class VenuedeviceService:IVenuedeviceService, ITransient
    {
        private readonly ILogger<VenuedeviceService> _logger;
        private readonly IRepository<Venuedevice> _venuedeviceRepository;
        private readonly IRepository<Regulation> _regulationRepository;
        
        private readonly IRepository<Building> _buiildingRepository;
        public VenuedeviceService(ILogger<VenuedeviceService> logger,
            IRepository<Regulation> regulationRepository,
            IRepository<Venuedevice> venuedeviceRepository, IRepository<Building> buiildingRepository)
        {
            _logger = logger;
            _venuedeviceRepository = venuedeviceRepository;
            _buiildingRepository = buiildingRepository;
            _regulationRepository = regulationRepository;
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="model">数据</param>
        /// <returns></returns>
        public PagedList<DtoVenuedevice> GetList(DtoVenuedeviceQuery model)
        {
            var query = _venuedeviceRepository.Where(x => !x.IsDeleted);
            query = query.Where(!string.IsNullOrEmpty(model.VenuedeviceName), x => x.Name.Contains(model.VenuedeviceName));
            var pageList = query.OrderByDescending(s => s.CreatedTime).ProjectToType<DtoVenuedevice>().ToPagedList(model.PageIndex, model.PageSize);
            if (pageList.Items.Count() > 0)
            {
                var buildingIdList= pageList.Items.Select(x => x.BuildingId).Distinct().ToList();
                var buildingList= _buiildingRepository.Where(x => buildingIdList.Contains(x.Id)).ToList();
                foreach (var item in pageList.Items)
                {
                    var entity= buildingList.Where(x => x.Id == item.BuildingId).FirstOrDefault();
                    item.BuildingName = entity?.BuildingName;
                }
            }
            
            return pageList;


            //将数据映射到DtoVenuedevice中
            //return query.OrderByDescending(s=>s.CreatedTime).ProjectToType<DtoVenuedevice>().ToPagedList(model.PageIndex, model.PageSize);
        }
        /// <summary>
        /// 根据编号查询详情
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<DtoVenuedevice> GetById(long id)
        {
            var entity = await _venuedeviceRepository.FindOrDefaultAsync(id);

            var dto = entity.Adapt<DtoVenuedevice>();
            if (dto!=null)
            {
                //获取规章制度
                dto.Regulation = _regulationRepository.FindOrDefault(dto.RegulationId);
            }
            return dto;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Insert(DtoVenuedeviceForm model)
        {
            var entity = model.Adapt<Venuedevice>();
            entity.Id = YitIdHelper.NextId();
            entity.CreatedTime = DateTime.Now;
            entity.Creator = JwtHelper.GetAccountId();
            _venuedeviceRepository.Insert(entity);
            return await _venuedeviceRepository.SaveNowAsync();
        }
     
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(DtoVenuedeviceForm model)
        {
            //先根据id查询实体
            var entity= _venuedeviceRepository.FindOrDefault(model.Id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            //将模型数据映射给实体属性
            entity= model.Adapt(entity);
            entity.UpdatedTime = DateTime.Now;
            entity.Updater = JwtHelper.GetAccountId();
            _venuedeviceRepository.Update(entity);
            return await _venuedeviceRepository.SaveNowAsync();
        }
     
        /// <summary>
        /// 假删除
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<int> FakeDelete(long id)
        {
            var entity = _venuedeviceRepository.FindOrDefault(id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            entity.IsDeleted = true;
            return await _venuedeviceRepository.SaveNowAsync();
        }
        /// <summary>
        /// 批量假删除
        /// </summary>
        /// <param name="idList">编号</param>
        /// <returns></returns>
        public async Task<int> FakeDelete(List<long> idList)
        {
            var result = await _venuedeviceRepository.Context.BatchUpdate<Venuedevice>()
                .Where(x => idList.Contains(x.Id))
                .Set(x => x.IsDeleted, x => true)
                .ExecuteAsync();
            return result;
        }
    

    }
}
