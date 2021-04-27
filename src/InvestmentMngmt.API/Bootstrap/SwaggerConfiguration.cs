using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace InvestmentMngmt.API.Bootstrap
{ 
    /// <summary>
    /// 
    /// </summary>
    public static class SwaggerConfiguration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureSwaggerServices(this IServiceCollection services)
        {
            var apiProviderDescription = services.BuildServiceProvider().GetService<IApiVersionDescriptionProvider>();

            services.AddSwaggerGen(opt =>
            {
                foreach (var description in apiProviderDescription.ApiVersionDescriptions)
                {
                    opt.SwaggerDoc(
                        description.GroupName, 
                        new OpenApiInfo 
                        { 
                            Title = "InvestmentMngmt API", 
                            Version = description.ApiVersion.ToString() 
                        });
                }

                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
                opt.IncludeXmlComments(xmlCommentsFullPath);

            });                       

            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder ConfigureSwagger(this IApplicationBuilder app, IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            app.UseSwagger();
            app.UseSwaggerUI(opt =>
            {
                foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
                {
                    opt.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
            });

            return app;
        }
    }

}
