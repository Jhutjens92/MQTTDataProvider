﻿using MQTTDataProvider.ViewModel;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Diagnostics;
using System.Text;
using uPLibrary.Networking.M2Mqtt.Messages;
using uPLibrary.Networking.M2Mqtt;

namespace MQTTDataProvider.MQTTManager
{
    class MqttDataManager
    {
        #region Vars
        // String containing the MQTT published message
        string ReceivedMessage;

        // JSON Parser MQTT message
        dynamic Parsed_ReceivedMessage;

        // Default topic value for WEKIT
        readonly string Topic_Subscribe = "wekit/vest";

        // Default brokeraddress
        string BrokerAddress = "localhost";
        #endregion

        #region Instance Declaration
        MqttClient Client;
        #endregion

        #region Events
        // handler for subscribing classes where you do +=
        public event EventHandler<TextReceivedEventArgs> NewMqttTextReceived;

        // this is for raising the event in the class
        protected virtual void OnNewTextReceived(TextReceivedEventArgs UpdateValuesEvent)
        {
            NewMqttTextReceived?.Invoke(this, UpdateValuesEvent);
        }
        //inherits from event args which holds all the values that needs to be passed as args in the event
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
        #endregion

        #region Constructor
        // Constructor
        public MqttDataManager() 
        {
            string ClientId;
            SetParameters();
            Client = new MqttClient(BrokerAddress);
            ClientId = Guid.NewGuid().ToString();
            // register a callback-function (we have to implement, see below) which is called by the library when a message was received
            Client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;
            Client.Connect(ClientId);
            Subscribe_Default();
        }
        #endregion

        #region Methods
        // Executes when a MQTT message was received
        private void Client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            ReceivedMessage = Encoding.UTF8.GetString(e.Message);
            if (Globals.IsRecordingMqtt == true)
            {
                JSONParse_ReceivedMessage();
                UpdateValues();
            }
        }

        // Send the data from ESP to the VTT Player using MQTT/QOS 1
        private void PublishData(TextReceivedEventArgs e)
        {
            Client.Publish("wekit/vest/GSR_Raw", Encoding.UTF8.GetBytes(e.GSR), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
            Client.Publish("wekit/vest/Pulse_Raw", Encoding.UTF8.GetBytes(e.Pulse_TempLobe), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
            Client.Publish("wekit/vest/Sht0_Temp", Encoding.UTF8.GetBytes(e.Temp_Ext), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
            Client.Publish("wekit/vest/Sht0_Hum", Encoding.UTF8.GetBytes(e.Humidity_Ext), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
            Client.Publish("wekit/vest/Sht1_Temp", Encoding.UTF8.GetBytes(e.Temp_Int), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
            Client.Publish("wekit/vest/Sht1_Hum", Encoding.UTF8.GetBytes(e.Humidity_Int), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
        }

        // Checks the startup parameters
        private void SetParameters()
        {
            string[] Parameters = Environment.GetCommandLineArgs();
            if (Parameters.Any(s => s.Contains("-ba")))
            {
                int parameterIndex = Array.IndexOf(Parameters, "-ba");
                BrokerAddress = Parameters[parameterIndex + 1];
            }
            else
            {
                Console.WriteLine("No valid paramater provided, starting with default values.");
            }
        }

        // Parse MQTT JSON String
        private void JSONParse_ReceivedMessage()
        {
            Parsed_ReceivedMessage = JObject.Parse(ReceivedMessage);
        }

        // Subscribes to the default WEKIT Topic ("wekit/vest")
        private void Subscribe_Default()
        {
            Client.Subscribe(new string[] { Topic_Subscribe }, new byte[] { 1 });
        }

        // Sets all the variables to the received values
        private void UpdateValues()
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
            PublishData(args);
        }
        #endregion
    }
}