using JiangDuo.Application.System.Config.Dto;
using JiangDuo.Application.System.Config.Services;
using JiangDuo.Application.System.Post.Dto;
using JiangDuo.Application.System.Post.Services;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.System.Post
{
    /// <summary>
    /// 职位管理
    /// </summary>
    [Route("api/[controller]")]
    public class PostAppService : IDynamicApiController
    {
        /// <summary>
        /// 配置
        /// </summary>
        private readonly IPostService _postService;
        public PostAppService(IPostService postService)
        {
            _postService = postService;
        }
        /// <summary>
        /// 获取配置列表（分页）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public PagedList<PostDto> Get([FromQuery] PostRequest model)
        {
            return _postService.GetList(model);
        }

        /// <summary>
        /// 根据id获取详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<PostDto> Get(long id)
        {
            return await _postService.GetById(id);
        }
        /// <summary>
        /// 配置新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Insert(DtoPostForm model)
        {
            return await _postService.Insert(model);
        }
        /// <summary>
        /// 配置修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(DtoPostForm model)
        {
            return await _postService.Update(model);
        }
        /// <summary>
        /// 根据id删除配置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> Delete(long id)
        {
            return await _postService.FakeDelete(id);
        }
        /// <summary>
        /// 批量删除配置
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        [HttpPost("Delete")]
        public async Task<int> Delete([FromBody] List<long> idList)
        {
            return await _postService.FakeDelete(idList);
        }
    }
}
