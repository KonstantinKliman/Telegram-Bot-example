using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using TelegramBot;
using TelegramBot.Application.Interfaces.Repositories;
using TelegramBot.Application.Interfaces.Repositories.UnitOfWork;
using TelegramBot.Presentation.Handlers;
using TelegramBot.Infrastructure;
using TelegramBot.Infrastructure.Repositories;
using TelegramBot.Infrastructure.UnitOfWork;
using TelegramBot.Application.Mapping;

var builder = WebApplication.CreateBuilder(args);

var botConfiguration = builder.Configuration.GetSection("BotConfiguration");
builder.Services.Configure<BotConfiguration>(botConfiguration);
builder.Services.AddHttpClient("tgwebhook").RemoveAllLoggers().AddTypedClient<ITelegramBotClient>(
    httpClient => new TelegramBotClient(botConfiguration.Get<BotConfiguration>()!.BotToken, httpClient));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<UpdateHandler>();
builder.Services.ConfigureTelegramBotMvc();

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("TelegramBotDatabase")));

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.ApplyMigrations();

app.Run();
