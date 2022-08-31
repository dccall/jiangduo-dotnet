using Furion.DynamicApiController;
using JiangDuo.Application.System.Notice.Dtos;
using JiangDuo.Application.System.Notice.Services;
using JiangDuo.Core.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JiangDuo.Application.System.Notice
{
    /// <summary>
    /// 通知管理
    /// </summary>
    [Route("api/[controller]")]
    public class NoticeAppService : IDynamicApiController
    {
        /// <summary>
        /// 公告
        /// </summary>
        private readonly INoticeService _noticeService;

        private readonly RedisCache _redisService;

        public NoticeAppService(INoticeService noticeService, RedisCache redisService)
        {
            _noticeService = noticeService;
            _redisService = redisService;
        }

        /// <summary>
        /// 获取公告列表（分页）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public PagedList<NoticeDto> Get([FromQuery] NoticeRequest model)
        {
            return _noticeService.GetList(model);
        }

        /// <summary>
        /// 根据id获取详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<NoticeDto> Get(long id)
        {
            return await _noticeService.GetById(id);
        }

        /// <summary>
        /// 公告新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Insert(DtoNoticeForm model)
        {
            return await _noticeService.Insert(model);
        }

        /// <summary>
        /// 公告修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(DtoNoticeForm model)
        {
            return await _noticeService.Update(model);
        }

        /// <summary>
        /// 根据id删除公告
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> Delete(long id)
        {
            return await _noticeService.FakeDelete(id);
        }

        /// <summary>
        /// 批量删除公告
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        [HttpPost("Delete")]
        public async Task<int> Delete([FromBody] List<long> idList)
        {
            return await _noticeService.FakeDelete(idList);
        }
    }
}