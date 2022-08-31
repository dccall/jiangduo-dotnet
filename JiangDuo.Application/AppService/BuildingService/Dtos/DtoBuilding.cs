using Furion.DatabaseAccessor;
using JiangDuo.Core.Models;
using System.Collections.Generic;

namespace JiangDuo.Application.AppService.BuildingService.Dto
{
    [Manual]
    public class DtoBuilding : Building
    {
        public List<SysUploadFile> ImageList { get; set; } = new List<SysUploadFile>();
    }
}