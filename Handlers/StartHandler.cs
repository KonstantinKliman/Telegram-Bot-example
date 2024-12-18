using System;
using System.Reflection.Metadata;
using Telegram.Bot;

namespace TelegramBot.Handlers;

public class StartHandler(ITelegramBotClient bot) : BaseHanlder
{
    public override async Task Handle(long chatId) 
    {
        string message = "Start Handler is working!";

        await bot.SendMessage(chatId, message);
    }
}
