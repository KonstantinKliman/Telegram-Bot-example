using System;

namespace TelegramBot.Core.Interfaces;

public interface ICommandHandler
{
    Task Handle(long chatId);
}
