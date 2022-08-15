using JiangDuo.Core.Models;
using Furion.DatabaseAccessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JiangDuo.Core.Enums;

namespace JiangDuo.Application.AppService.VenuedeviceService.Dto
{
    [Manual]
    public class DtoVenuedevice : Venuedevice
    {
        /// <summary>
        /// 所属建筑名称
        /// </summary>
        public string BuildingName{ get; set; }
        /// <summary>
        /// 所属选区
        /// </summary>
        public string SelectAreaName { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName=> Type.GetDescription();
        /// <summary>
        /// 规章制度
        /// </summary>
        public Regulation Regulation { get; set; }

        /// <summary>
        /// 图片列表
        /// </summary>
        public List<SysUploadFile> ImageList { get; set; } = new List<SysUploadFile>();
    }
}
