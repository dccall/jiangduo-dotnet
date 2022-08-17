using JiangDuo.Application.System.Config.Dto;
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
using JiangDuo.Application.AppService.BuildingService.Dto;
using JiangDuo.Application.AppService.NewsService.Dto;
using Furion.FriendlyException;

namespace JiangDuo.Application.AppService.NewsService.Services
{
    public class NewsService : INewsService, ITransient
    {
        private readonly ILogger<NewsService> _logger;
        private readonly IRepository<News> _newsRepository;
        private readonly IRepository<SysUploadFile> _uploadFileRepository;
        private readonly IRepository<Newsclassify> _newsclassifyRepository;
        public NewsService(ILogger<NewsService> logger,
             IRepository<SysUploadFile> uploadFileRepository,
             IRepository<Newsclassify> newsclassifyRepository,
            IRepository<News> newsRepository)
        {
            _logger = logger;
            _newsRepository = newsRepository;
            _uploadFileRepository = uploadFileRepository;
            _newsclassifyRepository = newsclassifyRepository;
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="model">数据</param>
        /// <returns></returns>
        public PagedList<DtoNews> GetList(DtoNewsQuery model)
        {
            var query = _newsRepository.Where(x => !x.IsDeleted);
            query = query.Where(!string.IsNullOrEmpty(model.Title), x => x.Title.Contains(model.Title));
            query = query.Where(model.IsRecommend != null, x => x.IsRecommend == model.IsRecommend);
            query = query.Where(model.Status != null, x => x.Status == model.Status);
            query = query.Where(model.NewsClassifyId != null, x => x.NewsClassifyId == model.NewsClassifyId);

            var query2 = from x in query
                         join c in _newsclassifyRepository.Entities on x.NewsClassifyId equals c.Id into result1
                         from xc in result1.DefaultIfEmpty()
                         select new DtoNews
                         {
                             Id = x.Id,
                             Author = x.Author,
                             Content = x.Content,
                             CoverPhoto = x.CoverPhoto,
                             NewsClassifyId = x.NewsClassifyId,
                             NewsClassifyName = xc.ClassifyName,
                             IsRecommend = x.IsRecommend,
                             Subtitle = x.Subtitle,
                             Title = x.Title,
                             CreatedTime = x.CreatedTime,
                             Creator = x.Creator,
                             UpdatedTime = x.UpdatedTime,
                             Updater = x.Updater,
                             IsDeleted = x.IsDeleted,
                             Status = x.Status,
                             RelationId = x.RelationId,
                         };
            var pageList = query2.OrderByDescending(s => s.CreatedTime).ToPagedList(model.PageIndex, model.PageSize);
            return pageList;

        }
        /// <summary>
        /// 根据编号查询详情
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<DtoNews> GetById(long id)
        {
            var query = _newsRepository.Where(x => !x.IsDeleted && x.Id == id);

            var query2 = from x in query
                         join c in _newsclassifyRepository.Entities on x.NewsClassifyId equals c.Id into result1
                         from nc in result1.DefaultIfEmpty()
                         select new DtoNews
                         {
                             Id = x.Id,
                             Author = x.Author,
                             Content = x.Content,
                             CoverPhoto = x.CoverPhoto,
                             NewsClassifyId = x.NewsClassifyId,
                             IsRecommend = x.IsRecommend,
                             Subtitle = x.Subtitle,
                             Title = x.Title,
                             CreatedTime = x.CreatedTime,
                             Creator = x.Creator,
                             UpdatedTime = x.UpdatedTime,
                             Updater = x.Updater,
                             IsDeleted = x.IsDeleted,
                             Status = x.Status,
                             RelationId = x.RelationId,
                         };

            var dto = query2.FirstOrDefault();
            if (dto != null && !string.IsNullOrEmpty(dto.CoverPhoto))
            {
                var idList = dto.CoverPhoto.Split(',').ToList();
                dto.CoverPhotoFiles = _uploadFileRepository.Where(x => idList.Contains(x.FileId.ToString())).ToList();
            }
            return dto;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Insert(DtoNewsForm model)
        {
            var account = JwtHelper.GetAccountInfo();
            var entity = model.Adapt<News>();
            entity.Id = YitIdHelper.NextId();
            entity.Author = account.Name;
            entity.CreatedTime = DateTime.Now;
            entity.Creator = JwtHelper.GetAccountId();
            if (model.CoverPhotoFiles != null && model.CoverPhotoFiles.Any())
            {
                entity.CoverPhoto = String.Join(",", model.CoverPhotoFiles.Select(x => x.FileId));
            }
            _newsRepository.Insert(entity);
            return await _newsRepository.SaveNowAsync();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(DtoNewsForm model)
        {
            //先根据id查询实体
            var entity = _newsRepository.FindOrDefault(model.Id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            //将模型数据映射给实体属性
            entity = model.Adapt(entity);
            entity.UpdatedTime = DateTime.Now;
            entity.Updater = JwtHelper.GetAccountId();
            if (model.CoverPhotoFiles != null && model.CoverPhotoFiles.Any())
            {
                entity.CoverPhoto = String.Join(",", model.CoverPhotoFiles.Select(x => x.FileId));
            }
            _newsRepository.Update(entity);
            return await _newsRepository.SaveNowAsync();
        }
        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> UpdateStatus(DtoNewsUpdateStatus model)
        {
            //先根据id查询实体
            var entity = _newsRepository.FindOrDefault(model.Id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            entity.Status = model.Status;
            _newsRepository.UpdateInclude(entity, new string[] { nameof(entity.Status) });

            return await _newsRepository.SaveNowAsync();
        }

        /// <summary>
        /// 假删除
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<int> FakeDelete(long id)
        {
            var entity = _newsRepository.FindOrDefault(id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            entity.IsDeleted = true;
            return await _newsRepository.SaveNowAsync();
        }
        /// <summary>
        /// 批量假删除
        /// </summary>
        /// <param name="idList">编号</param>
        /// <returns></returns>
        public async Task<int> FakeDelete(List<long> idList)
        {
            var result = await _newsRepository.Context.BatchUpdate<News>()
                .Where(x => idList.Contains(x.Id))
                .Set(x => x.IsDeleted, x => true)
                .ExecuteAsync();
            return result;
        }


    }
}
