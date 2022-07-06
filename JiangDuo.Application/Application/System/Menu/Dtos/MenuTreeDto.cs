﻿using JiangDuo.Core;
using JiangDuo.Core.Enums;
using JiangDuo.Core.Models;
using Furion.DatabaseAccessor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.Menu.Dtos
{
    [Manual]
    public class MenuTreeDto : SysMenu
    {
        /// <summary>
        /// 独立布局页
        /// </summary>
        public string SingleLayout { get; set; }
        /// <summary>
        /// 组件布局页
        /// </summary>
        public string Component { get; set; }
        /// <summary>
        /// 子集
        /// </summary>
        public  List<MenuTreeDto> Children { get; set; }
        public Meta Meta
        {
            get {
                return new Meta() {
                    Icon = this.Icon,
                    Order = this.Order,
                    Title = this.Title,
                    SingleLayout = this.SingleLayout,
                    Hide = this.Hide,
                }; 
            }
        }
    }

    public class Meta
    {
        public bool? Hide { get; set; }
        public string Icon { get; set; }
        public string SingleLayout { get; set; }
        public int Order { get; set; }
        public string Title { get; set; }
    }
}
