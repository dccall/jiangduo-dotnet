
using JiangDuo.Application.OperLog.Dtos;
using JiangDuo.Application.Tools;
using JiangDuo.Core;
using JiangDuo.Core.Enums;
using JiangDuo.Core.Models;
using Furion;
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

namespace JiangDuo.Application.OperLog.Services
{
    public class OperLogService : IOperLogService, ITransient
    {
        private readonly ILogger<OperLogService> _logger;
        private readonly IRepository<SysOperLog> _operLogRepository;

        public OperLogService(ILogger<OperLogService> logger,
            IRepository<SysOperLog> operLogRepository)
        {
            _logger = logger;
            _operLogRepository = operLogRepository;
        }
    
      
        public async Task<PagedList<OperLogDto>> GetList(OperLogRequest model)
        {
            var query = _operLogRepository.AsQueryable();
            var list = await query.OrderByDescending(x => x.OperTime).ProjectToType<OperLogDto>().ToPagedListAsync(model.PageIndex, model.PageSize);
            return list;

        }
        public async Task<OperLogDto> GetById(long id)
        {
            var entity = await _operLogRepository.FindOrDefaultAsync(id);

            var dto = entity.Adapt<OperLogDto>();

            return await Task.FromResult(dto);
        }
        public async Task<int> Insert(OperLogDto model)
        {
            var user = JwtHelper.GetUserInfo();
            var entity = model.Adapt<SysOperLog>();
            var app= App.WebHostEnvironment;
            //entity.Id = YitIdHelper.NextId();
            entity.OperName = user == null ? "" : user.NickName;
            entity.OperIp = "";
            entity.OperTime = DateTime.UtcNow;
            _operLogRepository.Insert(entity);
            return await _operLogRepository.SaveNowAsync();
        }
        public async Task<int> Delete(long id)
        {
            _operLogRepository.Delete(id);
            return await _operLogRepository.SaveNowAsync();
        }
        public async Task<int> Delete(List<long> idList)
        {
            foreach (var id in idList)
            {
                _operLogRepository.Delete(id);
            }
            return await _operLogRepository.SaveNowAsync();
        }

       
    }
}
