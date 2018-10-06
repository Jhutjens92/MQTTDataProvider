﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;

namespace MQTTDataProvider.ViewModel
{
    public static class Globals
    {
        private static bool _isRecordingMqtt = false;
        public static bool IsRecordingMqtt
        {
            get { return _isRecordingMqtt; }
            set
            {
                _isRecordingMqtt = value;
            }
        }

        //default MQTT server value for WEKIT
        private static string _brokerAddress = "";
        public static string BrokerAddress
        {
            get { return _brokerAddress; }
            set
            {
                _brokerAddress = value;
            }
        }

        // use a unique id as client id
        private static readonly string _cliendID = Guid.NewGuid().ToString();
        public static string ClientID
        {
            get { return _cliendID; }
        }

        private static MqttClient _client = new MqttClient(BrokerAddress);
        public static MqttClient Client
        {
            get {
                if (_client.IsConnected == false)
                {
                    _client.Connect(ClientID);
                }
                    return _client; }
        }

    }
}