using Telegram.Bot;
using TelegramBot;
using TelegramBot.Core.Services;

var builder = WebApplication.CreateBuilder(args);

var botConfiguration = builder.Configuration.GetSection("BotConfiguration");
builder.Services.Configure<BotConfiguration>(botConfiguration);
builder.Services.AddHttpClient("tgwebhook").RemoveAllLoggers().AddTypedClient<ITelegramBotClient>(
    httpClient => new TelegramBotClient(botConfiguration.Get<BotConfiguration>()!.BotToken, httpClient));
builder.Services.AddSingleton<UpdateHandlerService>();
builder.Services.ConfigureTelegramBotMvc();

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
