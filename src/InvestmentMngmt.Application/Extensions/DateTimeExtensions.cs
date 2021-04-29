using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentMngmt.Application.Extensions
{
    public static class DateTimeExtensions
    {
        public static DistributedCacheEntryOptions UntilMidnight(this DateTime dateTime)
        {
            var untilMidnight = DateTime.Today.AddDays(1.0) - dateTime;
            return new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(untilMidnight.TotalSeconds)
            };
        }
    }
}
