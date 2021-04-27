using InvestmentMngmt.API.Presentation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvestmentMngmt.API.Bootstrap
{
    /// <summary>
    /// 
    /// </summary>
    public static class GeneralConfiguration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureGeneralOptions(this IServiceCollection services)
        {
            services.AddControllers();

            services.AddTransient<IPresenter, Presenter>();

            return services;
        }
    }
}
