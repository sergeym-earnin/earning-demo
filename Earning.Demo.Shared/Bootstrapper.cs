using Earning.Demo.Shared.Common;
using Earning.Demo.Shared.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Earning.Demo.Shared
{
    public static class Bootstrapper
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IConfigurationService, ConfigurationService>();
            services.AddTransient<IEnviromentService, EnviromentService>();
            services.AddScoped<IApiClient>(sp => {
                return new ApiClient(ApiPolicies.Create(),
                                     sp.GetService<IConfigurationService>(), 
                                     sp.GetService<IHttpContextAccessor>());
            });
        }
    }
}
