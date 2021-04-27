using InvestmentMngmt.Application.Extensions;
using InvestmentMngmt.Application.Services;
using InvestmentMngmt.Domain.Constants;
using Microsoft.Extensions.Caching.Distributed;
using OpenTracing;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace InvestmentMngmt.Application.Investment.Commands.Get
{
    public class Handler: IHandlerBase<Request, Response<Result>>
    {
        private readonly IInvestmentSummaryService _investmentSummaryService;
        private readonly IDistributedCache _distributedCache;
        private readonly ITracer _tracer;

        public Handler(IInvestmentSummaryService investmentSummaryService,
            IDistributedCache distributedCache,
            ITracer tracer)
        {
            _investmentSummaryService = investmentSummaryService;
            _distributedCache = distributedCache;
            _tracer = tracer;
        }

        public async Task<Response<Result>> Handle(Request request, CancellationToken cancellationToken)
        {
            var distributedCacheKey = $"{DistributedCacheKeys.InvestmentPortfolio}{DateTime.Now.ToShortDateString()}";
            var resultResponseBytes = await _distributedCache.GetAsync(distributedCacheKey, cancellationToken);            

            if (resultResponseBytes is null)
            {
                _tracer.ActiveSpan.SetTag("cache", false);
                var resultResponse = await _investmentSummaryService.GetSummaryAsync();
                await _distributedCache.SetAsync(distributedCacheKey, JsonSerializer.SerializeToUtf8Bytes(resultResponse), DateTime.Now.UntilMidnight(), cancellationToken);

                return new Response<Result>(resultResponse);            
            }
            else
            {
                _tracer.ActiveSpan.SetTag("cache", true);
                var resultResponse = JsonSerializer.Deserialize<Result>(new ReadOnlySpan<byte>(resultResponseBytes));                    
                return new Response<Result>(resultResponse);
            }
        }
    }
}
