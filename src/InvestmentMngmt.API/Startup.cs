using InvestmentMngmt.API.Bootstrap;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace InvestmentMngmt.API
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        private IConfiguration _configuration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>        
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureGeneralOptions();
            services.ConfigureVersioning();            
            services.ConfigureSwaggerServices();
            services.ConfigureMediatR();
            services.ConfigureInvestmentPortfolio(_configuration);
            services.ConfigureDistributedCache(_configuration);
            services.ConfigureTrancing();
            services.ConfigureHangfireServer();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="apiVersionDescriptionProvider"></param>
        public void Configure(IApplicationBuilder app, 
            IWebHostEnvironment env, 
            IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            app.UseHttpsRedirection();
            app.UseRouting();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.ConfigureSwagger(apiVersionDescriptionProvider);

            app.ConfigureHangfireDashboard(_configuration);
        }
    }
}
