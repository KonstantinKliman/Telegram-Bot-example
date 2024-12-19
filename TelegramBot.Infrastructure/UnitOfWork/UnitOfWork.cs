using System;
using TelegramBot.Core.Entities;
using TelegramBot.Core.Interfaces.Repositories;

namespace TelegramBot.Infrastructure.UnitOfWork;

public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    public IRepository<User> Users { get; }

    public async Task<int> CompleteAsync()
    {
        return await context.SaveChangesAsync();
    }
}
