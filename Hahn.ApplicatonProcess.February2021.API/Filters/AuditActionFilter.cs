using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.February2021.API.Filters
{
    /// <summary>
    /// Logs the Request and Response of the API
    /// TODO : Setup a correlation id to track individual request and response , also log request body
    /// </summary>
    public class AuditActionFilter : IActionFilter
    {
        private readonly ILogger _logger;

        public AuditActionFilter(ILoggerFactory logFactory)
        {
            _logger = logFactory.CreateLogger("Assets");
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            _logger.LogInformation($"HTTP Method - {request.Method}");
            _logger.LogInformation($"Entering API - {request.Scheme}://{request.Host + request.Path + request.QueryString}");            
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var response = context.HttpContext.Response;
            _logger.LogInformation($"Exiting API");
            _logger.LogInformation($"Status Code {response.StatusCode}");                       
        }

    }
}
