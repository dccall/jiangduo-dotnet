using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Furion.FriendlyException;
using JiangDuo.Application.AppService.QrcodeService.Dto;
using JiangDuo.Core.Models;
using JiangDuo.Core.Utils;
using Mapster;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yitter.IdGenerator;

namespace JiangDuo.Application.AppService.QrcodeService.Services
{
    public class QrcodeService : IQrcodeService, ITransient
    {
        private readonly ILogger<QrcodeService> _logger;
        private readonly IRepository<Qrcode> _qrcodeRepository;
        private readonly IRepository<SysUploadFile> _uploadFileRepository;

        public QrcodeService(ILogger<QrcodeService> logger,
            IRepository<SysUploadFile> uploadFileRepository,
            IRepository<Qrcode> qrcodeRepository)
        {
            _logger = logger;
            _qrcodeRepository = qrcodeRepository;
            _uploadFileRepository = uploadFileRepository;
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="model">数据</param>
        /// <returns></returns>
        public PagedList<DtoQrcode> GetList(DtoQrcodeQuery model)
        {
            var query = _qrcodeRepository.AsQueryable(false).Where(x => !x.IsDeleted);
            query = query.Where(!string.IsNullOrEmpty(model.Name), x => x.Name.Contains(model.Name));

            //将数据映射到DtoQrcode中
            return query.OrderByDescending(s => s.CreatedTime).ProjectToType<DtoQrcode>().ToPagedList(model.PageIndex, model.PageSize);
        }

        /// <summary>
        /// 根据id查询详情
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<DtoQrcode> GetById(long id)
        {
            var entity = await _qrcodeRepository.FindOrDefaultAsync(id);

            var dto = entity.Adapt<DtoQrcode>();

            if (dto != null && !string.IsNullOrEmpty(dto.Attachments))
            {
                var idList = dto.Attachments.Split(',').ToList();
                dto.AttachmentsFiles = _uploadFileRepository.Where(x => idList.Contains(x.FileId.ToString())).ToList();
            }

            return dto;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Insert(DtoQrcodeForm model)
        {
            var entity = model.Adapt<Qrcode>();
            entity.Id = YitIdHelper.NextId();
            entity.CreatedTime = DateTime.Now;
            entity.Creator = JwtHelper.GetAccountId();
            if (model.AttachmentsFiles != null && model.AttachmentsFiles.Any())
            {
                entity.Attachments = String.Join(",", model.AttachmentsFiles.Select(x => x.FileId));
            }
            _qrcodeRepository.Insert(entity);
            return await _qrcodeRepository.SaveNowAsync();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(DtoQrcodeForm model)
        {
            //先根据id查询实体
            var entity = _qrcodeRepository.FindOrDefault(model.Id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            //将模型数据映射给实体属性
            entity = model.Adapt(entity);
            entity.UpdatedTime = DateTime.Now;
            entity.Updater = JwtHelper.GetAccountId();
            if (model.AttachmentsFiles != null && model.AttachmentsFiles.Any())
            {
                entity.Attachments = String.Join(",", model.AttachmentsFiles.Select(x => x.FileId));
            }
            _qrcodeRepository.Update(entity);
            return await _qrcodeRepository.SaveNowAsync();
        }

        /// <summary>
        /// 假删除
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<int> FakeDelete(long id)
        {
            var entity = _qrcodeRepository.FindOrDefault(id);
            if (entity == null)
            {
                throw Oops.Oh("数据不存在");
            }
            entity.IsDeleted = true;
            return await _qrcodeRepository.SaveNowAsync();
        }

        /// <summary>
        /// 批量假删除
        /// </summary>
        /// <param name="idList">编号</param>
        /// <returns></returns>
        public async Task<int> FakeDelete(List<long> idList)
        {
            var result = await _qrcodeRepository.Context.BatchUpdate<Qrcode>()
                .Where(x => idList.Contains(x.Id))
                .Set(x => x.IsDeleted, x => true)
                .ExecuteAsync();
            return result;
        }
    }
}