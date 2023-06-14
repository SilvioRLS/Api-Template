using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeAutomation.Facades.Interfaces;
using HomeAutomation.Models.Data;
using HomeAutomation.Models.Mqtt;
using HomeAutomation.Models.Responses;
using HomeAutomation.Services;
using Microsoft.AspNetCore.Mvc;
using MQTTnet.Client.Publishing;

namespace HomeAutomation.Facades
{
    public class MqttFacade : IMqttFacade
    {
        public async Task<MqttClientPublishResult> PublishToTopic(PublishBody body)
        {
            var response = await MqttService.MakePublish(body.MessageContent, body.TopicName);

            return response;
        }

        public async Task SubiscribeToTopic(string topicName)
        {
            await MqttService.SubscribeToTopic(topicName);
        }

        public async Task<HouseData> FullDataLog()
        {
            return await MqttService.GetHouseData();
        }


        public async Task<ActuationRecordData> FullActuationRecordLog()
        {
            return await MqttService.GetActuationRecordDataAsync();
        }

        public async Task FlushDataLog()
        {
            await MqttService.FlushDataLog();
        }
    }
}
