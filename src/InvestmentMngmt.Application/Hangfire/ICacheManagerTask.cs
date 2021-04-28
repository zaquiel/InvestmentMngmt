using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentMngmt.Application.Hangfire
{
    public interface ICacheManagerTask
    {
        Task UpdateCacheAsync();
        Task ClearCacheAsync();

    }
}
