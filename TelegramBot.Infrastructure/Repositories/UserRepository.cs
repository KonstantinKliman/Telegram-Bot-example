using System;
using Microsoft.EntityFrameworkCore;
using TelegramBot.Application.Entities;
using TelegramBot.Application.Interfaces.Repositories;

namespace TelegramBot.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository (ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Create(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<User> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<User?> GetById(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public void Update(User user)
    {
        throw new NotImplementedException();
    }

    public async Task<User?> GetByChatId(long chatId)
    {
        return await _context.Users.SingleOrDefaultAsync(u => u.ChatId == chatId);
    }
}
