using JiangDuo.Application.System.Config.Dto;
using JiangDuo.Application.Tools;
using JiangDuo.Core.Models;
using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Mapster;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yitter.IdGenerator;
using JiangDuo.Core.Utils;
using Furion.FriendlyException;
using JiangDuo.Application.AppletService.OfficialApplet.Services;

namespace JiangDuo.Application.AppletAppService.OfficialApplet.Services
{
    public class OfficialAppletService:IOfficialAppletService, ITransient
    {
        private readonly ILogger<OfficialAppletService> _logger;

        public OfficialAppletService(ILogger<OfficialAppletService> logger)
        {
            _logger = logger;
        }




    }
}
