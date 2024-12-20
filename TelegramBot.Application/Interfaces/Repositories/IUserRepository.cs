using TelegramBot.Application.Entities;

namespace TelegramBot.Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task Create(User user);

    void Delete(int id);

    IEnumerable<User> GetAll();

    Task<User?> GetById(int id);

    void Update(User user);

    Task<User?> GetByChatId(long chatId);
}
