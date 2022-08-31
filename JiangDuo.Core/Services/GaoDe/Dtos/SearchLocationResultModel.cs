using Newtonsoft.Json;
using System.Collections.Generic;

namespace JiangDuo.Core.Services.GaoDe.Dtos;

public class SearchLocationResultModel
{
    /// <summary>
    ///
    /// </summary>
    [JsonProperty("tips")]
    public List<TipsModel> Tips { get; set; }

    public string status { get; set; }
    public string info { get; set; }
    public string infocode { get; set; }
    public string count { get; set; }
}

public class TipsModel
{
    /// <summary>
    /// 返回数据ID
    /// </summary>
    [JsonProperty("id")]
    public object Id { get; set; }

    /// <summary>
    /// tip名称
    /// </summary>
    [JsonProperty("name")]
    public object Name { get; set; }

    /// <summary>
    /// 所属区域
    /// </summary>
    [JsonProperty("district")]
    public object District { get; set; }

    /// <summary>
    /// 区域编码
    /// </summary>
    [JsonProperty("adcode")]
    public object Adcode { get; set; }

    /// <summary>
    /// tip中心点坐标
    /// </summary>
    [JsonProperty("location")]
    public object Location { get; set; }

    /// <summary>
    /// 详细地址
    /// </summary>
    [JsonProperty("address")]
    public object Address { get; set; }

    /// <summary>
    /// typecode
    /// </summary>
    [JsonProperty("typecode")]
    public object Typecode { get; set; }

    /// <summary>
    /// city 城市
    /// </summary>
    [JsonProperty("city")]
    public object City { get; set; }
}