using InvestmentMngmt.Application.Extensions;
using InvestmentMngmt.Application.Services;
using InvestmentMngmt.Domain.Constants;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace InvestmentMngmt.Application.Hangfire
{
    public class CacheManagerTask : ICacheManagerTask
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IInvestmentSummaryService _investmentSummaryService;

        public CacheManagerTask(IDistributedCache distributedCache, IInvestmentSummaryService investmentSummaryService)
        {
            _distributedCache = distributedCache;
            _investmentSummaryService = investmentSummaryService;
        }

        public async Task ClearCacheAsync()
        {
            await _distributedCache.RemoveAsync(DistributedCacheKeys.InvestmentPortfolio);
        }

        public async Task UpdateCacheAsync()
        {
            var result = await _investmentSummaryService.GetSummaryAsync();
            _ = _distributedCache.SetAsync(DistributedCacheKeys.InvestmentPortfolio, JsonSerializer.SerializeToUtf8Bytes(result), DateTime.Now.UntilMidnight());
        }
    }
}
