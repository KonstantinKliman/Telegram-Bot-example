using System;
using Telegram.Bot;

namespace TelegramBot.Handlers;

public class HelloHandler(ITelegramBotClient bot) : BaseHanlder
{
    public override async Task Handle(long chatId)
    {
        string message = "Hello Handler is working!";

        await bot.SendMessage(chatId, message);
    }
}
