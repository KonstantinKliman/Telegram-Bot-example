using System;
using Telegram.Bot.Types;

namespace TelegramBot.Core.Interfaces;

public interface ICommandHandler
{
    Task Handle(Update update);
}
