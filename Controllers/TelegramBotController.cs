using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Services;

namespace TelegramBot.Controllers
{
    [Route("api/bot")]
    [ApiController]
    public class TelegramBotController(IOptions<BotConfiguration> Config) : ControllerBase
    {
        [HttpGet("setWebhook")]
        public async Task<string> SetWebHook([FromServices] ITelegramBotClient bot, CancellationToken ct)
        {
            var webhookUrl = Config.Value.BotWebhookUrl.AbsoluteUri;
            await bot.SetWebhook(webhookUrl, allowedUpdates: [], cancellationToken: ct);
            return $"Webhook set to {webhookUrl}";
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Update update, [FromServices] ITelegramBotClient bot, [FromServices] UpdateHandlerService handleUpdateService, CancellationToken ct)
        {
            try
            {
                await handleUpdateService.HandleUpdateAsync(bot, update, ct);
            }
            catch (Exception exception)
            {
                await handleUpdateService.HandleErrorAsync(bot, exception, Telegram.Bot.Polling.HandleErrorSource.HandleUpdateError, ct);
            }
            return Ok();
        }
    }
}
