using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAutomation.Models.Data
{
    public class ActuationData
    {
        [JsonProperty("previousState")]
        public string PreviousState { get; set; }

        [JsonProperty("newState")]
        public string NewState { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }
    }
}
