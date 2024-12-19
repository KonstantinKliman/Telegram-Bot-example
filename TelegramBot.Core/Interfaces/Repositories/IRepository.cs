using System;
using TelegramBot.Core.Entities;

namespace TelegramBot.Core.Interfaces.Repositories;

public interface IRepository<T> where T : class
{
    IEnumerable<T> GetAll();
    T GetById(int id);
    Task Create(T item);
    void Update(T item);
    void Delete(int id);
}