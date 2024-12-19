using System;
using System.Reflection.Metadata;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Core.Interfaces;

namespace TelegramBot.Core.Handlers;

public class StartHandler(ITelegramBotClient bot) : ICommandHandler
{
    public async Task Handle(Update update) 
    {
        var message = "Start Handler is working!";

        await bot.SendMessage(update.Message!.Chat.Id, message);
    }
}
