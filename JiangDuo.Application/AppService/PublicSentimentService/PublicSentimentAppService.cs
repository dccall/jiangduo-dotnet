using Furion.DynamicApiController;
using JiangDuo.Application.AppService.PublicSentimentService.Dto;
using JiangDuo.Application.AppService.PublicSentimentService.Dtos;
using JiangDuo.Application.AppService.PublicSentimentService.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.PublicSentimentService;

/// <summary>
/// 民情民意
/// </summary>
[Route("api/[controller]")]
[ApiDescriptionSettings("Default", "民情民意")]
public class PublicSentimentAppService : IDynamicApiController
{
    private readonly IPublicSentimentService _publicSentimentService;

    public PublicSentimentAppService(IPublicSentimentService publicSentimentService)
    {
        _publicSentimentService = publicSentimentService;
    }

    /// <summary>
    /// 获取列表（分页）
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public PagedList<DtoPublicSentiment> Get([FromQuery] DtoPublicSentimentQuery model)
    {
        return _publicSentimentService.GetList(model);
    }

    /// <summary>
    /// 根据id获取详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<DtoPublicSentiment> Get(long id)
    {
        return await _publicSentimentService.GetById(id);
    }

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<int> Insert(DtoPublicSentimentForm model)
    {
        return await _publicSentimentService.Insert(model);
    }

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<int> Update(DtoPublicSentimentForm model)
    {
        return await _publicSentimentService.Update(model);
    }

    /// <summary>
    /// 完结反馈
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("Feedback")]
    public async Task<int> Feedback(DtoPublicSentimentFedBack model)
    {
        return await _publicSentimentService.Feedback(model);
    }

    /// <summary>
    /// 根据id删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<int> Delete(long id)
    {
        return await _publicSentimentService.FakeDelete(id);
    }

    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="idList"></param>
    /// <returns></returns>
    [HttpPost("Delete")]
    public async Task<int> Delete([FromBody] List<long> idList)
    {
        return await _publicSentimentService.FakeDelete(idList);
    }
}