using Newtonsoft.Json;

namespace HomeAutomation.Models.Mqtt
{
    public class PublishBody
    {
        [JsonProperty("topicName")]
        public string TopicName { get; set; }

        [JsonProperty("messageContent")]
        public string MessageContent { get; set; }
    }
}
