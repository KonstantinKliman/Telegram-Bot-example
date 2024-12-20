using TelegramBot.Application.DTO;

namespace TelegramBot.Application.Interfaces;

public interface ICommandHandler
{
    Task Handle(UpdateDTO updateDTO);
}
