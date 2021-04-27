using OpenTracing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InvestmentMngmt.Application
{
    public class OpenTrancingMessageHandler: DelegatingHandler
    {
        private readonly ITracer _tracer;

        public OpenTrancingMessageHandler(ITracer tracer)
        {
            _tracer = tracer;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);
            var traceLogResponse = await response.Content.ReadAsStringAsync(cancellationToken);
            _tracer.ActiveSpan?.Log($"Response: {traceLogResponse}");
            return response;
        }
    }
}
