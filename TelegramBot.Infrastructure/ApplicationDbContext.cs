using Microsoft.EntityFrameworkCore;
using TelegramBot.Application.Entities;

namespace TelegramBot.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;

    public ApplicationDbContext() { }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = "Server=localhost;Port=5432;Database=telegram_bot_db;Username=postgres;Password=root";
            optionsBuilder.UseNpgsql(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) // у меня не получилось через атрибуты у сущности 
    {
        base.OnModelCreating(modelBuilder);
    
        modelBuilder.Entity<User>()
            .HasIndex(u => u.ChatId)
            .IsUnique();
    }
}
