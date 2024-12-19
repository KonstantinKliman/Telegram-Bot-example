namespace TelegramBot.Core.Interfaces.Repositories.UnitOfWork;

public interface IUnitOfWork
{
    IUserRepository Users { get; }
    Task<int> CompleteAsync();
}
