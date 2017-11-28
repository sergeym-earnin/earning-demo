using Earning.Demo.Shared.Common;
using Earning.Demo.Shared.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Earning.Demo.Shared
{
    public static class Bootstrapper
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IConfigurationService, ConfigurationService>();
            services.AddTransient<IEnviromentService, EnviromentService>();
            services.AddScoped<IApiClient, ApiClient>();
        }
    }
}
