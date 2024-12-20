using System;

namespace TelegramBot.Application.DTO;

public class UserDTO
{
    public long ChatId { get; set; }

    public string? Name { get; set; }

    public required string FirstName { get; set; }

    public string? LastName { get; set; }
}
