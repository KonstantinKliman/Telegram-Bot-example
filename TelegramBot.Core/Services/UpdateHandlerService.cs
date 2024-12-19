using System;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using TelegramBot.Core.Interfaces;
using TelegramBot.Core.Handlers;
using Microsoft.Extensions.Logging;
using TelegramBot.Core.Interfaces.Repositories;
using TelegramBot.Core.Entities;

namespace TelegramBot.Core.Services;

public class UpdateHandlerService : IUpdateHandler
{
    private readonly ITelegramBotClient _bot;

    private readonly ILogger<UpdateHandlerService> _logger;

    private readonly IUnitOfWork _unitOfWork;

    public UpdateHandlerService(ITelegramBotClient bot, ILogger<UpdateHandlerService> logger, IUnitOfWork unitOfWork)
    {
        _bot = bot;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, HandleErrorSource source, CancellationToken cancellationToken)
    {
        _logger.LogInformation("HandleError: {Exception}", exception);
        if (exception is RequestException)
            await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
    }

    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var userEntity = new Entities.User
        {
            ChatId = update.Message!.From!.Id,
            Name = update.Message.From.Username ?? "unknown",
            FirstName = update.Message.From.FirstName,
            LastName = update.Message.From.LastName
        };

        await _unitOfWork.Users.Create(userEntity);
        await _unitOfWork.CompleteAsync();

        var commandHandlers = new Dictionary<string, ICommandHandler> {
            {"/start", new StartHandler(botClient)},
            {"/hello", new HelloHandler(botClient)}
        };   

        var chatId = update.Message!.Chat.Id;

        if (commandHandlers.TryGetValue(update.Message.Text!, out var handler))
        {
            await handler.Handle(update);
        }
        else 
        {
            await botClient.SendMessage(chatId, "Unknown command!");
        }
    }
}
