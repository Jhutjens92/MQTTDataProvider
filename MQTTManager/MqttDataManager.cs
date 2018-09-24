﻿using MQTTDataProvider.ViewModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace MQTTDataProvider.MQTTManager
{
    class MqttDataManager
    {

        MqttClient client;
        readonly string clientId;


        //string containing the MQTT published message
        string ReceivedMessage;

        //JSON Parser MQTT message
        dynamic Parsed_ReceivedMessage;

        //default topic value for WEKIT
        readonly string Topic_Subscribe;

        //default MQTT server value for WEKIT
        readonly string BrokerAddress;
                       
        public event EventHandler<TextReceivedEventArgs> NewMqttTextReceived;
        protected virtual void OnNewTextReceived(TextReceivedEventArgs UpdateValuesEvent)
        {
            NewMqttTextReceived?.Invoke(this, UpdateValuesEvent);
        }

        public class TextReceivedEventArgs : EventArgs
        {
            public string TextReceived { get; set; }
            public string IMU1_AccX { get; set; }
            public string IMU1_AccY { get; set; }
            public string IMU1_AccZ { get; set; }
            public string IMU1_GyroX { get; set; }
            public string IMU1_GyroY { get; set; }
            public string IMU1_GyroZ { get; set; }
            public string IMU1_MagX { get; set; }
            public string IMU1_MagY { get; set; }
            public string IMU1_MagZ { get; set; }
            public string IMU1_Q0 { get; set; }
            public string IMU1_Q1 { get; set; }
            public string IMU1_Q2 { get; set; }
            public string IMU1_Q3 { get; set; }
            public string IMU2_AccX { get; set; }
            public string IMU2_AccY { get; set; }
            public string IMU2_AccZ { get; set; }
            public string IMU2_GyroX { get; set; }
            public string IMU2_GyroY { get; set; }
            public string IMU2_GyroZ { get; set; }
            public string IMU2_MagX { get; set; }
            public string IMU2_MagY { get; set; }
            public string IMU2_MagZ { get; set; }
            public string IMU2_Q0 { get; set; } 
            public string IMU2_Q1 { get; set; }
            public string IMU2_Q2 { get; set; }
            public string IMU2_Q3 { get; set; }
            public string Temp_Ext { get; set; }
            public string Humidity_Ext { get; set; }
            public string Temp_Int { get; set; }
            public string Humidity_Int { get; set; }
            public string Pulse_TempLobe { get; set; }
            public string GSR { get; set; }
        }


        public MqttDataManager()
        {
            //INIT Var Values//
            BrokerAddress = "localhost";
            Topic_Subscribe = "wekit/vest";
            //MQTT Functions//
            client = new MqttClient(BrokerAddress);
            // register a callback-function (we have to implement, see below) which is called by the library when a message was received
            client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;
            // use a unique id as client id, each time we start the application
            clientId = Guid.NewGuid().ToString();
            client.Connect(clientId);
            Subscribe_Default();
        }


        #region Methods
        // this code runs when a message was received
        void Client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {

            ReceivedMessage = Encoding.UTF8.GetString(e.Message);

            if (Globals.IsRecordingMqtt == true)
            {
                JSONParse_ReceivedMessage();
                Publish_Data();
                UpdateValues();

            }

        }

        void UpdateValues()
        {
            TextReceivedEventArgs args = new TextReceivedEventArgs
            {
                TextReceived = ReceivedMessage,
                IMU1_AccX = Parsed_ReceivedMessage.imus[0].ax,
                IMU1_AccY = Parsed_ReceivedMessage.imus[0].ay,
                IMU1_AccZ = Parsed_ReceivedMessage.imus[0].az,
                IMU1_GyroX = Parsed_ReceivedMessage.imus[0].gx,
                IMU1_GyroY = Parsed_ReceivedMessage.imus[0].gy,
                IMU1_GyroZ = Parsed_ReceivedMessage.imus[0].gz,
                IMU1_MagX = Parsed_ReceivedMessage.imus[0].mx,
                IMU1_MagY = Parsed_ReceivedMessage.imus[0].my,
                IMU1_MagZ = Parsed_ReceivedMessage.imus[0].mz,
                IMU1_Q0 = Parsed_ReceivedMessage.imus[0].q0,
                IMU1_Q1 = Parsed_ReceivedMessage.imus[0].q1,
                IMU1_Q2 = Parsed_ReceivedMessage.imus[0].q2,
                IMU1_Q3 = Parsed_ReceivedMessage.imus[0].q3,
                IMU2_AccX = Parsed_ReceivedMessage.imus[1].ax,
                IMU2_AccY = Parsed_ReceivedMessage.imus[1].ay,
                IMU2_AccZ = Parsed_ReceivedMessage.imus[1].az,
                IMU2_GyroX = Parsed_ReceivedMessage.imus[1].gx,
                IMU2_GyroY = Parsed_ReceivedMessage.imus[1].gy,
                IMU2_GyroZ = Parsed_ReceivedMessage.imus[1].gz,
                IMU2_MagX = Parsed_ReceivedMessage.imus[1].mx,
                IMU2_MagY = Parsed_ReceivedMessage.imus[1].my,
                IMU2_MagZ = Parsed_ReceivedMessage.imus[1].mz,
                IMU2_Q0 = Parsed_ReceivedMessage.imus[1].q0,
                IMU2_Q1 = Parsed_ReceivedMessage.imus[1].q1,
                IMU2_Q2 = Parsed_ReceivedMessage.imus[1].q2,
                IMU2_Q3 = Parsed_ReceivedMessage.imus[1].q3,
                Temp_Ext = Parsed_ReceivedMessage.shts[0].temp,
                Humidity_Ext = Parsed_ReceivedMessage.shts[0].hum,
                Temp_Int = Parsed_ReceivedMessage.shts[1].temp,
                Humidity_Int = Parsed_ReceivedMessage.shts[1].hum,
                Pulse_TempLobe = Parsed_ReceivedMessage.pulse,
                GSR = Parsed_ReceivedMessage.gsr
        };
            OnNewTextReceived(args);
        }

        //parse MQTT JSON String
        void JSONParse_ReceivedMessage()
        {
            Parsed_ReceivedMessage = JObject.Parse(ReceivedMessage);
        }

        #endregion

        #region MQTT

        private void Subscribe_Default()
        {
            // subscribe to the topic with QoS 2
            client.Subscribe(new string[] { Topic_Subscribe }, new byte[] { 2 });   // we need arrays as parameters because we can subscribe to different topics with one call
        }

        // this code runs when data is published to the subscribed topic
        private void Publish_Data()
        {
            // whole topic
            string GSR_Value = Parsed_ReceivedMessage.gsr;
            client.Publish("wekit/vest/GSR_Raw", Encoding.UTF8.GetBytes(GSR_Value), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);

            string Pulse_Value = Parsed_ReceivedMessage.pulse;
            client.Publish("wekit/vest/Pulse_Raw", Encoding.UTF8.GetBytes(Pulse_Value), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);

            string SHT1X1_Temp_Value = Parsed_ReceivedMessage.shts[0].temp;
            client.Publish("wekit/vest/Sht0_Temp", Encoding.UTF8.GetBytes(SHT1X1_Temp_Value), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);

            string SHT1X1_Hum_Value = Parsed_ReceivedMessage.shts[0].hum;
            client.Publish("wekit/vest/Sht0_Hum", Encoding.UTF8.GetBytes(SHT1X1_Hum_Value), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);

            string SHT2X2_Temp_Value = Parsed_ReceivedMessage.shts[1].temp;
            client.Publish("wekit/vest/Sht2_Temp", Encoding.UTF8.GetBytes(SHT2X2_Temp_Value), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);

            string SHT2X2_Hum_Value = Parsed_ReceivedMessage.shts[1].hum;
            client.Publish("wekit/vest/Sht2_Hum", Encoding.UTF8.GetBytes(SHT2X2_Hum_Value), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
        }

        #endregion
    }
}