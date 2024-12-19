using TelegramBot.Core.DTO;

namespace TelegramBot.Core.Interfaces;

public interface ICommandHandler
{
    Task Handle(UpdateDTO updateDTO);
}
