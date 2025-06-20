using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;

namespace CarInsuranceBot.TelegramHost.HostedServices
{
    public class TelegramPollingService : BackgroundService
    {
        private readonly ILogger<TelegramPollingService> _logger;
        private readonly TelegramBotClient _botClient;
        private readonly IUpdateHandler _updateHandler;

        public TelegramPollingService(
            ILogger<TelegramPollingService> logger,
            TelegramBotClient botClient,
            IUpdateHandler updateHandler)
        {
            _logger = logger;
            _botClient = botClient;
            _updateHandler = updateHandler;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = Array.Empty<UpdateType>() // receive all update types
            };

            _logger.LogInformation("Starting Telegram Bot...");

            _botClient.StartReceiving(
                updateHandler: _updateHandler,
                receiverOptions: receiverOptions,
                cancellationToken: stoppingToken);

            var me = await _botClient.GetMe();
            _logger.LogInformation("Bot started. Username: @{Username}", me.Username);
        }
    }
}
