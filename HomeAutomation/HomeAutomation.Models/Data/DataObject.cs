using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HomeAutomation.Models.Data
{
    public class DataObject
    {
        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("lastUpdate")]
        public string LastUpdate { get; set; }
    }
}
