using JiangDuo.Application.System.Notice.Dtos;
using JiangDuo.Application.Tools;
using JiangDuo.Core.Enums;
using JiangDuo.Core.Models;
using JiangDuo.Core.Utils;
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
using Furion.FriendlyException;

namespace JiangDuo.Application.System.Notice.Services
{
    public class NoticeService : INoticeService, ITransient
    {

        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILogger<NoticeService> _logger;
        /// <summary>
        /// SysNotice仓储
        /// </summary>
        private readonly IRepository<SysNotice> _noticeRepository;
        private readonly RedisCache _redisService;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志</param>
        /// <param name="noticeRepository">公告仓储</param>
        /// <param name="redisService">缓存</param>
        public NoticeService(ILogger<NoticeService> logger, IRepository<SysNotice> noticeRepository, RedisCache redisService)
        {
            _logger = logger;
            _noticeRepository = noticeRepository;
            _redisService = redisService;
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="model">数据</param>
        /// <returns></returns>
        public PagedList<NoticeDto> GetList(NoticeRequest model)
        {
            return _noticeRepository.Where(x => !x.IsDeleted).ProjectToType<NoticeDto>().ToPagedList(model.PageIndex, model.PageSize);
        }
        /// <summary>
        /// 根据编号查询详情
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<NoticeDto> GetById(long id)
        {
            var entity = await _noticeRepository.FindOrDefaultAsync(id);

            var dto = entity.Adapt<NoticeDto>();
            return dto;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Insert(DtoNoticeForm model)
        {

            var entity = model.Adapt<SysNotice>();
            entity.Id = YitIdHelper.NextId();
            entity.CreatedTime = DateTimeOffset.UtcNow;
            entity.Creator = JwtHelper.GetAccountId();
            entity.Status = NoticeStatus.Normal;
            _noticeRepository.Insert(entity);
            return await _noticeRepository.SaveNowAsync();
        }
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Insert(List<DtoNoticeForm> model)
        {
            for (int i = 0; i < model.Count; i++)
            {
                //将ReserveStationDto转换成ReserveStation类
                var entity = model[i].Adapt<SysNotice>();
                entity.Id = YitIdHelper.NextId();
                entity.CreatedTime = DateTime.UtcNow;
                entity.Creator = 0;
                await _noticeRepository.InsertAsync(entity, ignoreNullValues: true);
            }
            //提交
            return await _noticeRepository.SaveNowAsync();
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(DtoNoticeForm model)
        {
            //先根据id查询实体
            var entity = _noticeRepository.FindOrDefault(model.Id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            //将模型数据映射给实体属性
            entity = model.Adapt(entity);
            entity.UpdatedTime = DateTimeOffset.UtcNow;
            entity.Updater = JwtHelper.GetAccountId();
            _noticeRepository.Update(entity);
            return await _noticeRepository.SaveNowAsync();
        }
        /// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(List<DtoNoticeForm> model)
        {
            foreach (var id in model)
            {
                //先根据id查询实体
                var entity = _noticeRepository.FindOrDefault(id);
                if (entity == null)
                {
                    throw Oops.Oh("数据不存在");
                }
                //将模型数据映射给实体属性
                entity = model.Adapt(entity);
                entity.UpdatedTime = DateTimeOffset.UtcNow;
                entity.Updater = JwtHelper.GetAccountId();
                await _noticeRepository.UpdateAsync(entity, ignoreNullValues: true);
            }
            //提交
            return await _noticeRepository.SaveNowAsync();
        }
        /// <summary>
        /// 假删除
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<int> FakeDelete(long id)
        {
            var entity = _noticeRepository.FindOrDefault(id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            entity.IsDeleted = true;
            return await _noticeRepository.SaveNowAsync();
        }
        /// <summary>
        /// 批量假删除
        /// </summary>
        /// <param name="idList">编号</param>
        /// <returns></returns>
        public async Task<int> FakeDelete(List<long> idList)
        {
            var result = await _noticeRepository.Context.BatchUpdate<SysNotice>()
                .Where(x => idList.Contains(x.Id))
                .Set(x => x.IsDeleted, x => true)
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
            _noticeRepository.Delete(id);
            return await _noticeRepository.SaveNowAsync();
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
                _noticeRepository.Delete(id);
            }
            return await _noticeRepository.SaveNowAsync();
        }
    }
}
