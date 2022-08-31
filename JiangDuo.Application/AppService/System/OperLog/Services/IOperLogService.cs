using JiangDuo.Application.OperLog.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JiangDuo.Application.OperLog.Services
{
    public interface IOperLogService
    {
        public Task<PagedList<OperLogDto>> GetList(OperLogRequest model);

        Task<OperLogDto> GetById(long id);

        Task<int> Insert(OperLogDto model);

        Task<int> Delete(long id);

        Task<int> Delete(List<long> idList);
    }
}