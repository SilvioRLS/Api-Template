using HomeAutomation.Facades.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HomeAutomation.Facades
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSingletons(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IMqttFacade, MqttFacade>();
        }
    }
}
