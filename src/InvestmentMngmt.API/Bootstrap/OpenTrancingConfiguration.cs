using InvestmentMngmt.API.Filters;
using Jaeger;
using Jaeger.Reporters;
using Jaeger.Samplers;
using Jaeger.Senders.Thrift;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTracing;
using OpenTracing.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace InvestmentMngmt.API.Bootstrap
{
    /// <summary>
    /// 
    /// </summary>
    public static class OpenTrancingConfiguration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureTrancing(this IServiceCollection services)
        {
            services.AddScoped<TracingActionFilter>();

            services.AddMvc(options =>
            {
                options.Filters.AddService<TracingActionFilter>();
            });

            services.AddSingleton<ITracer>(serviceProvider =>
            {
                var serviceName = Assembly.GetEntryAssembly().GetName().Name;

                ILoggerFactory loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

                ISampler sampler = new ConstSampler(sample: true);
                var reporter = new RemoteReporter.Builder().WithLoggerFactory(loggerFactory).WithSender(new UdpSender("localhost", 6831, 0)).Build();
                var tracer = new Tracer.Builder(serviceName)
                    .WithLoggerFactory(loggerFactory)
                    .WithReporter(reporter)
                    .WithSampler(sampler)
                    .Build();

                GlobalTracer.Register(tracer);

                return tracer;
            });

            services.AddOpenTracing();
            return services;
        }
    }
}
