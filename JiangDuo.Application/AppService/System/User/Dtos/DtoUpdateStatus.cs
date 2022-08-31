﻿using JiangDuo.Core.Enums;

namespace JiangDuo.Application.AppService.System.User.Dtos
{
    public class DtoUpdateStatus
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long? Id { get; set; }

        /// <summary>
        /// 帐号状态（0正常 1停用）
        /// </summary>
        public UserStatus Status { get; set; }
    }
}