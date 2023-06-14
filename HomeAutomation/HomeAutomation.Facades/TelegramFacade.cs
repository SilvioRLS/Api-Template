using HomeAutomation.Facades.Interfaces;
using HomeAutomation.Models.Responses;
using HomeAutomation.Services.Interfaces;
using System.Threading.Tasks;

namespace HomeAutomation.Facades
{
    public class TelegramFacade : ITelegramFacade
    {
        private const string BOT_TOKE = "PLACE-HOLDER";
        private readonly ITelegramService _telegramService;

        public TelegramFacade(ITelegramService telegramService)
        {            
            _telegramService = telegramService;
            _telegramService.BotToke = BOT_TOKE;
        }

        public async Task<object> SendMessage(MessageInfo messageInfo)
        {
            return await _telegramService.SendMessageAsync(messageInfo);
        }
    }
}
