using HomeAutomation.Facades.Interfaces;
using HomeAutomation.Models.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace HomeAutomation.Controllers
{
    /// <summary>
    /// Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TelegramController : ControllerBase
    {
        private readonly ITelegramFacade _telegramFacade;

        /// <summary>
        /// Telegram Controller Constructor
        /// </summary>
        public TelegramController(ITelegramFacade telegramFacade)
        {
            _telegramFacade = telegramFacade;
        }


        /// <summary>
        /// Sends a message to a specific chat id; Id is informed in the body
        /// </summary>
        /// <param name="messageInfo">Cointais the information of the chat id as well as the message content</param>
        /// <returns>Returns a sucess or fail status, as well as the object returned by the telegram API</returns>
        /// <response code="200">Returns a sucess status, message has ben sent</response>
        /// <response code="400">Returs a fail status, chat id may be empty</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof())]// Add a type here after u model the response object from telegram api
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("send-message")]
        public async Task<IActionResult> TelegramSendMessagePost([FromBody] MessageInfo messageInfo)
        {
            if(string.IsNullOrEmpty(messageInfo.ChatId))
            {
                return BadRequest();
            }
            var messageResponse = await _telegramFacade.SendMessage(messageInfo);

            return Ok(messageResponse);
        }
    }
}
