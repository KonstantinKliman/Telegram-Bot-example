using System;
using System.Reflection.Metadata;
using Telegram.Bot;

namespace TelegramBot.Handlers;

public class StartHandler(ITelegramBotClient bot) : ICommandHandler
{
    public async Task Handle(long chatId) 
    {
        var message = "Start Handler is working!";

        await bot.SendMessage(chatId, message);
    }
}
