using System;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using TelegramBot.Handlers;

namespace TelegramBot.Services;

public class UpdateHandlerService(ITelegramBotClient bot, ILogger<UpdateHandlerService> logger) : IUpdateHandler
{
    public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, HandleErrorSource source, CancellationToken cancellationToken)
    {
        logger.LogInformation("HandleError: {Exception}", exception);
        if (exception is RequestException)
            await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
    }

    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        Dictionary<string, BaseHanlder> handlers = new Dictionary<string, BaseHanlder> {
            {"/start", new StartHandler(bot)},
            {"/hello", new HelloHandler(bot)}
        };

        long chatId = update.Message.Chat.Id;

        var handler = handlers[update.Message!.Text];
        await handler.Handle(chatId);
    }
}
