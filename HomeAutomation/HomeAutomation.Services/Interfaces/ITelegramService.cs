using HomeAutomation.Models.Responses;
using RestEase;
using System.Threading.Tasks;

namespace HomeAutomation.Services.Interfaces
{
    public interface ITelegramService
    {
        [Path("botToken")]
        string BotToke { get; set; }

        [Post("bot{botToken}/sendMessage")]
        Task<object> SendMessageAsync([Body] MessageInfo messageInfo);
    }
}
