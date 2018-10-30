using MQTTDataProvider.ViewModel;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Text;
using uPLibrary.Networking.M2Mqtt.Messages;
using uPLibrary.Networking.M2Mqtt;
using Newtonsoft.Json.Serialization;
using MQTTDataProvider.Classes;
using System.ComponentModel;

namespace MQTTDataProvider.Classes
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// MqttManager class handles all Mqtt functions. Also includes updating the variables based on
    /// published data.
    /// </summary>
    ///
    /// <remarks>   Jordi Hutjens, 26-10-2018. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    
    class MqttManager
    {
        #region Instance declaration

        static MqttClient Client;
        CheckParameters chkpar = new CheckParameters();
        JsonParser jsonpar = new JsonParser();
        SendToLH sendlh = new SendToLH();

        #endregion

        #region Events

        /// <summary>   Handler for other classes to subscribe to. </summary>
        public event EventHandler<TextReceivedEventArgs> NewMqttTextReceived;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Raising the event in the MqttManager class. </summary>
        ///
        /// <remarks>   Jordi Hutjens, 26-10-2018. </remarks>
        ///
        /// <param name="e">    Containing the filtered Json string. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        protected virtual void OnNewTextReceived(TextReceivedEventArgs e)
        {
            NewMqttTextReceived?.Invoke(this, e);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// inherits from event args which holds all the values that needs to be passed as args in the
        /// event.
        /// </summary>
        ///
        /// <remarks>   Jordi Hutjens, 26-10-2018. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public class TextReceivedEventArgs : EventArgs
        {
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>   Gets or sets the text received. </summary>
            ///
            /// <value> The text received. </value>
            ////////////////////////////////////////////////////////////////////////////////////////////////////

            public string TextReceived
            {
                get
                {
                    return textReceived;
                }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    textReceived = value;
                }
            }
            private string textReceived;

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>   Gets or sets the imu 1 accumulate x coordinate. </summary>
            ///
            /// <value> The imu 1 accumulate x coordinate. </value>
            ////////////////////////////////////////////////////////////////////////////////////////////////////

            public string IMU1_AccX
            {
                get { return imu1_AccX; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    imu1_AccX = value;
                }
            }
            private string imu1_AccX = "";

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>   Gets or sets the imu 1 accumulate y coordinate. </summary>
            ///
            /// <value> The imu 1 accumulate y coordinate. </value>
            ////////////////////////////////////////////////////////////////////////////////////////////////////

            public string IMU1_AccY
            {
                get { return imu1_AccY; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    imu1_AccY = value;
                }
            }
            private string imu1_AccY = "";

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>   Gets or sets the imu 1 accumulate z coordinate. </summary>
            ///
            /// <value> The imu 1 accumulate z coordinate. </value>
            ////////////////////////////////////////////////////////////////////////////////////////////////////

            public string IMU1_AccZ
            {
                get { return imu1_AccZ; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    imu1_AccZ = value;
                }
            }
            private string imu1_AccZ = "";

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>   Gets or sets the imu 1 gyro x coordinate. </summary>
            ///
            /// <value> The imu 1 gyro x coordinate. </value>
            ////////////////////////////////////////////////////////////////////////////////////////////////////

            public string IMU1_GyroX
            {
                get { return imu1_GyroX; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    imu1_GyroX = value;
                }
            }
            private string imu1_GyroX = "";

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>   Gets or sets the imu 1 gyro y coordinate. </summary>
            ///
            /// <value> The imu 1 gyro y coordinate. </value>
            ////////////////////////////////////////////////////////////////////////////////////////////////////

            public string IMU1_GyroY
            {
                get { return imu1_GyroY; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    imu1_GyroY = value;
                }
            }
            private string imu1_GyroY = "";

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>   Gets or sets the imu 1 gyro z coordinate./ </summary>
            ///
            /// <value> The imu 1 gyro z coordinate. </value>
            ////////////////////////////////////////////////////////////////////////////////////////////////////

            public string IMU1_GyroZ
            {
                get { return imu1_GyroZ; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    imu1_GyroZ = value;
                }
            }
            private string imu1_GyroZ = "";

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>   Gets or sets the imu 1 magnitude x coordinate. </summary>
            ///
            /// <value> The imu 1 magnitude x coordinate. </value>
            ////////////////////////////////////////////////////////////////////////////////////////////////////

            public string IMU1_MagX
            {
                get { return imu1_MagX; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    imu1_MagX = value;
                }
            }
            private string imu1_MagX = "";

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>   Gets or sets the imu 1 magnitude y coordinate. </summary>
            ///
            /// <value> The imu 1 magnitude y coordinate. </value>
            ////////////////////////////////////////////////////////////////////////////////////////////////////

            public string IMU1_MagY
            {
                get { return imu1_MagY; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    imu1_MagY = value;
                }
            }
            private string imu1_MagY = "";

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>   Gets or sets the imu 1 magnitude z coordinate. </summary>
            ///
            /// <value> The imu 1 magnitude z coordinate. </value>
            ////////////////////////////////////////////////////////////////////////////////////////////////////

            public string IMU1_MagZ
            {
                get { return imu1_MagZ; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    imu1_MagZ = value;
                }
            }
            private string imu1_MagZ = "";

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>   Gets or sets the imu 1 q 0. </summary>
            ///
            /// <value> The imu 1 q 0. </value>
            ////////////////////////////////////////////////////////////////////////////////////////////////////

            public string IMU1_Q0
            {
                get { return imu1_Q0; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    imu1_Q0 = value;
                }
            }
            private string imu1_Q0 = "";

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>   Gets or sets the imu 1 q 1. </summary>
            ///
            /// <value> The imu 1 q 1. </value>
            ////////////////////////////////////////////////////////////////////////////////////////////////////

            public string IMU1_Q1
            {
                get { return imu1_Q1; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    imu1_Q1 = value;
                }
            }
            private string imu1_Q1 = "";

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>   Gets or sets the imu 1 q 2. </summary>
            ///
            /// <value> The imu 1 q 2. </value>
            ////////////////////////////////////////////////////////////////////////////////////////////////////

            public string IMU1_Q2
            {
                get { return imu1_Q2; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    imu1_Q2 = value;
                }
            }
            private string imu1_Q2 = "";

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>   Gets or sets the imu 1 q 3. </summary>
            ///
            /// <value> The imu 1 q 3. </value>
            ////////////////////////////////////////////////////////////////////////////////////////////////////

            public string IMU1_Q3
            {
                get { return imu1_Q3; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    imu1_Q3 = value;
                }
            }
            private string imu1_Q3 = "";

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>   Gets or sets the imu 2 accumulate x coordinate. </summary>
            ///
            /// <value> The imu 2 accumulate x coordinate. </value>
            ////////////////////////////////////////////////////////////////////////////////////////////////////

            public string IMU2_AccX
            {
                get { return imu2_AccX; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    imu2_AccX = value;
                }
            }
            private string imu2_AccX = "";

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>   Gets or sets the imu 2 accumulate y coordinate. </summary>
            ///
            /// <value> The imu 2 accumulate y coordinate. </value>
            ////////////////////////////////////////////////////////////////////////////////////////////////////

            public string IMU2_AccY
            {
                get { return imu2_AccY; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    imu2_AccY = value;
                }
            }
            private string imu2_AccY = "";

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>   Gets or sets the imu 2 accumulate z coordinate. </summary>
            ///
            /// <value> The imu 2 accumulate z coordinate. </value>
            ////////////////////////////////////////////////////////////////////////////////////////////////////

            public string IMU2_AccZ
            {
                get { return imu2_AccZ; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    imu2_AccZ = value;
                }
            }
            private string imu2_AccZ = "";

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>   Gets or sets the imu 2 gyro x coordinate. </summary>
            ///
            /// <value> The imu 2 gyro x coordinate. </value>
            ////////////////////////////////////////////////////////////////////////////////////////////////////

            public string IMU2_GyroX
            {
                get { return imu2_GyroX; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    imu2_GyroX = value;
                }
            }
            private string imu2_GyroX = "";

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>   Gets or sets the imu 2 gyro y coordinate. </summary>
            ///
            /// <value> The imu 2 gyro y coordinate. </value>
            ////////////////////////////////////////////////////////////////////////////////////////////////////

            public string IMU2_GyroY
            {
                get { return imu2_GyroY; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    imu2_GyroY = value;
                }
            }
            private string imu2_GyroY = "";

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>   Gets or sets the imu 2 gyro z coordinate. </summary>
            ///
            /// <value> The imu 2 gyro z coordinate. </value>
            ////////////////////////////////////////////////////////////////////////////////////////////////////

            public string IMU2_GyroZ
            {
                get { return imu2_GyroZ; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    imu2_GyroZ = value;
                }
            }
            private string imu2_GyroZ = "";

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>   Gets or sets the imu 2 magnitude x coordinate. </summary>
            ///
            /// <value> The imu 2 magnitude x coordinate. </value>
            ////////////////////////////////////////////////////////////////////////////////////////////////////

            public string IMU2_MagX
            {
                get { return imu2_MagX; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    imu2_MagX = value;
                }
            }
            private string imu2_MagX = "";

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>   Gets or sets the imu 2 magnitude y coordinate. </summary>
            ///
            /// <value> The imu 2 magnitude y coordinate. </value>
            ////////////////////////////////////////////////////////////////////////////////////////////////////

            public string IMU2_MagY
            {
                get { return imu2_MagY; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    imu2_MagY = value;
                }
            }
            private string imu2_MagY = "";

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>   Gets or sets the imu 2 magnitude z coordinate. </summary>
            ///
            /// <value> The imu 2 magnitude z coordinate. </value>
            ////////////////////////////////////////////////////////////////////////////////////////////////////

            public string IMU2_MagZ
            {
                get { return imu2_MagZ; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    imu2_MagZ = value;
                }
            }
            private string imu2_MagZ = "";

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>   Gets or sets the imu 2 q 0. </summary>
            ///
            /// <value> The imu 2 q 0. </value>
            ////////////////////////////////////////////////////////////////////////////////////////////////////

            public string IMU2_Q0
            {
                get { return imu2_Q0; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    imu2_Q0 = value;
                }
            }
            private string imu2_Q0 = "";

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>   Gets or sets the imu 2 q 1. </summary>
            ///
            /// <value> The imu 2 q 1. </value>
            ////////////////////////////////////////////////////////////////////////////////////////////////////

            public string IMU2_Q1
            {
                get { return imu2_Q1; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    imu2_Q1 = value;
                }
            }
            private string imu2_Q1 = "";

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>   Gets or sets the imu 2 q 2. </summary>
            ///
            /// <value> The imu 2 q 2. </value>
            ////////////////////////////////////////////////////////////////////////////////////////////////////

            public string IMU2_Q2
            {
                get { return imu2_Q2; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    imu2_Q2 = value;
                }
            }
            private string imu2_Q2 = "";

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>   Gets or sets the imu 2 q 3. </summary>
            ///
            /// <value> The imu 2 q 3. </value>
            ////////////////////////////////////////////////////////////////////////////////////////////////////

            public string IMU2_Q3
            {
                get { return imu2_Q3; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    imu2_Q3 = value;
                }
            }
            private string imu2_Q3 = "";

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>   Gets or sets the temporary external. </summary>
            ///
            /// <value> The temporary external. </value>
            ////////////////////////////////////////////////////////////////////////////////////////////////////

            public string TempExternal
            {
                get { return tempExternal; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    tempExternal = value;
                }
            }
            private string tempExternal = "";

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>   Gets or sets the hum external. </summary>
            ///
            /// <value> The hum external. </value>
            ////////////////////////////////////////////////////////////////////////////////////////////////////

            public string HumExternal
            {
                get { return humExternal; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    humExternal = value;
                }
            }
            private string humExternal = "";

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>   Gets or sets the temporary internal. </summary>
            ///
            /// <value> The temporary internal. </value>
            ////////////////////////////////////////////////////////////////////////////////////////////////////

            public string TempInternal
            {
                get { return tempInternal; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    tempInternal = value;
                }
            }
            private string tempInternal = "";

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>   Gets or sets the hum internal. </summary>
            ///
            /// <value> The hum internal. </value>
            ////////////////////////////////////////////////////////////////////////////////////////////////////

            public string HumInternal
            {
                get { return humInternal; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    humInternal = value;
                }
            }
            private string humInternal = "";

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>   Gets or sets the pulse. </summary>
            ///
            /// <value> The pulse. </value>
            ////////////////////////////////////////////////////////////////////////////////////////////////////

            public string Pulse
            {
                get { return pulse; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    pulse = value;
                }
            }
            private string pulse = "";

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>   Gets or sets the gsr. </summary>
            ///
            /// <value> The gsr. </value>
            ////////////////////////////////////////////////////////////////////////////////////////////////////

            public string GSR
            {
                get { return gsr; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    gsr = value;
                }
            }
            private string gsr = "";

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>   Gets or sets the esp time stamp. </summary>
            ///
            /// <value> The esp time stamp. </value>
            ////////////////////////////////////////////////////////////////////////////////////////////////////

            public string ESPTimeStamp
            {
                get { return espTimeStamp; }
                set
                {
                    if (value == null)
                    {
                        value = 0.ToString();
                    }
                    espTimeStamp = value;
                }
            }
            private string espTimeStamp = "";
        }

        #endregion

        #region Constructor

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Default constructor. </summary>
        ///
        /// <remarks>   Jordi Hutjens, 26-10-2018. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public MqttManager()
        {
            chkpar.CheckStartupParameters();
            CreateMqttClient();
            ConnectMqttClient();
            Client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;
            Subscribe_Default();

        }

        #endregion

        #region Methods

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Creates the MQTT Client. </summary>
        ///
        /// <remarks>   Jordi Hutjens, 26-10-2018. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        private void CreateMqttClient()
        {
            try
            {
                Client = new MqttClient(chkpar.BrokerAddress);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            } 
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Connects to the MQTT Client with a random ID (generated) </summary>
        ///
        /// <remarks>   Jordi Hutjens, 26-10-2018. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        private void ConnectMqttClient()
        {
            try
            {
                string clientId;
                clientId = Guid.NewGuid().ToString();
                Client.Connect(clientId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Closes the MQTT connection when the program stops. </summary>
        ///
        /// <remarks>   Jordi Hutjens, 26-10-2018. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public void CloseMqttConnection()
        {
            try
            {
                Client.Disconnect();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Callback function that executes when a MQTT message was received. </summary>
        ///
        /// <remarks>   Jordi Hutjens, 26-10-2018. </remarks>
        ///
        /// <param name="sender">   . </param>
        /// <param name="e">        Event arguments containing the published Mqtt message. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            try
            {
                // string containing the MQTT published message
                string ReceivedMqttMsg = Encoding.UTF8.GetString(e.Message);
                if (Globals.IsRecordingMqtt)
                {
                    jsonpar.JSONParseReceivedMessage(ReceivedMqttMsg);
                    UpdateValues();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Publish specific ESP data to the predefined topics using Mqtt/QoS 1. </summary>
        ///
        /// <remarks>   Jordi Hutjens, 26-10-2018. </remarks>
        ///
        /// <param name="e">    Containing the filtered received Mqtt text. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////  
        private void PublishData(TextReceivedEventArgs e)
        {
            try
            {
                Client.Publish("wekit/vest/GSR_Raw", Encoding.UTF8.GetBytes(e.GSR), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
                Client.Publish("wekit/vest/Pulse_Raw", Encoding.UTF8.GetBytes(e.Pulse), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
                Client.Publish("wekit/vest/Sht0_Temp", Encoding.UTF8.GetBytes(e.TempExternal), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
                Client.Publish("wekit/vest/Sht0_Hum", Encoding.UTF8.GetBytes(e.HumExternal), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
                Client.Publish("wekit/vest/Sht1_Temp", Encoding.UTF8.GetBytes(e.TempInternal), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
                Client.Publish("wekit/vest/Sht1_Hum", Encoding.UTF8.GetBytes(e.HumInternal), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Subscribes to the default Mqtt topic ("wekit/vest") </summary>
        ///
        /// <remarks>   Jordi Hutjens, 26-10-2018. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Subscribe_Default()
        {
            try
            {
                // Default topic value for WEKIT
                string topicSubscribe = "wekit/vest";
                Client.Subscribe(new string[] { topicSubscribe }, new byte[] { 1 });
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Sets all the variables to the received values. </summary>
        ///
        /// <remarks>   Jordi Hutjens, 26-10-2018. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        private void UpdateValues()
        {
            try
            {
                TextReceivedEventArgs args = new TextReceivedEventArgs
                {
                    TextReceived = jsonpar.ParsedMqttMsg,
                    ESPTimeStamp = jsonpar.ParsedMqttMsg.time,
                    IMU1_AccX = jsonpar.ParsedMqttMsg.imus[0].ax,
                    IMU1_AccY = jsonpar.ParsedMqttMsg.imus[0].ay,
                    IMU1_AccZ = jsonpar.ParsedMqttMsg.imus[0].az,
                    IMU1_GyroX = jsonpar.ParsedMqttMsg.imus[0].gx,
                    IMU1_GyroY = jsonpar.ParsedMqttMsg.imus[0].gy,
                    IMU1_GyroZ = jsonpar.ParsedMqttMsg.imus[0].gz,
                    IMU1_MagX = jsonpar.ParsedMqttMsg.imus[0].mx,
                    IMU1_MagY = jsonpar.ParsedMqttMsg.imus[0].my,
                    IMU1_MagZ = jsonpar.ParsedMqttMsg.imus[0].mz,
                    IMU1_Q0 = jsonpar.ParsedMqttMsg.imus[0].q0,
                    IMU1_Q1 = jsonpar.ParsedMqttMsg.imus[0].q1,
                    IMU1_Q2 = jsonpar.ParsedMqttMsg.imus[0].q2,
                    IMU1_Q3 = jsonpar.ParsedMqttMsg.imus[0].q3,
                    IMU2_AccX = jsonpar.ParsedMqttMsg.imus[1].ax,
                    IMU2_AccY = jsonpar.ParsedMqttMsg.imus[1].ay,
                    IMU2_AccZ = jsonpar.ParsedMqttMsg.imus[1].az,
                    IMU2_GyroX = jsonpar.ParsedMqttMsg.imus[1].gx,
                    IMU2_GyroY = jsonpar.ParsedMqttMsg.imus[1].gy,
                    IMU2_GyroZ = jsonpar.ParsedMqttMsg.imus[1].gz,
                    IMU2_MagX = jsonpar.ParsedMqttMsg.imus[1].mx,
                    IMU2_MagY = jsonpar.ParsedMqttMsg.imus[1].my,
                    IMU2_MagZ = jsonpar.ParsedMqttMsg.imus[1].mz,
                    IMU2_Q0 = jsonpar.ParsedMqttMsg.imus[1].q0,
                    IMU2_Q1 = jsonpar.ParsedMqttMsg.imus[1].q1,
                    IMU2_Q2 = jsonpar.ParsedMqttMsg.imus[1].q2,
                    IMU2_Q3 = jsonpar.ParsedMqttMsg.imus[1].q3,
                    TempExternal = jsonpar.ParsedMqttMsg.shts[0].temp,
                    HumExternal = jsonpar.ParsedMqttMsg.shts[0].hum,
                    TempInternal = jsonpar.ParsedMqttMsg.shts[1].temp,
                    HumInternal = jsonpar.ParsedMqttMsg.shts[1].hum,
                    Pulse = jsonpar.ParsedMqttMsg.pulse,
                    GSR = jsonpar.ParsedMqttMsg.gsr
                };
                OnNewTextReceived(args);
                PublishData(args);
                sendlh.SendDataToLH(args);
            }
            catch (Exception ex)
            {
                TextReceivedEventArgs args = new TextReceivedEventArgs
                {
                    TextReceived = ex.Message
                };
                OnNewTextReceived(args);
            }
        }

        #endregion
    }
}