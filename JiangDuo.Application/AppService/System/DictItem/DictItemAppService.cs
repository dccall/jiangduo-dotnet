﻿using Furion.DynamicApiController;
using JiangDuo.Application.System.DictItem.Dto;
using JiangDuo.Application.System.DictItem.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JiangDuo.Application.System.DictItem
{
    /// <summary>
    /// 字典项管理
    /// </summary>
    [Route("api/[controller]")]
    public class DictitemAppService : IDynamicApiController
    {
        /// <summary>
        /// 字典项
        /// </summary>
        private readonly IDictItemService _dictItemService;

        public DictitemAppService(IDictItemService dictItemService)
        {
            _dictItemService = dictItemService;
        }

        /// <summary>
        /// 获取字典项列表（分页）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public PagedList<DictItemDto> Get([FromQuery] DictItemRequest model)
        {
            return _dictItemService.GetList(model);
        }

        /// <summary>
        /// 根据id获取详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<DictItemDto> Get(long id)
        {
            return await _dictItemService.GetById(id);
        }

        /// <summary>
        /// 字典项新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Insert(DtoDictItemForm model)
        {
            return await _dictItemService.Insert(model);
        }

        /// <summary>
        /// 字典项修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(DtoDictItemForm model)
        {
            return await _dictItemService.Update(model);
        }

        /// <summary>
        /// 根据id删除字典项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> Delete(long id)
        {
            return await _dictItemService.FakeDelete(id);
        }

        /// <summary>
        /// 批量删除字典项
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        [HttpPost("Delete")]
        public async Task<int> Delete([FromBody] List<long> idList)
        {
            return await _dictItemService.FakeDelete(idList);
        }
    }
}