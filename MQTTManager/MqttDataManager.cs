﻿using MQTTDataProvider.ViewModel;
using Newtonsoft.Json.Linq;
using System;
using System.Text;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace MQTTDataProvider.MQTTManager
{
    class MqttDataManager
    {
        #region Vars
        //string containing the MQTT published message
        string ReceivedMessage;

        //JSON Parser MQTT message
        dynamic Parsed_ReceivedMessage;

        //default topic value for WEKIT
        readonly string Topic_Subscribe = "wekit/vest";
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
        //Constructor
        public MqttDataManager() 
        {
            // register a callback-function (we have to implement, see below) which is called by the library when a message was received
            Globals.Client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;

            Subscribe_Default();
        }
        #endregion
        
        #region Methods
        // this function executes when a MQTT message was received
        private void Client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            ReceivedMessage = Encoding.UTF8.GetString(e.Message);
            if (Globals.IsRecordingMqtt == true)
            {
                JSONParse_ReceivedMessage();
                UpdateValues();
            }
        }
        
        // this function is used to parse MQTT JSON String
        private void JSONParse_ReceivedMessage()
        {
            Parsed_ReceivedMessage = JObject.Parse(ReceivedMessage);
        }

        // this function subscribes to the default WEKIT Topic ("wekit/vest")
        private void Subscribe_Default()
        {
            Globals.Client.Subscribe(new string[] { Topic_Subscribe }, new byte[] { 1 });
        }

        // this function sets all the variables to the received values
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
        #endregion
    }
}