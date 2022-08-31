using Furion.DynamicApiController;
using JiangDuo.Application.System.Config.Dto;
using JiangDuo.Application.System.Config.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JiangDuo.Application.System.Config
{
    /// <summary>
    /// 系统配置
    /// </summary>
    [Route("api/[controller]")]
    public class ConfigAppService : IDynamicApiController
    {
        /// <summary>
        /// 配置
        /// </summary>
        private readonly IConfigService _configService;

        public ConfigAppService(IConfigService configService)
        {
            _configService = configService;
        }

        /// <summary>
        /// 获取配置列表（分页）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public PagedList<ConfigDto> Get([FromQuery] ConfigRequest model)
        {
            return _configService.GetList(model);
        }

        /// <summary>
        /// 根据id获取详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ConfigDto> Get(long id)
        {
            return await _configService.GetById(id);
        }

        /// <summary>
        /// 根据key查询详情
        /// </summary>
        /// <param name="key">key</param>
        /// <returns></returns>
        public async Task<ConfigDto> GetByKey(string key)
        {
            return await _configService.GetByKey(key);
        }

        /// <summary>
        /// 配置新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Insert(DtoConfigForm model)
        {
            return await _configService.Insert(model);
        }

        /// <summary>
        /// 配置修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(DtoConfigForm model)
        {
            return await _configService.Update(model);
        }

        /// <summary>
        /// 根据id删除配置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> Delete(long id)
        {
            return await _configService.FakeDelete(id);
        }

        /// <summary>
        /// 批量删除配置
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        [HttpPost("Delete")]
        public async Task<int> Delete([FromBody] List<long> idList)
        {
            return await _configService.FakeDelete(idList);
        }
    }
}