using System;
using Telegram.Bot;

namespace TelegramBot.Handlers;

public class HelloHandler(ITelegramBotClient bot) : ICommandHandler
{
    public async Task Handle(long chatId)
    {
        var message = "Hello Handler is working!";

        await bot.SendMessage(chatId, message);
    }
}
