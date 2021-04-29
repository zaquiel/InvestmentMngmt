using Hangfire;
using Hangfire.MemoryStorage;
using InvestmentMngmt.API.Filters;
using InvestmentMngmt.Application.Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace InvestmentMngmt.API.Bootstrap
{
    /// <summary>
    /// 
    /// </summary>
    public static class HangfireConfiguration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureHangfireServer(this IServiceCollection services)
        {           
            services.AddHangfire((provider, gConfig) =>
            {
                gConfig.UseMemoryStorage();
                gConfig.UseSerializerSettings(new Newtonsoft.Json.JsonSerializerSettings() 
                { 
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
            });

            services.AddHangfireServer();

            return services;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationBuilder"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IApplicationBuilder ConfigureHangfireDashboard(this IApplicationBuilder applicationBuilder, IConfiguration configuration)
        {
            applicationBuilder.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHangfireDashboard(options: new DashboardOptions
                {
                    AppPath = null,
                    Authorization = new[] { new AllowAllConnectionsFilter() },
                    IgnoreAntiforgeryToken = true,
                    StatsPollingInterval = 30000
                });
            });

            var updateCacheCronExpression = configuration.GetValue<string>("HangfireTaskSettings:UpdateCacheCronExpression");

            RecurringJob.AddOrUpdate<CacheManagerTask>("Update Cache", x => x.UpdateCacheAsync(), updateCacheCronExpression, TimeZoneInfo.Local);
            RecurringJob.AddOrUpdate<CacheManagerTask>("Clear Cache", x => x.ClearCacheAsync(), Cron.Never, TimeZoneInfo.Local);

            return applicationBuilder;
        }
    }
}
