using System.Threading.Tasks;
using HomeAutomation.Models.Data;
using HomeAutomation.Models.Mqtt;
using HomeAutomation.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using MQTTnet.Client.Publishing;

namespace HomeAutomation.Facades.Interfaces
{
    public interface IMqttFacade
    {
        /// <summary>
        /// Process the information to be published in a specific topic
        /// </summary>
        public Task<MqttClientPublishResult> PublishToTopic(PublishBody body);

        /// <summary>
        /// 
        /// </summary>
        public Task SubiscribeToTopic(string topicName);

        /// <summary>
        /// 
        /// </summary>
        public Task<HouseData> FullDataLog();

        /// <summary>
        /// 
        /// </summary>
        Task<ActuationRecordData> FullActuationRecordLog();

        /// <summary>
        /// 
        /// </summary>
        public Task FlushDataLog();
    }
}
