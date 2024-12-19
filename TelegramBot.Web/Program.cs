using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using TelegramBot;
using TelegramBot.Core.Interfaces.Repositories;
using TelegramBot.Core.Services;
using TelegramBot.Infrastructure;
using TelegramBot.Infrastructure.Repositories;
using TelegramBot.Infrastructure.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

var botConfiguration = builder.Configuration.GetSection("BotConfiguration");
builder.Services.Configure<BotConfiguration>(botConfiguration);
builder.Services.AddHttpClient("tgwebhook").RemoveAllLoggers().AddTypedClient<ITelegramBotClient>(
    httpClient => new TelegramBotClient(botConfiguration.Get<BotConfiguration>()!.BotToken, httpClient));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<UpdateHandlerService>();
builder.Services.ConfigureTelegramBotMvc();

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("TelegramBotDatabase")));

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.ApplyMigrations();

app.Run();
