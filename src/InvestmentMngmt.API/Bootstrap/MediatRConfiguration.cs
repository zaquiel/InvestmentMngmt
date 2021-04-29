using InvestmentMngmt.Application;
using InvestmentMngmt.Application.Investment.Commands.Get;
using InvestmentMngmt.Application.Pipeline;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace InvestmentMngmt.API.Bootstrap
{
    /// <summary>
    /// 
    /// </summary>
    public static class MediatRConfiguration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureMediatR(this IServiceCollection services)
        {
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(MeasureTime<,>));
            services.AddScoped(typeof(IRequestExceptionHandler<Request, Response<Result>>), typeof(ExceptionHandle));

            services.AddMediatR(typeof(IHandlerBase<,>));

            return services;
        }
    }
}
