using System;
using Microsoft.EntityFrameworkCore;
using TelegramBot.Infrastructure;

namespace TelegramBot;

public static class WebAppExtensions
{
    /// <summary>
    /// Запускает миграции.
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static void ApplyMigrations(this WebApplication app)
    {
        var serviceScopeFactory = app.Services.GetService<IServiceScopeFactory>();
        if (serviceScopeFactory != null)
        {
            using var scope = serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<ApplicationDbContext>()!;
            context.Database.Migrate();
        }
    }
}