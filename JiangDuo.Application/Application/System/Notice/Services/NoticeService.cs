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
            var list = await Task.FromResult(dto);
            DateTime date = DateTime.Now.AddMinutes(1);

            //设置缓存
            await _redisService.SetAsync<NoticeDto>("name", list, date);
            //await _redisService.SetAsync<string>("name", "sdss", date);
            return list;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Insert(NoticeDto model)
        {

            var entity = model.Adapt<SysNotice>();
            entity.Id = YitIdHelper.NextId();
            entity.CreatedTime = DateTime.Now;
            entity.Creator = JwtHelp.GetUserId();
            entity.Status = NoticeStatus.Normal;
            _noticeRepository.Insert(entity);
            return await _noticeRepository.SaveNowAsync();
        }
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Insert(List<NoticeDto> model)
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
        public async Task<int> Update(NoticeDto model)
        {
            var entity = model.Adapt<SysNotice>();
            entity.UpdatedTime = DateTime.Now;
            entity.Updater = JwtHelp.GetUserId();
            _noticeRepository.Update(entity);
            return await _noticeRepository.SaveNowAsync();
        }
        /// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(List<NoticeDto> model)
        {
            for (int i = 0; i < model.Count; i++)
            {
                //将ReserveStationDto转换成ReserveStation类
                var entity = model[i].Adapt<SysNotice>();
                entity.UpdatedTime = DateTime.UtcNow;
                entity.Updater = 0;
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
            if (entity != null)
            {
                entity.IsDeleted = true;

                return await _noticeRepository.SaveNowAsync();
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
            var result = await _noticeRepository.Context.BatchUpdate<SysNotice>()
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
