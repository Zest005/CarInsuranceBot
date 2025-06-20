using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using CarInsuranceBot.Infrastructure.Telegram;
using Telegram.Bot.Polling;

namespace CarInsuranceBot.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
        {
            var botToken = config["Telegram:Token"];

            services.AddSingleton<ITelegramBotClient>(new TelegramBotClient(botToken));

            services.AddSingleton<IUpdateHandler, UpdateDispatcher>();

            // TODO: Mindee, OpenAI, session storage

            return services;
        }
    }
}
