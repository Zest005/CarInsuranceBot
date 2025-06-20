using CarInsuranceBot.Application.Services;
using CarInsuranceBot.Infrastructure.Extensions;
using CarInsuranceBot.TelegramHost.HostedServices;
using Serilog;

namespace CarInsuranceBot.TelegramHost
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            // Serilog
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .CreateLogger();

            builder.Logging.AddSerilog(Log.Logger);

            // DI
            builder.Services.AddApplicationServices(); // empty extension
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddHostedService<TelegramPollingService>();


            await builder.Build().RunAsync();
        }
    }
}