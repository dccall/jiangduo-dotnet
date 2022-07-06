using JiangDuo.Application.System.Config.Dto;
using JiangDuo.Application.System.Post.Dto;
using JiangDuo.Application.Tools;
using JiangDuo.Core.Models;
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
using JiangDuo.Core.Utils;

namespace JiangDuo.Application.System.Post.Services
{
    public class PostService:IPostService, ITransient
    {
        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILogger<PostService> _logger;
        /// <summary>
        /// SysPost仓储
        /// </summary>
        private readonly IRepository<SysPost> _postRepository;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志</param>
        /// <param name="postRepository">部门仓储</param>
        public PostService(ILogger<PostService> logger, IRepository<SysPost> postRepository)
        {
            _logger = logger;
            _postRepository = postRepository;
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="model">数据</param>
        /// <returns></returns>
        public PagedList<PostDto> GetList(PostRequest model)
        {
            var query = _postRepository.Where(x => !x.IsDeleted);
            query = query.Where(!string.IsNullOrEmpty(model.PostName), x => x.PostName.Contains(model.PostName));
            //将数据映射到ConfigDto中
            return query.OrderBy(s=>s.CreatedTime).ProjectToType<PostDto>().ToPagedList(model.PageIndex, model.PageSize);
        }
        /// <summary>
        /// 根据编号查询详情
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<PostDto> GetById(long id)
        {
            var entity = await _postRepository.FindOrDefaultAsync(id);

            var dto = entity.Adapt<PostDto>();

            return await Task.FromResult(dto);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Insert(PostDto model)
        {

            var entity = model.Adapt<SysPost>();
            entity.Id = YitIdHelper.NextId();
            entity.CreatedTime = DateTime.Now;
            entity.Creator = JwtHelp.GetUserId();
            _postRepository.Insert(entity);
            return await _postRepository.SaveNowAsync();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(PostDto model)
        {
            var entity = model.Adapt<SysPost>();
            entity.UpdatedTime = DateTime.Now;
            entity.Updater = JwtHelp.GetUserId();
            _postRepository.Update(entity);
            return await _postRepository.SaveNowAsync();
        }
   
        /// <summary>
        /// 假删除
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<int> FakeDelete(long id)
        {
            var entity = _postRepository.FindOrDefault(id);
            if (entity != null)
            {
                entity.IsDeleted = true;

                return await _postRepository.SaveNowAsync();
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
            var result = await _postRepository.Context.BatchUpdate<SysPost>()
                .Set(x => x.IsDeleted, x => true)
                .Where(x => idList.Contains(x.Id))
                .ExecuteAsync();
            return result;
        }
       

    }
}
