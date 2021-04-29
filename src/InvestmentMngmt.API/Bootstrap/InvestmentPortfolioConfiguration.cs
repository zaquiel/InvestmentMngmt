using System;
using Refit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using InvestmentMngmt.Application.Services.External;
using InvestmentMngmt.Application.Services;
using InvestmentMngmt.Domain.Settings;
using InvestmentMngmt.Application;

namespace InvestmentMngmt.API.Bootstrap
{ 
    /// <summary>
    /// 
    /// </summary>
    public static class InvestmentPortfolioConfiguration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureInvestmentPortfolio(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IInvestmentSummaryService, InvestmentSummaryService>();
            services.AddTransient<OpenTrancingMessageHandler>();
            services.AddRefitClient<IInvestmentPortfolioService>().ConfigureHttpClient(opt =>
            {
                opt.BaseAddress = new Uri(configuration.GetValue<string>("ExternalServicesSettings:BaseUri"));
                opt.Timeout = TimeSpan.FromSeconds(configuration.GetValue<int>("TimeoutHttpRequest"));                
            }).AddHttpMessageHandler<OpenTrancingMessageHandler>();

            services.Configure<ExternalServicesSettings>(configuration.GetSection("ExternalServicesSettings"));

            return services;            
        }
    }
}

