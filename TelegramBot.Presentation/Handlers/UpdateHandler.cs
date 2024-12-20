using AutoMapper;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using TelegramBot.Application.DTO;
using TelegramBot.Application.Interfaces;
using TelegramBot.Application.Interfaces.Repositories.UnitOfWork;
using TelegramBot.Presentation.Handlers.Commands;

namespace TelegramBot.Presentation.Handlers;

public class UpdateHandler : IUpdateHandler
{
    private readonly ITelegramBotClient _bot;

    private readonly ILogger<UpdateHandler> _logger;

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateHandler(ITelegramBotClient bot, ILogger<UpdateHandler> logger, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _bot = bot;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, HandleErrorSource source, CancellationToken cancellationToken)
    {
        _logger.LogInformation("HandleError: {Exception}", exception);
        if (exception is RequestException)
            await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
    }

    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        await SetCommands();

        if (update.Message == null || update.Message.From == null)
        {
            _logger.LogWarning("Update does not contain a valid message or sender information.");
            return;
        }

        var exsistedUser = await _unitOfWork.Users.GetByChatId(update.Message.From.Id);

        if (exsistedUser == null)
        {
            var userEntity = new UserDTO
            {
                ChatId = update.Message.From.Id,
                Name = update.Message.From.Username,
                FirstName = update.Message.From.FirstName,
                LastName = update.Message.From.LastName
            };

            var user = _mapper.Map<Application.Entities.User>(userEntity);

            await _unitOfWork.Users.Create(user);
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
        };

        var updateDTO = new UpdateDTO
        {
            SenderChatId = update.Message.From.Id
        };

        if (commandHandlers.TryGetValue(messageText, out var handler))
        {
            await handler.Handle(updateDTO);
        }
        else
        {
            await botClient.SendMessage(update.Message.From.Id, "Unknown command!");
        }
    }

    private async Task SetCommands()
    {
        var commands = new List<BotCommand> 
        {
            new BotCommand { Command = "/start", Description = "Запускает или перезапускает бота"}
        };
        await _bot.SetMyCommands(commands);
    }
}