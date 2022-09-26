using Npoi.Mapper.Attributes;

namespace JiangDuo.Application.AppService.OfficialsstructService.Dto;

public class DtoOfficialsstructExport
{
    [Column("Id")]
    public string Id { get; set; }

    /// <summary>
    /// 人大结构名称
    /// </summary>
    [Column("人大结构名称")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// 描述
    /// </summary>
    [Column("描述")]
    public string Remarks { get; set; }
}
