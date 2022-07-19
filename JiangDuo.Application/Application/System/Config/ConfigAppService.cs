using JiangDuo.Application.System.Config.Dto;
using JiangDuo.Application.System.Config.Services;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.System.Config
{
    public class ConfigAppService: IDynamicApiController
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
        /// 配置新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Insert(ConfigDto model)
        {
            return await _configService.Insert(model);
        }
        /// <summary>
        /// 配置修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(ConfigDto model)
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
        public async Task<int> Delete([FromBody] List<long> idList)
        {
            return await _configService.FakeDelete(idList);
        }
    }
}
