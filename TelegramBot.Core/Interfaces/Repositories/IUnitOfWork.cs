using System;
using TelegramBot.Core.Entities;

namespace TelegramBot.Core.Interfaces.Repositories;

public interface IUnitOfWork
{
    IRepository<User> Users { get; }
    Task<int> CompleteAsync();
}
