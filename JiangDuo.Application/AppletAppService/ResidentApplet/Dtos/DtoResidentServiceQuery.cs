﻿using JiangDuo.Core.Base;
using JiangDuo.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiangDuo.Application.AppletAppService.ResidentApplet.Dtos;

public class DtoResidentServiceQuery : BaseRequest
{
    /// <summary>
    /// 服务名称
    /// </summary>
    public string ServiceName { get; set; }
    /// <summary>
    /// 服务类型
    /// </summary>
    public ServiceTypeEnum? ServiceType { get; set; }
}

