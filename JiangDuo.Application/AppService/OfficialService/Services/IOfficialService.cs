using JiangDuo.Application.AppService.OfficialService.Dto;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.OfficialService.Services
{
    public interface IOfficialService
    {
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public PagedList<DtoOfficial> GetList(DtoOfficialQuery model);

        /// <summary>
        /// 根据id查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<DtoOfficial> GetById(long id);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Insert(DtoOfficialForm model);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Update(DtoOfficialForm model);

        /// <summary>
        /// 假删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<int> FakeDelete(long id);

        //批量假删除
        public Task<int> FakeDelete(List<long> idList);

        /// <summary>
        /// 导出excel
        /// </summary>
        /// <returns></returns>
        public MemoryStream ExportExcel(DtoOfficialQuery model);

        /// <summary>
        /// 导入excel
        /// </summary>
        /// <returns></returns>
        public  Task<bool> ImportExcel(IFormFile file);
    }
}