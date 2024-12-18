using System;

namespace TelegramBot.Handlers;

public interface ICommandHandler
{
    Task Handle(long chatId);
}
