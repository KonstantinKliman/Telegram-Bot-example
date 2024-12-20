using TelegramBot.Application.Interfaces.Repositories;
using TelegramBot.Application.Interfaces.Repositories.UnitOfWork;

namespace TelegramBot.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public IUserRepository Users { get; }

    public UnitOfWork(ApplicationDbContext context, IUserRepository userRepository)
    {
        _context = context;
        Users = userRepository;
    }
    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
