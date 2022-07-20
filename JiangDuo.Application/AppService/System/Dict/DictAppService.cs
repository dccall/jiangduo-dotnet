using JiangDuo.Application.System.Dict.Dto;
using JiangDuo.Application.System.Dict.Services;
using JiangDuo.Core.Models;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.System.Dict
{
    /// <summary>
    /// 字典管理
    /// </summary>
    [Route("api/[controller]")]
    public class DictAppService: IDynamicApiController
    {
        /// <summary>
        /// 字典
        /// </summary>
        private readonly IDictService _dictService;
        public DictAppService(IDictService dictService)
        {
            _dictService = dictService;
        }
        /// <summary>
        /// 获取字典列表（分页）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public PagedList<DictDto> Get([FromQuery] DictRequest model)
        {
            return _dictService.GetList(model);
        }

        /// <summary>
        /// 根据id获取详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<DictDto> Get(long id)
        {
            return await _dictService.GetById(id);
        }
        /// <summary>
        /// 字典新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Insert(DtoDictForm model)
        {
            return await _dictService.Insert(model);
        }
        /// <summary>
        /// 字典修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(DtoDictForm model)
        {
            return await _dictService.Update(model);
        }
        /// <summary>
        /// 根据id删除字典
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> Delete(long id)
        {
            return await _dictService.FakeDelete(id);
        }
        /// <summary>
        /// 批量删除字典
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        [HttpPost("Delete")]
        public async Task<int> Delete([FromBody] List<long> idList)
        {
            return await _dictService.FakeDelete(idList);
        }
    }
}
