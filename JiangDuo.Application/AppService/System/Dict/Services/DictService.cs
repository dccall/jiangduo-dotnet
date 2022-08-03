using JiangDuo.Application.System.Dict.Dto;
using JiangDuo.Application.Tools;
using JiangDuo.Core.Enums;
using JiangDuo.Core.Models;
using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yitter.IdGenerator;
using JiangDuo.Core.Utils;

namespace JiangDuo.Application.System.Dict.Services
{
    public class DictService : IDictService, ITransient
    {
        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILogger<DictService> _logger;
        /// <summary>
        /// SysDict仓储
        /// </summary>
        private readonly IRepository<SysDict> _dictRepository;

        /// <summary>
        /// SysDictItem仓储
        /// </summary>
        private readonly IRepository<SysDictItem> _dictItemRepository;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志</param>
        /// <param name="dictRepository">字典仓储</param>
        /// <param name="dictItemRepository">字典项</param>
        public DictService(ILogger<DictService> logger, IRepository<SysDict> dictRepository, IRepository<SysDictItem> dictItemRepository)
        {
            _logger = logger;
            _dictRepository = dictRepository;
            _dictItemRepository = dictItemRepository;
        }
        /// <summary>
        /// 分页,一对多分页
        /// </summary>
        /// <param name="model">数据</param>
        /// <returns></returns>
        public PagedList<DictDto> GetList(DictRequest model)
        {
            var query = _dictRepository.Where(x => !x.IsDeleted&&x.Status== DictStatus.Normal);
            query = query.Where(!string.IsNullOrEmpty(model.DictName), x => x.DictName.Contains(model.DictName));
            //将数据映射到DictDto中
            return query.Include(u => u.SysDictItem).ProjectToType<DictDto>().ToPagedList(model.PageIndex, model.PageSize);
        }
        /// <summary>
        /// 根据编号查询详情
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<DictDto> GetById(long id)
        {
            var entity = await _dictRepository.Where(x=>x.Id== id).Include(u => u.SysDictItem).FirstOrDefaultAsync();
           
            var dto = entity.Adapt<DictDto>();

            return dto;
        }
        /// <summary>
        /// 根据名称查询详情
        /// </summary>
        /// <param name="dictName">dictName</param>
        /// <returns></returns>
        public async Task<DictDto> GetByDictName(string dictName)
        {
            var entity = await _dictRepository.Where(x=>!x.IsDeleted && x.Status == DictStatus.Normal && x.DictName== dictName).Include(u => u.SysDictItem.Where(x=>!x.IsDeleted && x.Status == DictStatus.Normal)).FirstOrDefaultAsync();

            var dto = entity.Adapt<DictDto>();

            return dto;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Insert(DtoDictForm model)
        {

            var entity = model.Adapt<SysDict>();
            entity.Id = YitIdHelper.NextId();
            entity.CreatedTime = DateTime.Now;
            entity.Creator = JwtHelper.GetAccountId();
            entity.Status = DictStatus.Normal;
            _dictRepository.Insert(entity);
            return await _dictRepository.SaveNowAsync();
        }
      
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(DtoDictForm model)
        {
            var entity = model.Adapt<SysDict>();
            entity.UpdatedTime = DateTime.Now;
            entity.Updater = JwtHelper.GetAccountId();
            _dictRepository.Update(entity);
            return await _dictRepository.SaveNowAsync();
        }
      
        /// <summary>
        /// 假删除
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<int> FakeDelete(long id)
        {
            var entity = _dictRepository.FindOrDefault(id);
            if (entity != null)
            {
                entity.IsDeleted = true;

                return await _dictRepository.SaveNowAsync();
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
            var result = await _dictRepository.Context.BatchUpdate<SysDict>()
                .Where(x => idList.Contains(x.Id))
                .Set(x => x.IsDeleted, x => true)
                .ExecuteAsync();
            return result;
        }
       
    }
}
