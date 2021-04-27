using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OpenTracing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace InvestmentMngmt.API.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class TracingActionFilter: IActionFilter
    {
        private readonly ITracer _tracer;
        private readonly ILogger<TracingActionFilter> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tracer"></param>
        /// <param name="logger"></param>
        public TracingActionFilter(ITracer tracer, ILogger<TracingActionFilter> logger)
        {
            _tracer = tracer;
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var operationName = context.HttpContext.Request.Path.Value;
            var scope = _tracer.BuildSpan(operationName).StartActive();

            var receiveMessage = new
            {
                Data = DateTime.UtcNow,
                OperationName = operationName,
                SpainId = scope.Span.Context.SpanId,
                scope.Span.Context.TraceId
            };

            _logger.LogInformation($"ReceiveMessage: {JsonConvert.SerializeObject(receiveMessage)}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var scope = _tracer.ScopeManager.Active;

            var requestOk = context.HttpContext.Response.StatusCode != (int)HttpStatusCode.OK ? "Request OK" : "Request Fail";

            _tracer.ActiveSpan?.Log(requestOk);            

            scope.Span.SetTag("ResultData", JsonConvert.SerializeObject(context.Result)).Finish();
        }
    }
}
