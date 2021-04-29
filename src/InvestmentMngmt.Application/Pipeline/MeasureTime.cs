using InvestmentMngmt.Application.Services.External;
using MediatR;
using OpenTracing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InvestmentMngmt.Application.Pipeline
{
    public class MeasureTime<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {        
        private readonly ITracer _tracer;

        public MeasureTime(ITracer tracer)
        {            
            _tracer = tracer;
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var stopWatch = Stopwatch.StartNew();
            var result = await next();
            var elapsed = stopWatch.Elapsed;

            _tracer.ActiveSpan.SetTag("Request Time", $"{elapsed}ms");            

            Debug.WriteLine($"Tempo de execução do request {typeof(TRequest).FullName}: {elapsed}ms");
            return result;
        }
    }
}
