using System;

namespace TelegramBot.Handlers;

abstract public class BaseHanlder
{
    abstract public Task Handle(long chatId);
}
