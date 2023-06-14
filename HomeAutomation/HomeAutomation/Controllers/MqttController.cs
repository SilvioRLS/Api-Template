using System.Threading.Tasks;
using HomeAutomation.Facades.Interfaces;
using HomeAutomation.Models.Mqtt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MQTTnet.Client.Publishing;

namespace HomeAutomation.Controllers
{
    /// <summary>
    /// Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MqttController : ControllerBase
    {
        private readonly IMqttFacade _mqttFacade;

        /// <summary>
        /// Telegram Controller Constructor
        /// </summary>
        public MqttController(IMqttFacade mqttFacade)
        {
            _mqttFacade = mqttFacade;
        }

        /// <summary>
        /// Publish a message to a specific topic
        /// </summary>
        /// <param name="publishBody">Cointais the information of the topic to be publish as well as the message content</param>
        /// <returns>Returns a sucess or fail status, as well as the object returned by the broker</returns>
        /// <response code="200">Returns a sucess status, message has ben sent</response>
        /// <response code="400">Returs a fail status, topic may be empty</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MqttClientPublishResult))]// Add a type here after u model the response object from telegram api
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("publish-to-topic")]
        public async Task<IActionResult> PostPublishToTopic([FromBody] PublishBody publishBody)
        {
            if (publishBody is null || string.IsNullOrWhiteSpace(publishBody.TopicName) || string.IsNullOrWhiteSpace(publishBody.MessageContent))
            {
                return BadRequest("Unable to get info. No request field can be null or empty");
            }

            var publishResponse = await _mqttFacade.PublishToTopic(publishBody);

            return Ok(publishResponse);
        }

        [HttpGet("subiscribe-to-topic")]
        public async Task<IActionResult> GetSubiscribeToTopic([FromQuery] string topicName)
        {
            if (string.IsNullOrWhiteSpace(topicName))
            {
                return BadRequest("Unable to get info. No request field can be null or empty");
            }

            await _mqttFacade.SubiscribeToTopic(topicName);

            return Ok();
        }

        [HttpGet("full-data-log")]
        public async Task<IActionResult> GetFullDataLog()
        {
            var fullLogResponse = await _mqttFacade.FullDataLog();
            return Ok(fullLogResponse);
        }

        [HttpGet("actuation-records-log")]
        public async Task<IActionResult> ActuationRecordsLog()
        {
            var fullActuationResponse = await _mqttFacade.FullActuationRecordLog();
            return Ok(fullActuationResponse);
        }

        [HttpGet("flush-data-log")]
        public async Task<IActionResult> GetFlushDataLog()
        {
            await _mqttFacade.FlushDataLog();
            return Ok();
        }
    }
}
