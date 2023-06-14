using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAutomation.Models.Responses
{
    public class MessageInfo
    {
        [JsonProperty("chat_id")]
        public string ChatId { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
