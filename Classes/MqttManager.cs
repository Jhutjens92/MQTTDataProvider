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
        public static string receivedMessage;

        // Default brokeraddress
        public static string brokerAddress = "localhost";

        // Default topic value for WEKIT
        readonly string topicSubscribe = "wekit/vest";

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
            public string textReceived
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
            public String imu1_AccX
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
            public String imu1_AccY
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
            public String imu1_AccZ
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
            public String imu1_GyroX
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
            public String imu1_GyroY
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
            public String imu1_GyroZ
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
            public String imu1_MagX
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
            public String imu1_MagY
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
            public String imu1_MagZ
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
            public String imu1_Q0
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
            public String imu1_Q1
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
            public String imu1_Q2
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
            public String imu1_Q3
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
            public String imu2_AccX
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
            public String imu2_AccY
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
            public String imu2_AccZ
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
            public String imu2_GyroX
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
            public String imu2_GyroY
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
            public String imu2_GyroZ
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
            public String imu2_MagX
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
            public String imu2_MagY
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
            public String imu2_MagZ
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
            public String imu2_Q0
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
            public String imu2_Q1
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
            public String imu2_Q2
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
            public String imu2_Q3
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
            public String tempExternal
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
            public String humExternal
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
            public String tempInternal
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
            public String humInternal
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
            public String pulse
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
            public String gsr
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
            public String espTimeStamp
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
        public MqttManager() 
        {
            string ClientId;
            ParameterSet.SetParameters();
            Client = new MqttClient(brokerAddress);
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
            receivedMessage = Encoding.UTF8.GetString(e.Message);
            if (Globals.isRecordingMqtt == true)
            {
                JsonParser.JSONParseReceivedMessage();
                UpdateValues();
            }
        }

        // Send the data from ESP to the VTT Player using MQTT/QOS 1
        private void PublishData(TextReceivedEventArgs e)
        {
            Client.Publish("wekit/vest/GSR_Raw", Encoding.UTF8.GetBytes(e.gsr), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
            Client.Publish("wekit/vest/Pulse_Raw", Encoding.UTF8.GetBytes(e.pulse), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
            Client.Publish("wekit/vest/Sht0_Temp", Encoding.UTF8.GetBytes(e.tempExternal), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
            Client.Publish("wekit/vest/Sht0_Hum", Encoding.UTF8.GetBytes(e.humExternal), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
            Client.Publish("wekit/vest/Sht1_Temp", Encoding.UTF8.GetBytes(e.tempInternal), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
            Client.Publish("wekit/vest/Sht1_Hum", Encoding.UTF8.GetBytes(e.humInternal), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
        }

        // Subscribes to the default WEKIT Topic ("wekit/vest")
        private void Subscribe_Default()
        {
            Client.Subscribe(new string[] { topicSubscribe }, new byte[] { 1 });
        }

        // Sets all the variables to the received values
        private void UpdateValues()
        {

            try
            {
                TextReceivedEventArgs args = new TextReceivedEventArgs
                {
                    textReceived = receivedMessage,
                    espTimeStamp = JsonParser.parsedReceivedMessage.time,
                    imu1_AccX = JsonParser.parsedReceivedMessage.imus[0].ax,
                    imu1_AccY = JsonParser.parsedReceivedMessage.imus[0].ay,
                    imu1_AccZ = JsonParser.parsedReceivedMessage.imus[0].az,
                    imu1_GyroX = JsonParser.parsedReceivedMessage.imus[0].gx,
                    imu1_GyroY = JsonParser.parsedReceivedMessage.imus[0].gy,
                    imu1_GyroZ = JsonParser.parsedReceivedMessage.imus[0].gz,
                    imu1_MagX = JsonParser.parsedReceivedMessage.imus[0].mx,
                    imu1_MagY = JsonParser.parsedReceivedMessage.imus[0].my,
                    imu1_MagZ = JsonParser.parsedReceivedMessage.imus[0].mz,
                    imu1_Q0 = JsonParser.parsedReceivedMessage.imus[0].q0,
                    imu1_Q1 = JsonParser.parsedReceivedMessage.imus[0].q1,
                    imu1_Q2 = JsonParser.parsedReceivedMessage.imus[0].q2,
                    imu1_Q3 = JsonParser.parsedReceivedMessage.imus[0].q3,
                    imu2_AccX = JsonParser.parsedReceivedMessage.imus[1].ax,
                    imu2_AccY = JsonParser.parsedReceivedMessage.imus[1].ay,
                    imu2_AccZ = JsonParser.parsedReceivedMessage.imus[1].az,
                    imu2_GyroX = JsonParser.parsedReceivedMessage.imus[1].gx,
                    imu2_GyroY = JsonParser.parsedReceivedMessage.imus[1].gy,
                    imu2_GyroZ = JsonParser.parsedReceivedMessage.imus[1].gz,
                    imu2_MagX = JsonParser.parsedReceivedMessage.imus[1].mx,
                    imu2_MagY = JsonParser.parsedReceivedMessage.imus[1].my,
                    imu2_MagZ = JsonParser.parsedReceivedMessage.imus[1].mz,
                    imu2_Q0 = JsonParser.parsedReceivedMessage.imus[1].q0,
                    imu2_Q1 = JsonParser.parsedReceivedMessage.imus[1].q1,
                    imu2_Q2 = JsonParser.parsedReceivedMessage.imus[1].q2,
                    imu2_Q3 = JsonParser.parsedReceivedMessage.imus[1].q3,
                    tempExternal = JsonParser.parsedReceivedMessage.shts[0].temp,
                    humExternal = JsonParser.parsedReceivedMessage.shts[0].hum,
                    tempInternal = JsonParser.parsedReceivedMessage.shts[1].temp,
                    humInternal = JsonParser.parsedReceivedMessage.shts[1].hum,
                    pulse = JsonParser.parsedReceivedMessage.pulse,
                    gsr = JsonParser.parsedReceivedMessage.gsr
                };
                OnNewTextReceived(args);
                PublishData(args);
            }
            catch (Exception)
            {
                TextReceivedEventArgs args = new TextReceivedEventArgs
                {
                    textReceived = "Invalid JSON message at the MQTT Receiver"
                };
                Globals.jsonErrorMessage = true;
                OnNewTextReceived(args);
            }

        }
        #endregion
    }
}