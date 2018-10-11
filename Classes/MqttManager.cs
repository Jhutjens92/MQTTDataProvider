using MQTTDataProvider.ViewModel;
using System;
using System.Text;
using uPLibrary.Networking.M2Mqtt.Messages;
using uPLibrary.Networking.M2Mqtt;


namespace MQTTDataProvider.Classes
{
    class MqttManager
    {
        #region Instance Declaration
        static MqttClient Client;
        #endregion

        #region Vars
        // String containing the MQTT published message
        public static string ReceivedMessage;

        // Default brokeraddress
        public static string BrokerAddress = "localhost";

        // Default topic value for WEKIT
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
            private string _TextReceived;
            public string TextReceived
            {
                get
                {
                    return _TextReceived;
                }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _TextReceived = value;
                }
            }

            private string _IMU1_AccX = "";
            public String IMU1_AccX
            {
                get { return _IMU1_AccX; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _IMU1_AccX = value;
                }
            }

            private string _IMU1_AccY = "";
            public String IMU1_AccY
            {
                get { return _IMU1_AccY; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _IMU1_AccY = value;
                }
            }

            private string _IMU1_AccZ = "";
            public String IMU1_AccZ
            {
                get { return _IMU1_AccZ; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _IMU1_AccZ = value;
                }
            }

            private string _IMU1_GyroX = "";
            public String IMU1_GyroX
            {
                get { return _IMU1_GyroX; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _IMU1_GyroX = value;
                }
            }

            private string _IMU1_GyroY = "";
            public String IMU1_GyroY
            {
                get { return _IMU1_GyroY; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _IMU1_GyroY = value;
                }
            }

            private string _IMU1_GyroZ = "";
            public String IMU1_GyroZ
            {
                get { return _IMU1_GyroZ; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _IMU1_GyroZ = value;
                }
            }

            private string _IMU1_MagX = "";
            public String IMU1_MagX
            {
                get { return _IMU1_MagX; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _IMU1_MagX = value;
                }
            }

            private string _IMU1_MagY = "";
            public String IMU1_MagY
            {
                get { return _IMU1_MagY; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _IMU1_MagY = value;
                }
            }

            private string _IMU1_MagZ = "";
            public String IMU1_MagZ
            {
                get { return _IMU1_MagZ; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _IMU1_MagZ = value;
                }
            }

            private string _IMU1_Q0 = "";
            public String IMU1_Q0
            {
                get { return _IMU1_Q0; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _IMU1_Q0 = value;
                }
            }

            private string _IMU1_Q1 = "";
            public String IMU1_Q1
            {
                get { return _IMU1_Q1; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _IMU1_Q1 = value;
                }
            }

            private string _IMU1_Q2 = "";
            public String IMU1_Q2
            {
                get { return _IMU1_Q2; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _IMU1_Q2 = value;
                }
            }

            private string _IMU1_Q3 = "";
            public String IMU1_Q3
            {
                get { return _IMU1_Q3; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _IMU1_Q3 = value;
                }
            }

            private string _IMU2_AccX = "";
            public String IMU2_AccX
            {
                get { return _IMU2_AccX; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _IMU2_AccX = value;
                }
            }

            private string _IMU2_AccY = "";
            public String IMU2_AccY
            {
                get { return _IMU2_AccY; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _IMU2_AccY = value;
                }
            }

            private string _IMU2_AccZ = "";
            public String IMU2_AccZ
            {
                get { return _IMU2_AccZ; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _IMU2_AccZ = value;
                }
            }

            private string _IMU2_GyroX = "";
            public String IMU2_GyroX
            {
                get { return _IMU2_GyroX; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _IMU2_GyroX = value;
                }
            }

            private string _IMU2_GyroY = "";
            public String IMU2_GyroY
            {
                get { return _IMU2_GyroY; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _IMU2_GyroY = value;
                }
            }

            private string _IMU2_GyroZ = "";
            public String IMU2_GyroZ
            {
                get { return _IMU2_GyroZ; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _IMU2_GyroZ = value;
                }
            }

            private string _IMU2_MagX = "";
            public String IMU2_MagX
            {
                get { return _IMU2_MagX; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _IMU2_MagX = value;
                }
            }

            private string _IMU2_MagY = "";
            public String IMU2_MagY
            {
                get { return _IMU2_MagY; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _IMU2_MagY = value;
                }
            }

            private string _IMU2_MagZ = "";
            public String IMU2_MagZ
            {
                get { return _IMU2_MagZ; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _IMU2_MagZ = value;
                 }
            }

            private string _IMU2_Q0 = "";
            public String IMU2_Q0
            {
                get { return _IMU2_Q0; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _IMU2_Q0 = value;
                }
            }

            private string _IMU2_Q1 = "";
            public String IMU2_Q1
            {
                get { return _IMU2_Q1; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _IMU2_Q1 = value;
                }
            }

            private string _IMU2_Q2 = "";
            public String IMU2_Q2
            {
                get { return _IMU2_Q2; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _IMU2_Q2 = value;
                }
            }

            private string _IMU2_Q3 = "";
            public String IMU2_Q3
            {
                get { return _IMU2_Q3; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _IMU2_Q3 = value;
                }
            }

            private string _Temp_External = "";
            public String Temp_External
            {
                get { return _Temp_External; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _Temp_External = value;
                }
            }

            private string _Humidity_External = "";
            public String Humidity_External
            {
                get { return _Humidity_External; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _Humidity_External = value;
                }
            }

            private string _Temp_Internal = "";
            public String Temp_Internal
            {
                get { return _Temp_Internal; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _Temp_Internal = value;
                }
            }

            private string _Humidity_Internal = "";
            public String Humidity_Internal
            {
                get { return _Humidity_Internal; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _Humidity_Internal = value;
                }
            }

            private string _Pulse_TempLobe = "";
            public String Pulse_TempLobe
            {
                get { return _Pulse_TempLobe; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _Pulse_TempLobe = value;
                }
            }

            private string _GSR = "";
            public String GSR
            {
                get { return _GSR; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _GSR = value;
                }
            }

            private string _ESP_TimeStamp = "";
            public String ESP_TimeStamp
            {
                get { return _ESP_TimeStamp; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    _ESP_TimeStamp = value;
                }
            }
        }
        #endregion

        #region Constructor
        // Constructor
        public MqttManager() 
        {
            string ClientId;
            ParameterSet.SetParameters();
            Client = new MqttClient(BrokerAddress);
            ClientId = Guid.NewGuid().ToString();
            // register a callback-function (we have to implement, see below) which is called by the library when a message was received
            Client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;
            Client.Connect(ClientId);
            Subscribe_Default();
        }
        #endregion

        #region Methods
        // Closes the MQTT connection when the program stops
        public static void CloseConnection()
        {
            Client.Disconnect();
        }

        // Executes when a MQTT message was received
        private void Client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            ReceivedMessage = Encoding.UTF8.GetString(e.Message);
            if (Globals.IsRecordingMqtt == true)
            {
                JsonParser.JSONParseReceivedMessage();
                UpdateValues();
            }
        }

        // Send the data from ESP to the VTT Player using MQTT/QOS 1
        private void PublishData(TextReceivedEventArgs e)
        {
            Client.Publish("wekit/vest/GSR_Raw", Encoding.UTF8.GetBytes(e.GSR), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
            Client.Publish("wekit/vest/Pulse_Raw", Encoding.UTF8.GetBytes(e.Pulse_TempLobe), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
            Client.Publish("wekit/vest/Sht0_Temp", Encoding.UTF8.GetBytes(e.Temp_External), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
            Client.Publish("wekit/vest/Sht0_Hum", Encoding.UTF8.GetBytes(e.Humidity_External), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
            Client.Publish("wekit/vest/Sht1_Temp", Encoding.UTF8.GetBytes(e.Temp_Internal), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
            Client.Publish("wekit/vest/Sht1_Hum", Encoding.UTF8.GetBytes(e.Humidity_Internal), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
        }

        // Subscribes to the default WEKIT Topic ("wekit/vest")
        private void Subscribe_Default()
        {
            Client.Subscribe(new string[] { Topic_Subscribe }, new byte[] { 1 });
        }

        // Sets all the variables to the received values
        private void UpdateValues()
        {

            try
            {
                TextReceivedEventArgs args = new TextReceivedEventArgs
                {
                    TextReceived = ReceivedMessage,
                    ESP_TimeStamp = JsonParser.ParsedReceivedMessage.time,
                    IMU1_AccX = JsonParser.ParsedReceivedMessage.imus[0].ax,
                    IMU1_AccY = JsonParser.ParsedReceivedMessage.imus[0].ay,
                    IMU1_AccZ = JsonParser.ParsedReceivedMessage.imus[0].az,
                    IMU1_GyroX = JsonParser.ParsedReceivedMessage.imus[0].gx,
                    IMU1_GyroY = JsonParser.ParsedReceivedMessage.imus[0].gy,
                    IMU1_GyroZ = JsonParser.ParsedReceivedMessage.imus[0].gz,
                    IMU1_MagX = JsonParser.ParsedReceivedMessage.imus[0].mx,
                    IMU1_MagY = JsonParser.ParsedReceivedMessage.imus[0].my,
                    IMU1_MagZ = JsonParser.ParsedReceivedMessage.imus[0].mz,
                    IMU1_Q0 = JsonParser.ParsedReceivedMessage.imus[0].q0,
                    IMU1_Q1 = JsonParser.ParsedReceivedMessage.imus[0].q1,
                    IMU1_Q2 = JsonParser.ParsedReceivedMessage.imus[0].q2,
                    IMU1_Q3 = JsonParser.ParsedReceivedMessage.imus[0].q3,
                    IMU2_AccX = JsonParser.ParsedReceivedMessage.imus[1].ax,
                    IMU2_AccY = JsonParser.ParsedReceivedMessage.imus[1].ay,
                    IMU2_AccZ = JsonParser.ParsedReceivedMessage.imus[1].az,
                    IMU2_GyroX = JsonParser.ParsedReceivedMessage.imus[1].gx,
                    IMU2_GyroY = JsonParser.ParsedReceivedMessage.imus[1].gy,
                    IMU2_GyroZ = JsonParser.ParsedReceivedMessage.imus[1].gz,
                    IMU2_MagX = JsonParser.ParsedReceivedMessage.imus[1].mx,
                    IMU2_MagY = JsonParser.ParsedReceivedMessage.imus[1].my,
                    IMU2_MagZ = JsonParser.ParsedReceivedMessage.imus[1].mz,
                    IMU2_Q0 = JsonParser.ParsedReceivedMessage.imus[1].q0,
                    IMU2_Q1 = JsonParser.ParsedReceivedMessage.imus[1].q1,
                    IMU2_Q2 = JsonParser.ParsedReceivedMessage.imus[1].q2,
                    IMU2_Q3 = JsonParser.ParsedReceivedMessage.imus[1].q3,
                    Temp_External = JsonParser.ParsedReceivedMessage.shts[0].temp,
                    Humidity_External = JsonParser.ParsedReceivedMessage.shts[0].hum,
                    Temp_Internal = JsonParser.ParsedReceivedMessage.shts[1].temp,
                    Humidity_Internal = JsonParser.ParsedReceivedMessage.shts[1].hum,
                    Pulse_TempLobe = JsonParser.ParsedReceivedMessage.pulse,
                    GSR = JsonParser.ParsedReceivedMessage.gsr
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
                Globals.JSONErrorMessage = true;
                OnNewTextReceived(args);
            }

        }
        #endregion
    }
}