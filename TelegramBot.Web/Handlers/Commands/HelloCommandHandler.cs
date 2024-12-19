using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Core.DTO;
using TelegramBot.Core.Interfaces;

namespace TelegramBot.Core.CommandHandlers;

public class HelloCommandHandler(ITelegramBotClient bot) : ICommandHandler
{
    public async Task Handle(UpdateDTO updateDTO) 
    {
        var message = "Hello Handler is working!";

        await bot.SendMessage(updateDTO.SenderChatId, message);
    }
}
