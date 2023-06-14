using Newtonsoft.Json;

namespace HomeAutomation.Models.Data
{
    public class HouseData
    {
        [JsonProperty("umidadeH")]
        public DataObject UmidadeH { get; set; }

        [JsonProperty("dateLastIrrigationH")]
        public DataObject DateLastIrrigationH { get; set; }

        [JsonProperty("temperatureCI")]
        public DataObject TemperatureCI { get; set; }

        [JsonProperty("timeUpdateC")]
        public DataObject TimeUpdateC { get; set; }

        [JsonProperty("targetTemp")]
        public DataObject TargetTemp { get; set; }

        [JsonProperty("fanSpeedCI")]
        public DataObject FanSpeedCI { get; set; }

        [JsonProperty("fanSpeedCO")]
        public DataObject FanSpeddCO { get; set; }

        [JsonProperty("stateP")]
        public DataObject StateP { get; set; }

        [JsonProperty("offSet")]
        public DataObject OffSet { get; set; }

        [JsonProperty("offSetConfig")]
        public DataObject OffSetConfig { get; set; }

        [JsonProperty("actuationTime")]
        public DataObject ActuationTime { get; set; }

        [JsonProperty("dateLastActionP")]
        public DataObject DateLastActionP { get; set; }

    }
}
