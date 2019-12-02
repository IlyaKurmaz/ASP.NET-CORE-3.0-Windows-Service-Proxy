using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging.EventLog;
using Serilog;
using Serilog.Events;

namespace SFTP_Proxy_Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
              .MinimumLevel.Debug()
              .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
              .Enrich.FromLogContext()
              .WriteTo.File(@"C:\temp\sftp\LogFile.txt")
              .CreateLogger();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
              .ConfigureServices(services =>
              {
                  services.Configure<EventLogSettings>(config =>
                  {
                      config.LogName = "SFTP Proxy API";
                      config.SourceName = "SFTP Proxy API";
                  });
              })
              .ConfigureWebHostDefaults(webBuilder =>
              {
                  webBuilder.UseStartup<Startup>();
              })
              .ConfigureWebHost(config =>
              {
                  config.UseUrls("http://*:5050");
              })
              .UseWindowsService();
    }
}
