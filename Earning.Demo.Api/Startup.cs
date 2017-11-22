using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Earning.Demo.Api.Services;
using Microsoft.Extensions.Configuration;
using Earning.Demo.Shared.Services;
using Earning.Demo.Shared;

namespace Earning.Demo.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Bootstrapper.ConfigureServices(services);

            // This only for demonstration purposes
            services.AddScoped<IStorageService>((ctx) =>
            {
                var configuration = ctx.GetService<IConfigurationService>();
                return configuration.IsAbTesting ?
                new AbTestStorageService(configuration) :
                new StorageService(configuration);
            });

            services.AddMvc();
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());

            app.UseMvc();
        }
    }
}
