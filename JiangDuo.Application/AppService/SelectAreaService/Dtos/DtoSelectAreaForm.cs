using JiangDuo.Core.Base;
using JiangDuo.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppService.SelectAreaService.Dto
{
    public class DtoSelectAreaForm 
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long? Id { get;  set; }
        /// <summary>
        /// 选区名称
        /// </summary>
        [MaxLength(50)]
        public string SelectAreaName { get; set; } = null!;

        /// <summary>
        /// 省
        /// </summary>
        public long? Province { get; set; }
        /// <summary>
        /// 市区
        /// </summary>
        public long? City { get; set; }
        /// <summary>
        /// 区
        /// </summary>
        public long? Area { get; set; }
        /// <summary>
        /// 区域类型
        /// </summary>
        public SelectAreaTypeEnum? SelectAreaType { get; set; }
        /// <summary>
        /// 父级Id
        /// </summary>
        public long? ParentId { get; set; }
        /// <summary>
        /// 选区范围(坐标集合)
        /// </summary>
        public string SelectAreaRange { get; set; }

        /// <summary>
        /// 人口数
        /// </summary>
        public long Population { get; set; }
        /// <summary>
        /// 总面积
        /// </summary>
        public double GrossArea { get; set; }
    }
}
