using System;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Core.Interfaces;

namespace TelegramBot.Core.Handlers;

public class HelloHandler(ITelegramBotClient bot) : ICommandHandler
{
    public async Task Handle(Update update) 
    {
        var message = "Hello Handler is working!";

        await bot.SendMessage(update.Message!.Chat.Id, message);
    }
}
