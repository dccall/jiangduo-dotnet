using JiangDuo.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
