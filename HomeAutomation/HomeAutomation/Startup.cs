using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Linq;
using HomeAutomation.Facades;
using HomeAutomation.Facades.Interfaces;
using HomeAutomation.Models.UI;
using HomeAutomation.Services;
using MQTTnet;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using RestEase;
using HomeAutomation.Services.Interfaces;

namespace HomeAutomation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Configuration);
            services.AddSingleton<IMqttFacade, MqttFacade>();
            services.AddSingleton<ITelegramFacade, TelegramFacade>();
            services.AddSingleton(RestClient.For<ITelegramService>("https://api.telegram.org/"));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HomeAutomation", Version = "v1" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); //This line
            });

            var settings = Configuration.GetSection("MQTTSettings").Get<MqttSettings>();

            ConfigureMqtt(settings);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            
            app.UseSwagger().UseSwaggerUI(c =>
            {
                c.RoutePrefix = string.Empty;
                c.SwaggerEndpoint("./swagger/v1/swagger.json", "HomeAutomationV1"); //originally "./swagger/v1/swagger.json"
            });

            app.UseHttpsRedirection()
                .UseAuthentication()
                .UseRouting()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
        }

        public static void ConfigureMqtt(MqttSettings settings)
        {
            MqttService.ConfigureMqtt(settings);
        }

        public static void handlePublish(MqttApplicationMessageReceivedEventArgs x)
        {

        }

        public static void onPublisherConnected(MqttClientConnectedEventArgs x)
        {

        }

        public static void onPublisherDisconnected(MqttClientDisconnectedEventArgs x)
        {

        }
    }
}
