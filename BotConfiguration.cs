using System;
using Telegram.Bot;

namespace TelegramBot;

public class BotConfiguration
{
    public string BotToken { get; init; } = default!;
    public Uri BotWebhookUrl { get; init; } = default!;
}
