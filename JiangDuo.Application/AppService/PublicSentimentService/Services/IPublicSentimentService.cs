using JiangDuo.Application.AppService.PublicSentimentService.Dto;
using JiangDuo.Application.AppService.PublicSentimentService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.PublicSentimentService.Services
{
    public interface IPublicSentimentService
    {
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public PagedList<DtoPublicSentiment> GetList(DtoPublicSentimentQuery model);
        /// <summary>
        /// 根据id查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<DtoPublicSentiment> GetById(long id);
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Insert(DtoPublicSentimentForm model);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Update(DtoPublicSentimentForm model);

        /// <summary>
        /// 完结反馈
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<int> Feedback(DtoPublicSentimentFedBack model);
        /// <summary>
        /// 假删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<int> FakeDelete(long id);
        //批量假删除
        public Task<int> FakeDelete(List<long> idList);
     
    }
}
