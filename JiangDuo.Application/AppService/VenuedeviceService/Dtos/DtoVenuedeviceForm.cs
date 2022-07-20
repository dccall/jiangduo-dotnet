﻿using JiangDuo.Core.Models;
using Furion.DatabaseAccessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using JiangDuo.Core.Enums;

namespace JiangDuo.Application.AppService.VenuedeviceService.Dto
{
    public class DtoVenuedeviceForm 
    {
        public long? Id { get; set; }

        /// <summary>
        /// 1.场地，2.设备
        /// </summary>
        public VenuedeviceEnum Type { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [MaxLength(50)]
        public string Name { get; set; }
        /// <summary>
        /// 所属建筑Id
        /// </summary>
        public long? BuildingId { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [MaxLength(255)]
        public string Remarks { get; set; }

    }
}
