using JiangDuo.Application.System.DictItem.Dto;
using JiangDuo.Application.Tools;
using JiangDuo.Core.Enums;
using JiangDuo.Core.Models;
using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yitter.IdGenerator;
using JiangDuo.Core.Utils;

namespace JiangDuo.Application.System.DictItem.Services
{
    public class DictItemService: IDictItemService, ITransient
    {
        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILogger<DictItemService> _logger;
        /// <summary>
        /// SysDictItem仓储
        /// </summary>
        private readonly IRepository<SysDictItem> _dictItemRepository;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志</param>
        /// <param name="dictItemRepository">字典项仓储</param>
        public DictItemService(ILogger<DictItemService> logger, IRepository<SysDictItem> dictItemRepository)
        {
            _logger = logger;
            _dictItemRepository = dictItemRepository;
        }
        /// <summary>
        /// 分页,一对多分页
        /// </summary>
        /// <param name="model">数据</param>
        /// <returns></returns>
        public PagedList<DictItemDto> GetList(DictItemRequest model)
        {
            //将数据映射到DictItemDto中
            return _dictItemRepository.Where(x => !x.IsDeleted).ProjectToType<DictItemDto>().ToPagedList(model.PageIndex, model.PageSize);
        }
        /// <summary>
        /// 根据编号查询详情
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<DictItemDto> GetById(long id)
        {
            var entity = await _dictItemRepository.FindOrDefaultAsync(id);

            var dto = entity.Adapt<DictItemDto>();

            return await Task.FromResult(dto);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Insert(DictItemDto model)
        {

            var entity = model.Adapt<SysDictItem>();
            entity.Id = YitIdHelper.NextId();
            entity.CreatedTime = DateTime.Now;
            entity.Creator = JwtHelp.GetUserId();
            entity.Status = DictStatus.Normal;
            _dictItemRepository.Insert(entity);
            return await _dictItemRepository.SaveNowAsync();
        }
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Insert(List<DictItemDto> model)
        {
            for (int i = 0; i < model.Count; i++)
            {
                //将ReserveStationDto转换成ReserveStation类
                var entity = model[i].Adapt<SysDictItem>();
                entity.Id = YitIdHelper.NextId();
                entity.CreatedTime = DateTime.UtcNow;
                entity.Creator = 0;
                await _dictItemRepository.InsertAsync(entity, ignoreNullValues: true);
            }
            //提交
            return await _dictItemRepository.SaveNowAsync();
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(DictItemDto model)
        {
            var entity = model.Adapt<SysDictItem>();
            entity.UpdatedTime = DateTime.Now;
            entity.Updater = JwtHelp.GetUserId();
            _dictItemRepository.Update(entity);
            return await _dictItemRepository.SaveNowAsync();
        }
        /// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(List<DictItemDto> model)
        {
            for (int i = 0; i < model.Count; i++)
            {
                //将ReserveStationDto转换成ReserveStation类
                var entity = model[i].Adapt<SysDictItem>();
                entity.UpdatedTime = DateTime.UtcNow;
                entity.Updater = 0;
                await _dictItemRepository.UpdateAsync(entity, ignoreNullValues: true);
            }
            //提交
            return await _dictItemRepository.SaveNowAsync();
        }
        /// <summary>
        /// 假删除
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<int> FakeDelete(long id)
        {
            var entity = _dictItemRepository.FindOrDefault(id);
            if (entity != null)
            {
                entity.IsDeleted = true;

                return await _dictItemRepository.SaveNowAsync();
            }
            return 0;
        }
        /// <summary>
        /// 批量假删除
        /// </summary>
        /// <param name="idList">编号</param>
        /// <returns></returns>
        public async Task<int> FakeDelete(List<long> idList)
        {
            var result = await _dictItemRepository.Context.BatchUpdate<SysDictItem>()
                .Set(x => x.IsDeleted, x => true)
                .Where(x => idList.Contains(x.Id))
                .ExecuteAsync();
            return result;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<int> Delete(long id)
        {
            _dictItemRepository.Delete(id);
            return await _dictItemRepository.SaveNowAsync();
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="idList">编号</param>
        /// <returns></returns>
        public async Task<int> Delete(List<long> idList)
        {
            foreach (var id in idList)
            {
                _dictItemRepository.Delete(id);
            }
            return await _dictItemRepository.SaveNowAsync();
        }
    }
}
