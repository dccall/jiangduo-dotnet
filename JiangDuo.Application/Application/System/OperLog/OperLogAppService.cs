
using JiangDuo.Application.OperLog.Dtos;
using JiangDuo.Application.OperLog.Services;
using Furion.DependencyInjection;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.OperLog
{
    /// <summary>
    /// 操作日志
    /// </summary>
    public class OperLogAppService : IDynamicApiController
    {
        private readonly IOperLogService _operLogService;
        public OperLogAppService(IOperLogService operLogService)
        {
            _operLogService = operLogService;
        }
        /// <summary>
        /// 查询列表（分页）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<PagedList<OperLogDto>> Get([FromQuery]OperLogRequest model)
        {
            return await _operLogService.GetList(model);
        }
        /// <summary>
        /// 根据id获取详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<OperLogDto> Get(long id)
        {
            return await _operLogService.GetById(id);
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Insert(OperLogDto model)
        {
            return await _operLogService.Insert(model);
        }
       
        /// <summary>
        /// 根据id删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> Delete(long id)
        {
            return await _operLogService.Delete(id);
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        public async Task<int> Delete([FromBody] List<long> idList)
        {
            return await _operLogService.Delete(idList);
        }
    }
}
