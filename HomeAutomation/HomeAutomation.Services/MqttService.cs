using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HomeAutomation.Models.Data;
using HomeAutomation.Models.Responses;
using HomeAutomation.Models.UI;
using HomeAutomation.Services.Interfaces;
using MQTTnet;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;
using MQTTnet.Client.Publishing;
using MQTTnet.Client.Receiving;
using MQTTnet.Extensions.ManagedClient;
using MQTTnet.Formatter;
using MQTTnet.Protocol;
using RestEase;

namespace HomeAutomation.Services
{
    /*
        This class is not a good example of architeture, ignore this one. 
    */
    public static class MqttService
    {
        public static IManagedMqttClient managedMqttClientPublisher;

        public static IManagedMqttClient managedMqttClientSubscriber;

        public static ITelegramService _telegramService;

        public static long _lastAlertId;

        public static ActuationRecordData ActuationRecordData = new ActuationRecordData
        {
            actuationRecordDatas = new List<ActuationData>()
        };

        public static HouseData HouseData = new HouseData
        {
            FanSpeddCO = new DataObject()
            {
                Value = "N/A",
                LastUpdate = "N/A"
            },
            DateLastActionP = new DataObject()
            {
                Value = "N/A",
                LastUpdate = "N/A"
            },
            DateLastIrrigationH = new()
            {
                Value = "N/A",
                LastUpdate = "N/A"
            },
            FanSpeedCI = new()
            {
                Value = "N/A",
                LastUpdate = "N/A"
            },
            StateP = new()
            {
                Value = "N/A",
                LastUpdate = "N/A"
            },
            TargetTemp = new()
            {
                Value = "N/A",
                LastUpdate = "N/A"
            },
            TemperatureCI = new()
            {
                Value = "N/A",
                LastUpdate = "N/A"
            },
            UmidadeH = new()
            {
                Value = "N/A",
                LastUpdate = "N/A"
            },
            TimeUpdateC = new()
            {
                Value = "N/A",
                LastUpdate = "N/A"
            },
            OffSet = new()
            {
                Value = "N/A",
                LastUpdate = "N/A"
            },
            ActuationTime = new()
            {
                Value = "N/A",
                LastUpdate = "N/A"
            },
            OffSetConfig = new()
            {
                Value = "N/A",
                LastUpdate = "N/A"
            }
        };

        public static async void ConfigureMqtt(MqttSettings settings)
        {
            MqttFactory mqttFactory = new MqttFactory();

            MqttClientTlsOptions tlsOptions = new MqttClientTlsOptions
            {
                UseTls = false,
                IgnoreCertificateChainErrors = true,
                IgnoreCertificateRevocationErrors = true,
                AllowUntrustedCertificates = true
            };

            MqttClientOptions options = new MqttClientOptions
            {
                ClientId = "ClientHttpServer",
                ProtocolVersion = MqttProtocolVersion.V311,
                ChannelOptions = new MqttClientTcpOptions
                {
                    Server = "localhost",
                    Port = int.Parse(settings.MqttPort),
                    TlsOptions = tlsOptions
                }
            };

            if (options.ChannelOptions == null)
            {
                throw new InvalidOperationException();
            }

            options.Credentials = new MqttClientCredentials
            {
                Username = settings.BrokerUserName,
                Password = Encoding.UTF8.GetBytes(settings.BrokerPassword)
            };

            options.CleanSession = true;
            options.KeepAlivePeriod = TimeSpan.FromSeconds(5);

            managedMqttClientPublisher = mqttFactory.CreateManagedMqttClient();
            managedMqttClientPublisher.UseApplicationMessageReceivedHandler(handlePublish);
            managedMqttClientPublisher.ConnectedHandler = new MqttClientConnectedHandlerDelegate(onPublisherConnected);
            managedMqttClientPublisher.DisconnectedHandler = new MqttClientDisconnectedHandlerDelegate(onPublisherDisconnected);

            await managedMqttClientPublisher.StartAsync(
                new ManagedMqttClientOptions
                {
                    ClientOptions = options
                });


            managedMqttClientSubscriber = mqttFactory.CreateManagedMqttClient();
            managedMqttClientSubscriber.ConnectedHandler = new MqttClientConnectedHandlerDelegate(OnSubscriberConnected);
            managedMqttClientSubscriber.DisconnectedHandler = new MqttClientDisconnectedHandlerDelegate(OnSubscriberDisconnected);
            managedMqttClientSubscriber.ApplicationMessageReceivedHandler = new MqttApplicationMessageReceivedHandlerDelegate(OnSubscriberMessageReceived);

            await managedMqttClientSubscriber.StartAsync(
                new ManagedMqttClientOptions
                {
                    ClientOptions = options
                });
        }

        public static async Task SubscribeToTopic(string topicName)
        {
            try
            {
                MqttTopicFilter topicFilter = new MqttTopicFilter { Topic = topicName };
                await managedMqttClientSubscriber.SubscribeAsync(topicFilter);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message, "Error Occurs ; OnSubscribing");
            }
        }


        public static async Task<MqttClientPublishResult> MakePublish(string content, string topicName)
        {
            try
            {
                byte[] payload = Encoding.UTF8.GetBytes(content);
                MqttApplicationMessage message = new MqttApplicationMessageBuilder().WithTopic(topicName).WithPayload(payload).WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce).WithRetainFlag().Build();

                return await managedMqttClientPublisher.PublishAsync(message);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, "Error Occurs ; OnSubscription");
            }

            return null;
        }

        public static void handlePublish(MqttApplicationMessageReceivedEventArgs x)
        {

        }

        public static void onPublisherConnected(MqttClientConnectedEventArgs x)
        {

        }

        public static void onPublisherDisconnected(MqttClientDisconnectedEventArgs x)
        {

        }

        /// <summary>
        /// Handles the subscriber connected event.
        /// </summary>
        /// <param name="x">The MQTT client connected event args.</param>
        private static void OnSubscriberConnected(MqttClientConnectedEventArgs x)
        {
            
        }

        /// <summary>
        /// Handles the subscriber disconnected event.
        /// </summary>
        /// <param name="x">The MQTT client disconnected event args.</param>
        private static void OnSubscriberDisconnected(MqttClientDisconnectedEventArgs x)
        {
            
        }
        
        public static async Task<HouseData> GetHouseData()
        {
            return await Task.Run(() =>
            {
                return HouseData;
            });
        }

        public static async Task<ActuationRecordData> GetActuationRecordDataAsync()
        {
            return await Task.Run(() =>
            {
                return ActuationRecordData;
            });
        }

        public static async Task ProcessMqttAlertAsync(string[] words)
        {
            var status = words[1];
            var temp = words[2];
            var alertId = words[3];

            if(long.Parse(alertId) > _lastAlertId)
            {
                _lastAlertId = long.Parse(alertId);

                var msgInfo = new MessageInfo()
                {
                    Text = "Temperatura está " + status + " de " + temp,
                    ChatId = "440798487"

                };

                _telegramService = RestClient.For<ITelegramService>("https://api.telegram.org/");
                _telegramService.BotToke = "PLACE-HOLDER";
                await _telegramService.SendMessageAsync(msgInfo);
            }            
        }

        public static void UpdateActuationRecord(string[] words, string now)
        {
            var state = words[1];
            var prS = "false";
            if(state == "false")
            {
                prS = "true";
            }
            else if(state == "true")
            {
                prS = "false";
            }
            
            if(ActuationRecordData.actuationRecordDatas.Count >= 6)
            {
                ActuationRecordData.actuationRecordDatas.RemoveAt(ActuationRecordData.actuationRecordDatas.Count - 1);
                ActuationRecordData.actuationRecordDatas.Add(new ActuationData()
                {
                    Date = now,
                    NewState = state,
                    PreviousState = prS
                });
            }
            else
            {
                ActuationRecordData.actuationRecordDatas.Add(new ActuationData()
                {
                    Date = now,
                    NewState = state,
                    PreviousState = prS
                });
            }            
        }

        public static async Task FlushDataLog()
        {
            await Task.Run(() =>
            {
                HouseData = new HouseData()
                {
                    FanSpeddCO = new DataObject()
                    {
                        Value = "N/A",
                        LastUpdate = "N/A"
                    },
                    DateLastActionP = new DataObject()
                    {
                        Value = "N/A",
                        LastUpdate = "N/A"
                    },
                    DateLastIrrigationH = new()
                    {
                        Value = "N/A",
                        LastUpdate = "N/A"
                    },
                    FanSpeedCI = new()
                    {
                        Value = "N/A",
                        LastUpdate = "N/A"
                    },
                    StateP = new()
                    {
                        Value = "N/A",
                        LastUpdate = "N/A"
                    },
                    TargetTemp = new()
                    {
                        Value = "N/A",
                        LastUpdate = "N/A"
                    },
                    TemperatureCI = new()
                    {
                        Value = "N/A",
                        LastUpdate = "N/A"
                    },
                    UmidadeH = new()
                    {
                        Value = "N/A",
                        LastUpdate = "N/A"
                    },
                    TimeUpdateC = new()
                    {
                        Value = "N/A",
                        LastUpdate = "N/A"
                    },
                    OffSet = new()
                    {
                        Value = "N/A",
                        LastUpdate = "N/A"
                    },
                    ActuationTime = new()
                    {
                        Value = "N/A",
                        LastUpdate = "N/A"
                    },
                    OffSetConfig = new()
                    {
                        Value = "N/A",
                        LastUpdate = "N/A"
                    }
                };
            });
        }

        /// <summary>
        /// Handles the received subscriber message event.
        /// </summary>
        /// <param name="x">The MQTT application message received event args.</param>
        private static void OnSubscriberMessageReceived(MqttApplicationMessageReceivedEventArgs x)
        {
            var now = DateTime.Now.ToString(System.Globalization.CultureInfo.CurrentCulture);
            var messagePayload = x.ApplicationMessage.ConvertPayloadToString();


            string[] words = messagePayload.Split(";");

            if(words[0] == "records")
            {
                UpdateActuationRecord(words, now);
            }
            else if(words[0] == "alert")
            {
                _ = ProcessMqttAlertAsync(words);
            }
            else if (words.Length >= 2)
            {
                if (words[0] == "umidadeH")
                {
                    HouseData.UmidadeH.Value = words[1];
                    HouseData.UmidadeH.LastUpdate = now;
                }
                else if (words[0] == "dateLastIrrigationH")
                {
                    HouseData.DateLastIrrigationH.Value = words[1];
                    HouseData.DateLastIrrigationH.LastUpdate = now;
                }
                else if (words[0] == "temperatureCI")
                {
                    HouseData.TemperatureCI.Value = words[1];
                    HouseData.TemperatureCI.LastUpdate = now;
                }
                else if (words[0] == "targetTemp")
                {
                    HouseData.TargetTemp.Value = words[1];
                    HouseData.TargetTemp.LastUpdate = now;
                }
                else if (words[0] == "fanSpeedCI")
                {
                    HouseData.FanSpeedCI.Value = words[1];
                    HouseData.FanSpeedCI.LastUpdate = now;
                }
                else if (words[0] == "fanSpeedCO")
                {
                    HouseData.FanSpeddCO.Value = words[1];
                    HouseData.FanSpeddCO.LastUpdate = now;
                }
                else if (words[0] == "stateP")
                {
                    HouseData.StateP.Value = words[1];
                    HouseData.StateP.LastUpdate = now;
                }
                else if (words[0] == "dateLastActionP")
                {
                    HouseData.DateLastActionP.Value = words[1];
                    HouseData.DateLastActionP.LastUpdate = now;
                }
                else if (words[0] == "timeUpdateC")
                {
                    HouseData.TimeUpdateC.Value = words[1];
                    HouseData.TimeUpdateC.LastUpdate = now;
                }
                else if(words[0] == "setOffSetConfig")
                {
                    HouseData.OffSetConfig.Value = words[1];
                    HouseData.OffSetConfig.LastUpdate = now;
                }
                else if (words[0] == "actuationTime")
                {
                    HouseData.ActuationTime.Value = words[1];
                    HouseData.ActuationTime.LastUpdate = now;
                }
                else if (words[0] == "offSet")
                {
                    HouseData.OffSet.Value = words[1];
                    HouseData.OffSet.LastUpdate = now;
                }
            }
        }
    }
}
