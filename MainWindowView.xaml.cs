using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Diagnostics;
using System.Windows.Media;
using System.Windows.Threading;

// including json library
using Newtonsoft.Json.Linq;

// including the M2Mqtt Library
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System.Text.RegularExpressions;

namespace MQTTDataProvider
{
    /// <summary>
    /// Interaction logic for MainWindowView.xaml
    /// </summary>
    public partial class MainWindowView : Window
    {

        MqttClient client;
        string clientId;

        //string containing the MQTT published message
        string ReceivedMessage;

        //default topic value for WEKIT
        string Topic_Subscribe;

        //default MQTT server value for WEKIT
        string BrokerAddress;

        //JSON Parser MQTT message
        dynamic Parsed_ReceivedMessage;

        public static bool isRecordingMQTT = false; //bool value for switching the record button text and the color
        //Checkbox Bools//
        public static bool multiple_Topics = false; //bool value for checking if multiple MQTT topics is enabled   
        public static bool debug = false;           //debug value for enable debugging

        MQTTManager.MQTTManager MQTTManager = new MQTTManager.MQTTManager();

        // this code runs when the main window opens (start of the app)
        public MainWindowView()
        {

            //INIT Var Values//
            BrokerAddress = "localhost";
            Topic_Subscribe = "wekit/vest";

            //INIT Functions//
            InitializeComponent();
            MQTTManager.SetValueNames();
            
            MQTTDataProvider.MQTTManager.MQTTManager.myConnector.stopRecordingEvent += MyConnector_stopRecordingEvent;
            MQTTDataProvider.MQTTManager.MQTTManager.myConnector.startRecordingEvent += MyConnector_startRecordingEvent;

            //MQTT Functions//
            client = new MqttClient(BrokerAddress);
            // register a callback-function (we have to implement, see below) which is called by the library when a message was received
            client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;
            // use a unique id as client id, each time we start the application
            clientId = Guid.NewGuid().ToString();
            client.Connect(clientId);
            Subscribe_Default();
        }

        private void MyConnector_stopRecordingEvent(object sender)
        {
            MQTTManager.IsRecording = false;
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(
                        () =>
                        {
                            StartRecordingData();
                        }));
        }

        private void MyConnector_startRecordingEvent(object sender)
        {
            MQTTManager.IsRecording = true;
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(
                        () =>
                        {
                            StartRecordingData();
                        }));
        }

        // this code runs when the main window closes (end of the app)
        protected override void OnClosed(EventArgs e)
        {
            client.Disconnect();

            base.OnClosed(e);
            App.Current.Shutdown();
        }

        private void Subscribe_Default()
        {
            // subscribe to the topic with QoS 2
            client.Subscribe(new string[] { Topic_Subscribe }, new byte[] { 2 });   // we need arrays as parameters because we can subscribe to different topics with one call
            txtReceived.Text = "";
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

        // this code runs when a message was received
        void Client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            ReceivedMessage = Encoding.UTF8.GetString(e.Message);

            if (isRecordingMQTT == true){
                Dispatcher.Invoke(delegate
                {              // we need this construction because the receiving code in the library and the UI with textbox run on different threads
                    txtReceived.Text = ReceivedMessage;
                });

                JSONParse_ReceivedMessage();
                Publish_Data();
                MQTTManager.MQTT_DataAcquired();
            }
        }

        //parse MQTT JSON String
        void JSONParse_ReceivedMessage()
        {
            Parsed_ReceivedMessage = JObject.Parse(ReceivedMessage);

            UpdateIMU1();
            UpdateIMU2();
            UpdateSHT1X1();
            UpdateSHT1X2();
            UpdatePulseTemplobe();
            UpdateGSR();
        }

        //code blocks to update the main window textboxes with the values they represent
        public void UpdateIMU1()
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(
                         () =>
                         {
                             IMU1_AccX.Text = Parsed_ReceivedMessage.imus[0].ax;
                             MQTTManager.IMU1_AccX = IMU1_AccX.Text;
                             IMU1_AccY.Text = Parsed_ReceivedMessage.imus[0].ay;
                             MQTTManager.IMU1_AccY = IMU1_AccY.Text;
                             IMU1_AccZ.Text = Parsed_ReceivedMessage.imus[0].az;
                             MQTTManager.IMU1_AccZ = IMU1_AccZ.Text;
                             IMU1_GyroX.Text = Parsed_ReceivedMessage.imus[0].gx;
                             MQTTManager.IMU1_GyroX = IMU1_GyroX.Text;
                             IMU1_GyroY.Text = Parsed_ReceivedMessage.imus[0].gy;
                             MQTTManager.IMU1_GyroY = IMU1_GyroY.Text;
                             IMU1_GyroZ.Text = Parsed_ReceivedMessage.imus[0].gz;
                             MQTTManager.IMU1_GyroZ = IMU1_GyroZ.Text;
                             IMU1_MagX.Text = Parsed_ReceivedMessage.imus[0].mx;
                             MQTTManager.IMU1_MagX = IMU1_MagX.Text;
                             IMU1_MagY.Text = Parsed_ReceivedMessage.imus[0].my;
                             MQTTManager.IMU1_MagY = IMU1_MagY.Text;
                             IMU1_MagZ.Text = Parsed_ReceivedMessage.imus[0].mz;
                             MQTTManager.IMU1_MagZ = IMU1_MagZ.Text;
                             IMU1_Q0.Text = Parsed_ReceivedMessage.imus[0].q0;
                             MQTTManager.IMU1_Q0 = IMU1_Q0.Text;
                             IMU1_Q1.Text = Parsed_ReceivedMessage.imus[0].q1;
                             MQTTManager.IMU1_Q1 = IMU1_Q1.Text;
                             IMU1_Q2.Text = Parsed_ReceivedMessage.imus[0].q2;
                             MQTTManager.IMU1_Q2 = IMU1_Q2.Text;
                             IMU1_Q3.Text = Parsed_ReceivedMessage.imus[0].q3;
                             MQTTManager.IMU1_Q3 = IMU1_Q3.Text;

                         }));
        }
        public void UpdateIMU2()
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(
                         () =>
                         {
                             IMU2_AccX.Text = Parsed_ReceivedMessage.imus[1].ax;
                             MQTTManager.IMU2_AccX = IMU2_AccX.Text;
                             IMU2_AccY.Text = Parsed_ReceivedMessage.imus[1].ay;
                             MQTTManager.IMU2_AccY = IMU2_AccY.Text;
                             IMU2_AccZ.Text = Parsed_ReceivedMessage.imus[1].az;
                             MQTTManager.IMU2_AccZ = IMU2_AccZ.Text;
                             IMU2_GyroX.Text = Parsed_ReceivedMessage.imus[1].gx;
                             MQTTManager.IMU2_GyroX = IMU2_GyroX.Text;
                             IMU2_GyroY.Text = Parsed_ReceivedMessage.imus[1].gy;
                             MQTTManager.IMU2_GyroY = IMU2_GyroY.Text;
                             IMU2_GyroZ.Text = Parsed_ReceivedMessage.imus[1].gz;
                             MQTTManager.IMU2_GyroZ = IMU2_GyroZ.Text;
                             IMU2_MagX.Text = Parsed_ReceivedMessage.imus[1].mx;
                             MQTTManager.IMU2_MagX = IMU2_MagX.Text;
                             IMU2_MagY.Text = Parsed_ReceivedMessage.imus[1].my;
                             MQTTManager.IMU2_MagY = IMU2_MagY.Text;
                             IMU2_MagZ.Text = Parsed_ReceivedMessage.imus[1].mz;
                             MQTTManager.IMU2_MagZ = IMU2_MagZ.Text;
                             IMU2_Q0.Text = Parsed_ReceivedMessage.imus[1].q0;
                             MQTTManager.IMU2_Q0 = IMU2_Q0.Text;
                             IMU2_Q1.Text = Parsed_ReceivedMessage.imus[1].q1;
                             MQTTManager.IMU2_Q1 = IMU2_Q1.Text;
                             IMU2_Q2.Text = Parsed_ReceivedMessage.imus[1].q2;
                             MQTTManager.IMU2_Q2 = IMU2_Q2.Text;
                             IMU2_Q3.Text = Parsed_ReceivedMessage.imus[1].q3;
                             MQTTManager.IMU2_Q3 = IMU2_Q3.Text;
                         }));
        }

        public void UpdateSHT1X1()
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(
                         () =>
                         {
                             SHT1X1_Temp.Text = Parsed_ReceivedMessage.shts[0].temp;
                             MQTTManager.Temp_External = SHT1X1_Temp.Text;
                             SHT1X1_Hum.Text = Parsed_ReceivedMessage.shts[0].hum;
                             MQTTManager.Humidity_External = SHT1X1_Hum.Text;
                         }));
        }

        public void UpdateSHT1X2()
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(
                         () =>
                         {
                             SHT1X2_Temp.Text = Parsed_ReceivedMessage.shts[0].temp;
                             MQTTManager.Temp_Internal = SHT1X2_Temp.Text;
                             SHT1X2_Hum.Text = Parsed_ReceivedMessage.shts[0].hum;
                             MQTTManager.Humidity_Internal = SHT1X2_Hum.Text;
                         }));
        }

        public void UpdatePulseTemplobe()
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(
                         () =>
                         {
                             Pulse_TempLobe.Text = Parsed_ReceivedMessage.pulse;
                             MQTTManager.Pulse_TempLobe = Pulse_TempLobe.Text;
                         }));
        }

        public void UpdateGSR()
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(
                         () =>
                         {
                             GSR.Text = Parsed_ReceivedMessage.gsr;
                             MQTTManager.GSR = GSR.Text;
                         }));
        }

        // this code runs when the button "Record" is clicked. Without hitting record the MQTT Manager wont store or receive data.
        private void BtnRecord_Click(object sender, RoutedEventArgs e)
        {
            MQTTManager.IsRecording = !MQTTManager.IsRecording;
            StartRecordingData();

        }

        public void StartRecordingData()
        {
            if (isRecordingMQTT == false)
            {
                isRecordingMQTT = true;
                BtnRecord.Content = "Stop Recording";
                BtnRecord.Background = new SolidColorBrush(Colors.Green);

            }
            else if (isRecordingMQTT == true)
            {
                isRecordingMQTT = false;
                BtnRecord.Content = "Start Recording";
                BtnRecord.Background = new SolidColorBrush(Colors.White);
            }
            Debug.WriteLine("isRecordingData= " + isRecordingMQTT);
        }
    }
}