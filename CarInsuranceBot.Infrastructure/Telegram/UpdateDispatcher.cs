using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Exceptions;

namespace CarInsuranceBot.Infrastructure.Telegram
{
    public class UpdateDispatcher : IUpdateHandler
    {
        private readonly ILogger<UpdateDispatcher> _logger;
        private readonly ITelegramBotClient _botClient;

        public UpdateDispatcher(ILogger<UpdateDispatcher> logger, ITelegramBotClient botClient)
        {
            _logger = logger;
            _botClient = botClient;
        }

        public async Task HandleUpdateAsync(ITelegramBotClient bot, Update update, CancellationToken ct)
        {
            try
            {
                if (update.Type == UpdateType.Message && update.Message!.Text != null)
                {
                    var message = update.Message;
                    var chatId = message.Chat.Id;

                    if (message.Text == "/start")
                    {
                        await _botClient.SendMessage(
                            chatId,
                            "Вітаю! Надішліть фото паспорта для оформлення страхування.",
                            cancellationToken: ct);
                    }
                    else
                    {
                        await _botClient.SendMessage(
                            chatId,
                            "Надішліть, будь ласка, фото паспорта або введіть /start.",
                            cancellationToken: ct);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error handling update");
            }
        }

        public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken ct)
        {
            if (exception is ApiRequestException apiEx)
            {
                Console.WriteLine($"Telegram API Error: [{apiEx.ErrorCode}] {apiEx.Message}");
            }
            else
            {
                Console.WriteLine(exception.ToString());
            }

            return Task.CompletedTask;
        }

        public Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, HandleErrorSource source, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
