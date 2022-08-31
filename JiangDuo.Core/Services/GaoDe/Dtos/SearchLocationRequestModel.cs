namespace JiangDuo.Core.Services.GaoDe.Dtos;

public class SearchLocationRequestModel
{
    /// <summary>
    /// 关键词
    /// </summary>
    public string Keywords { get; set; }

    /// <summary>
    /// 坐标
    /// </summary>
    public string Location { get; set; }

    /// <summary>
    /// 城市编号
    /// </summary>
    public string CityCode { get; set; } = "010 / 110000";
}