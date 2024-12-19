using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Core.DTO;
using TelegramBot.Core.Interfaces;

namespace TelegramBot.Core.CommandHandlers;

public class StartCommandHandler(ITelegramBotClient bot) : ICommandHandler
{
    public async Task Handle(UpdateDTO updateDTO) 
    {
        var message = "Start Handler is working!";

        await bot.SendMessage(updateDTO.SenderChatId, message);
    }
}
