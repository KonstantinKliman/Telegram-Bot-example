using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Application.DTO;
using TelegramBot.Application.Interfaces;

namespace TelegramBot.Presentation.Handlers.Commands;

public class StartCommandHandler(ITelegramBotClient bot) : ICommandHandler
{
    public async Task Handle(UpdateDTO updateDTO) 
    {
        var message = "Вы ввели команду /start";

        await bot.SendMessage(
            updateDTO.SenderChatId, 
            text: message
        );
    }
}
