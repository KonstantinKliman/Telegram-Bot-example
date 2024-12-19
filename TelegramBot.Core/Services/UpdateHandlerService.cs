using System;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using TelegramBot.Core.Interfaces;
using TelegramBot.Core.Handlers;
using Microsoft.Extensions.Logging;

namespace TelegramBot.Core.Services;

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
        var commandHandlers = new Dictionary<string, ICommandHandler> {
            {"/start", new StartHandler(bot)},
            {"/hello", new HelloHandler(bot)}
        };   

        var chatId = update.Message!.Chat.Id;

        if (commandHandlers.TryGetValue(update.Message.Text!, out var handler))
        {
            await handler.Handle(chatId);
        }
        else 
        {
            await botClient.SendMessage(chatId, "Unknown command!");
        }
    }
}
