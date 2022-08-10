using JiangDuo.Core.Models;
using Furion.DatabaseAccessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace JiangDuo.Application.AppService.BuildingService.Dto
{
    [Manual]
    public class DtoBuildingForm 
    {
        public long? Id { get; set; }

        /// <summary>
        /// 建筑名称
        /// </summary>
        [MaxLength(50)]
        public string BuildingName { get; set; } = null!;
        /// <summary>
        /// 建筑地址
        /// </summary>
        [MaxLength(300)]
        public string Address { get; set; }
        /// <summary>
        /// 建筑位置
        /// </summary>
        [MaxLength(50)]
        public string Location { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [MaxLength(300)]
        public string Remarks { get; set; }
        /// <summary>
        /// 建筑图片
        /// </summary>
        //[MaxLength(500)]
        public string Images { get; set; }

        public List<SysUploadFile> ImageList { get; set; } = new List<SysUploadFile>();
        /// <summary>
        /// 所属选区
        /// </summary>
        public long? SelectAreaId { get; set; }

    }
}
