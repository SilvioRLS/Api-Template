using HomeAutomation.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAutomation.Facades.Interfaces
{
    public interface ITelegramFacade
    {
        /// <summary>
        /// Process the information to be send as a message to a specific chat id
        /// </summary>
        Task<object> SendMessage(MessageInfo messageInfo);
    }
}
