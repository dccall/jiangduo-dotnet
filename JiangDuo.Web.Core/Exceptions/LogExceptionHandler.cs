using Furion;
using log4net;
using Furion.DependencyInjection;
using Furion.FriendlyException;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.IO;
using System.Text;

namespace JiangDuo.Web.Core.Exceptions
{
    public class LogExceptionHandler : IGlobalExceptionHandler, ISingleton
    {

        private readonly ILogger<LogExceptionHandler> _logger;
        private readonly ILog _log4Net;
        public LogExceptionHandler(ILogger<LogExceptionHandler> logger)
        {
            _log4Net = LogManager.GetLogger(typeof(LogExceptionHandler));
            _logger = logger;
        }

        public Task OnExceptionAsync(ExceptionContext context)
        {
            var requestPath = context.HttpContext.Request.Path.Value;
            var method = context.HttpContext.Request.Method;
            _log4Net.Error( context.Exception);
            return Task.CompletedTask;
        }

        
    }
}
