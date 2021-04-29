using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InvestmentMngmt.API.Bootstrap
{
    /// <summary>
    /// 
    /// </summary>
    public static class DistributedCacheConfiguration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureDistributedCache(this IServiceCollection services, IConfiguration configuration)
        {            
            var redisConnecttion = configuration.GetConnectionString("RedisConnectionString");
            if (!string.IsNullOrEmpty(redisConnecttion))
            {
                services.AddDistributedRedisCache(options =>
                {
                    options.Configuration = redisConnecttion;
                    options.InstanceName = "InvestmentPortfolio:";
                });
            }
            else
            {
                services.AddDistributedMemoryCache();
            }

            return services;
        }
    }
}
