﻿using JiangDuo.Application.System.Config.Dto;
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
using JiangDuo.Application.AppService.PublicSentimentService.Dto;
using Furion.FriendlyException;
using JiangDuo.Application.AppService.PublicSentimentService.Dtos;
using JiangDuo.Core.Enums;
using Microsoft.EntityFrameworkCore.Internal;

namespace JiangDuo.Application.AppService.PublicSentimentService.Services
{
    public class PublicSentimentService : IPublicSentimentService, ITransient
    {
        private readonly ILogger<PublicSentimentService> _logger;
        private readonly IRepository<PublicSentiment> _publicSentimentRepository;
        private readonly IRepository<Resident> _residentRepository;
        private readonly IRepository<SelectArea> _selectAreaRepository;
        private readonly IRepository<SysUploadFile> _uploadFileRepository;
        private readonly IRepository<Business> _businessRepository;


        public PublicSentimentService(ILogger<PublicSentimentService> logger,
            IRepository<PublicSentiment> publicSentimentRepository,
            IRepository<Resident> residentRepository,
            IRepository<SelectArea> selectAreaRepository,
            IRepository<SysUploadFile> uploadFileRepository,
            IRepository<Business> businessRepository

            )
        {
            _logger = logger;
            _publicSentimentRepository = publicSentimentRepository;
            _residentRepository = residentRepository;
            _selectAreaRepository = selectAreaRepository;
            _businessRepository = businessRepository;
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="model">数据</param>
        /// <returns></returns>
        public PagedList<DtoPublicSentiment> GetList(DtoPublicSentimentQuery model)
        {
            var query = _publicSentimentRepository.Where(x => !x.IsDeleted);
            query = query.Where(model.BusinessId != null, x => x.BusinessId == model.BusinessId);
            query = query.Where(model.SelectAreaId != null, x => x.SelectAreaId == model.SelectAreaId);
            query = query.Where(model.Status != null, x => x.Status == model.Status);

            return query.Join(_residentRepository.Entities, x => x.ResidentId, r => r.Id, (x, r) => new { PublicSentiment = x, Resident = r })
                .Join(_selectAreaRepository.Entities, x => x.PublicSentiment.SelectAreaId,y => y.Id,(x, y) => new DtoPublicSentiment()
                {
                    Id = x.PublicSentiment.Id,
                    ResidentId = x.PublicSentiment.Id,
                    ResidentName = x.Resident.Name,
                    Content = x.PublicSentiment.Content,
                    FeedbackContent = x.PublicSentiment.FeedbackContent,
                    FeedbackTime = x.PublicSentiment.FeedbackTime,
                    IsDeleted = x.PublicSentiment.IsDeleted,
                    Status = x.PublicSentiment.Status,
                    UpdatedTime = x.PublicSentiment.UpdatedTime,
                    Updater = x.PublicSentiment.Updater,
                    Creator = x.PublicSentiment.Creator,
                    BusinessId = x.PublicSentiment.BusinessId,
                    SelectAreaId = x.PublicSentiment.SelectAreaId,
                    SelectAreaName =y.SelectAreaName,
                    Attachments = x.PublicSentiment.Attachments,
                    CreatedTime = x.PublicSentiment.CreatedTime,
                    FeedbackPersonId = x.PublicSentiment.FeedbackPersonId,
                    FeedbackPerson= x.PublicSentiment.FeedbackPerson,
                }).OrderBy(s => s.CreatedTime).ToPagedList(model.PageIndex, model.PageSize);

        }
        /// <summary>
        /// 根据id查询详情
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public async Task<DtoPublicSentiment> GetById(long id)
        {
            var entity = await _publicSentimentRepository.FindOrDefaultAsync(id);

            var dto = entity.Adapt<DtoPublicSentiment>();

            if (!string.IsNullOrEmpty(dto.Attachments))
            {
                var idList = dto.Attachments.Split(',').ToList();
                dto.AttachmentsList = _uploadFileRepository.Where(x => idList.Contains(x.FileId.ToString())).ToList();
            }


            return dto;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Insert(DtoPublicSentimentForm model)
        {
            var account = JwtHelper.GetAccountInfo();
            var entity = model.Adapt<PublicSentiment>();
            entity.Id = YitIdHelper.NextId();
            entity.CreatedTime = DateTime.Now;
            entity.Creator = account.Id;
            entity.Status = PublicSentimentStatus.NotProcessed; //待处理
            //如果传了选区，用传的选区id，否则用账号带的选区
            entity.SelectAreaId = model.SelectAreaId.HasValue? model.SelectAreaId.Value: account.SelectAreaId;

            if (model.AttachmentsList!= null && model.AttachmentsList.Any())
            {
                entity.Attachments = String.Join(",", model.AttachmentsList.Select(x => x.FileId));
            }
            _publicSentimentRepository.Insert(entity);
            return await _publicSentimentRepository.SaveNowAsync();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(DtoPublicSentimentForm model)
        {
            //先根据id查询实体
            var entity = _publicSentimentRepository.FindOrDefault(model.Id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            //将模型数据映射给实体属性
            entity = model.Adapt(entity);
            entity.UpdatedTime = DateTime.Now;
            entity.Updater = JwtHelper.GetAccountId();

            if (model.AttachmentsList != null && model.AttachmentsList.Any())
            {
                entity.Attachments = String.Join(",", model.AttachmentsList.Select(x => x.FileId));
            }

            _publicSentimentRepository.Update(entity);
            return await _publicSentimentRepository.SaveNowAsync();
        }

        /// <summary>
        /// 完结反馈
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Feedback(DtoPublicSentimentFedBack model)
        {
            //先根据id查询实体
            var entity = _publicSentimentRepository.FindOrDefault(model.PublicSentimentId);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            entity.FeedbackContent = model.FeedbackContent;
            entity.FeedbackTime = DateTime.Now;
            entity.Status = PublicSentimentStatus.Feedback;
            _publicSentimentRepository.Update(entity);
            return await _publicSentimentRepository.SaveNowAsync();
        }

        /// <summary>
        /// 假删除
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<int> FakeDelete(long id)
        {
            var entity = _publicSentimentRepository.FindOrDefault(id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            entity.IsDeleted = true;
            return await _publicSentimentRepository.SaveNowAsync();
        }
        /// <summary>
        /// 批量假删除
        /// </summary>
        /// <param name="idList">编号</param>
        /// <returns></returns>
        public async Task<int> FakeDelete(List<long> idList)
        {
            var result = await _publicSentimentRepository.Context.BatchUpdate<PublicSentiment>()
                .Where(x => idList.Contains(x.Id))
                .Set(x => x.IsDeleted, x => true)
                .ExecuteAsync();
            return result;
        }



    }
}
