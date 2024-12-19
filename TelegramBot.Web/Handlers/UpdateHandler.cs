using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using TelegramBot.Core.CommandHandlers;
using TelegramBot.Core.DTO;
using TelegramBot.Core.Interfaces;
using TelegramBot.Core.Interfaces.Repositories.UnitOfWork;

namespace TelegramBot.Core.Services;

public class UpdateHandler : IUpdateHandler
{
    private readonly ITelegramBotClient _bot;

    private readonly ILogger<UpdateHandler> _logger;

    private readonly IUnitOfWork _unitOfWork;

    public UpdateHandler(ITelegramBotClient bot, ILogger<UpdateHandler> logger, IUnitOfWork unitOfWork)
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

        if (update.Message == null || update.Message.From == null)
        {
            _logger.LogWarning("Update does not contain a valid message or sender information.");
            return;
        }

        var exsistedUser = await _unitOfWork.Users.GetByChatId(update.Message.From.Id);

        if (exsistedUser == null)
        {
            var userEntity = new Entities.User
            {
                ChatId = update.Message.From.Id,
                Name = update.Message.From.Username,
                FirstName = update.Message.From.FirstName,
                LastName = update.Message.From.LastName
            };

            await _unitOfWork.Users.Create(userEntity);
            await _unitOfWork.CompleteAsync();
        }

        var messageText = update.Message.Text;
        if (string.IsNullOrEmpty(messageText))
        {
            _logger.LogWarning("Message does not contain text.");
            return;
        }

        var commandHandlers = new Dictionary<string, ICommandHandler>
        {
            { "/start", new StartCommandHandler(botClient) },
            { "/hello", new HelloCommandHandler(botClient) }
        };

        var chatId = update.Message.Chat.Id;

        var updateDTO = new UpdateDTO
        {
            SenderChatId = chatId
        };

        if (commandHandlers.TryGetValue(messageText, out var handler))
        {
            await handler.Handle(updateDTO);
        }
        else
        {
            await botClient.SendMessage(chatId, "Unknown command!");
        }
    }

}