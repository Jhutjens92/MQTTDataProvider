using MQTTDataProvider.ViewModel;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Text;
using uPLibrary.Networking.M2Mqtt.Messages;
using uPLibrary.Networking.M2Mqtt;
using Newtonsoft.Json.Serialization;
using MQTTDataProvider.Classes;

namespace MQTTDataProvider.MQTTManager
{
    class MqttManager
    {
        #region Instance Declaration
        static MqttClient Client;
        #endregion

        #region Vars
        // String containing the MQTT published message
        public string ReceivedMessage;

        // Default brokeraddress
        public class BrokerAddress
        {
            public string _brokerAddress { get; set; }
        }

        // JSON Parser MQTT message
        dynamic ParsedReceivedMessage;
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
            private string _textReceived;
            public string TextReceived
            {
                get
                {
                    return _textReceived;
                }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _textReceived = value;
                }
            }

            private string _imu1_AccX = "";
            public String IMU1_AccX
            {
                get { return _imu1_AccX; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _imu1_AccX = value;
                }
            }

            private string _imu1_AccY = "";
            public String IMU1_AccY
            {
                get { return _imu1_AccY; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _imu1_AccY = value;
                }
            }

            private string _imu1_AccZ = "";
            public String IMU1_AccZ
            {
                get { return _imu1_AccZ; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _imu1_AccZ = value;
                }
            }

            private string _imu1_GyroX = "";
            public String IMU1_GyroX
            {
                get { return _imu1_GyroX; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _imu1_GyroX = value;
                }
            }

            private string _imu1_GyroY = "";
            public String IMU1_GyroY
            {
                get { return _imu1_GyroY; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _imu1_GyroY = value;
                }
            }

            private string _imu1_GyroZ = "";
            public String IMU1_GyroZ
            {
                get { return _imu1_GyroZ; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _imu1_GyroZ = value;
                }
            }

            private string _imu1_MagX = "";
            public String IMU1_MagX
            {
                get { return _imu1_MagX; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _imu1_MagX = value;
                }
            }

            private string _imu1_MagY = "";
            public String IMU1_MagY
            {
                get { return _imu1_MagY; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _imu1_MagY = value;
                }
            }

            private string _imu1_MagZ = "";
            public String IMU1_MagZ
            {
                get { return _imu1_MagZ; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _imu1_MagZ = value;
                }
            }

            private string _imu1_Q0 = "";
            public String IMU1_Q0
            {
                get { return _imu1_Q0; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _imu1_Q0 = value;
                }
            }

            private string _imu1_Q1 = "";
            public String IMU1_Q1
            {
                get { return _imu1_Q1; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _imu1_Q1 = value;
                }
            }

            private string _imu1_Q2 = "";
            public String IMU1_Q2
            {
                get { return _imu1_Q2; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _imu1_Q2 = value;
                }
            }

            private string _imu1_Q3 = "";
            public String IMU1_Q3
            {
                get { return _imu1_Q3; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _imu1_Q3 = value;
                }
            }

            private string _imu2_AccX = "";
            public String IMU2_AccX
            {
                get { return _imu2_AccX; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _imu2_AccX = value;
                }
            }

            private string _imu2_AccY = "";
            public String IMU2_AccY
            {
                get { return _imu2_AccY; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _imu2_AccY = value;
                }
            }

            private string _imu2_AccZ = "";
            public String IMU2_AccZ
            {
                get { return _imu2_AccZ; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _imu2_AccZ = value;
                }
            }

            private string _imu2_GyroX = "";
            public String IMU2_GyroX
            {
                get { return _imu2_GyroX; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _imu2_GyroX = value;
                }
            }

            private string _imu2_GyroY = "";
            public String IMU2_GyroY
            {
                get { return _imu2_GyroY; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _imu2_GyroY = value;
                }
            }

            private string _imu2_GyroZ = "";
            public String IMU2_GyroZ
            {
                get { return _imu2_GyroZ; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _imu2_GyroZ = value;
                }
            }

            private string _imu2_MagX = "";
            public String IMU2_MagX
            {
                get { return _imu2_MagX; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _imu2_MagX = value;
                }
            }

            private string _imu2_MagY = "";
            public String IMU2_MagY
            {
                get { return _imu2_MagY; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _imu2_MagY = value;
                }
            }

            private string _imu2_MagZ = "";
            public String IMU2_MagZ
            {
                get { return _imu2_MagZ; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _imu2_MagZ = value;
                }
            }

            private string _imu2_Q0 = "";
            public String IMU2_Q0
            {
                get { return _imu2_Q0; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _imu2_Q0 = value;
                }
            }

            private string _imu2_Q1 = "";
            public String IMU2_Q1
            {
                get { return _imu2_Q1; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _imu2_Q1 = value;
                }
            }

            private string _imu2_Q2 = "";
            public String IMU2_Q2
            {
                get { return _imu2_Q2; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _imu2_Q2 = value;
                }
            }

            private string _imu2_Q3 = "";
            public String IMU2_Q3
            {
                get { return _imu2_Q3; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _imu2_Q3 = value;
                }
            }

            private string _tempExternal = "";
            public String TempExternal
            {
                get { return _tempExternal; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _tempExternal = value;
                }
            }

            private string _humExternal = "";
            public String HumExternal
            {
                get { return _humExternal; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _humExternal = value;
                }
            }

            private string _tempInternal = "";
            public String TempInternal
            {
                get { return _tempInternal; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _tempInternal = value;
                }
            }

            private string _humInternal = "";
            public String HumInternal
            {
                get { return _humInternal; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _humInternal = value;
                }
            }

            private string _pulse = "";
            public String Pulse
            {
                get { return _pulse; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _pulse = value;
                }
            }

            private string _gsr = "";
            public String GSR
            {
                get { return _gsr; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _gsr = value;
                }
            }

            private string _espTimeStamp = "";
            public String ESPTimeStamp
            {
                get { return _espTimeStamp; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _espTimeStamp = value;
                }
            }
        }
        #endregion

        #region Constructor
        // Constructor
        public void StartMqttClient()
        {
            SetLHDescriptions.SetDescriptions();
            CreateMqttClient();
            ConnectMqttClient();
            // register a callback-function (we have to implement, see below) which is called by the library when a message was received
            Client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;
            Subscribe_Default();
        }
        #endregion

        #region Methods

        public void CreateMqttClient()
        {
            var sba = new BrokerAddress();
            Client = new MqttClient(sba._brokerAddress);
        }

        public void ConnectMqttClient()
        {
            string ClientId;
            ClientId = Guid.NewGuid().ToString();
            Client.Connect(ClientId);
        }

        // Closes the MQTT connection when the program stops
        public static void CloseMqttConnection()
        {
            Client.Disconnect();
        }

        // Executes when a MQTT message was received
        private void Client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            ReceivedMessage = Encoding.UTF8.GetString(e.Message);
            if (Globals.isRecordingMqtt == true)
            {
                JSONParseReceivedMessage();
                UpdateValues();
            }
        }

        // Send the data from ESP to the VTT Player using MQTT/QOS 1
        private void PublishData(TextReceivedEventArgs e)
        {
            Client.Publish("wekit/vest/GSR_Raw", Encoding.UTF8.GetBytes(e.GSR), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
            Client.Publish("wekit/vest/Pulse_Raw", Encoding.UTF8.GetBytes(e.Pulse), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
            Client.Publish("wekit/vest/Sht0_Temp", Encoding.UTF8.GetBytes(e.TempExternal), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
            Client.Publish("wekit/vest/Sht0_Hum", Encoding.UTF8.GetBytes(e.HumExternal), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
            Client.Publish("wekit/vest/Sht1_Temp", Encoding.UTF8.GetBytes(e.TempInternal), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
            Client.Publish("wekit/vest/Sht1_Hum", Encoding.UTF8.GetBytes(e.HumInternal), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
        }

        // Parse MQTT JSON String
        private void JSONParseReceivedMessage()
        {
            ParsedReceivedMessage = JObject.Parse(ReceivedMessage);
        }

        // Subscribes to the default WEKIT Topic ("wekit/vest")
        private void Subscribe_Default()
        {
            // Default topic value for WEKIT
            string topicSubscribe = "wekit/vest";
            Client.Subscribe(new string[] { topicSubscribe }, new byte[] { 1 });
        }

        // Sets all the variables to the received values
        private void UpdateValues()
        {

            try
            {
                TextReceivedEventArgs args = new TextReceivedEventArgs
                {
                    TextReceived = ReceivedMessage,
                    ESPTimeStamp = ParsedReceivedMessage.time,
                    IMU1_AccX = ParsedReceivedMessage.imus[0].ax,
                    IMU1_AccY = ParsedReceivedMessage.imus[0].ay,
                    IMU1_AccZ = ParsedReceivedMessage.imus[0].az,
                    IMU1_GyroX = ParsedReceivedMessage.imus[0].gx,
                    IMU1_GyroY = ParsedReceivedMessage.imus[0].gy,
                    IMU1_GyroZ = ParsedReceivedMessage.imus[0].gz,
                    IMU1_MagX = ParsedReceivedMessage.imus[0].mx,
                    IMU1_MagY = ParsedReceivedMessage.imus[0].my,
                    IMU1_MagZ = ParsedReceivedMessage.imus[0].mz,
                    IMU1_Q0 = ParsedReceivedMessage.imus[0].q0,
                    IMU1_Q1 = ParsedReceivedMessage.imus[0].q1,
                    IMU1_Q2 = ParsedReceivedMessage.imus[0].q2,
                    IMU1_Q3 = ParsedReceivedMessage.imus[0].q3,
                    IMU2_AccX = ParsedReceivedMessage.imus[1].ax,
                    IMU2_AccY = ParsedReceivedMessage.imus[1].ay,
                    IMU2_AccZ = ParsedReceivedMessage.imus[1].az,
                    IMU2_GyroX = ParsedReceivedMessage.imus[1].gx,
                    IMU2_GyroY = ParsedReceivedMessage.imus[1].gy,
                    IMU2_GyroZ = ParsedReceivedMessage.imus[1].gz,
                    IMU2_MagX = ParsedReceivedMessage.imus[1].mx,
                    IMU2_MagY = ParsedReceivedMessage.imus[1].my,
                    IMU2_MagZ = ParsedReceivedMessage.imus[1].mz,
                    IMU2_Q0 = ParsedReceivedMessage.imus[1].q0,
                    IMU2_Q1 = ParsedReceivedMessage.imus[1].q1,
                    IMU2_Q2 = ParsedReceivedMessage.imus[1].q2,
                    IMU2_Q3 = ParsedReceivedMessage.imus[1].q3,
                    TempExternal = ParsedReceivedMessage.shts[0].temp,
                    HumExternal = ParsedReceivedMessage.shts[0].hum,
                    TempInternal = ParsedReceivedMessage.shts[1].temp,
                    HumInternal = ParsedReceivedMessage.shts[1].hum,
                    Pulse = ParsedReceivedMessage.pulse,
                    GSR = ParsedReceivedMessage.gsr
                };
                OnNewTextReceived(args);
                PublishData(args);
            }
            catch (Exception)
            {
                TextReceivedEventArgs args = new TextReceivedEventArgs
                {
                    TextReceived = "Invalid JSON message at the MQTT Receiver"
                };
                Globals.jsonErrorMessage = true;
                OnNewTextReceived(args);
            }

        }
        #endregion
    }
}