using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore;

namespace HomeAutomation
{
#pragma warning disable RCS1102
    public class Program
#pragma warning restore RCS1102
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls("http://localhost:5000/")
                .UseKestrel()
                .Build();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                    .UseUrls("http://localhost:5000/");
                });
    }
}
