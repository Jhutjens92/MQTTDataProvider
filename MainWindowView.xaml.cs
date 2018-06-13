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
        string Topic_Subscribe = "wekit/vest";

        //default MQTT server value for WEKIT
        string BrokerAddress = "test.mosquitto.org";

        //JSON Parser MQTT message
        dynamic Parsed_ReceivedMessage;

        public static bool isRecordingMQTT = false; //bool value for switching the record button text and the color
        //Checkbox Bools//
        public static bool multiple_Topics = false; //bool value for checking if multiple MQTT topics is enabled   
        public static bool debug = false;           //debug value for enable debugging

        MQTTManager.MQTTManager MQTTManager = new MQTTManager.MQTTManager();
        MQTTManager.LHConnector LHConnector = new MQTTManager.LHConnector();

        // this code runs when the main window opens (start of the app)
        public MainWindowView()
        {
            //INIT Functions//
            InitializeComponent();
            LHConnector.SetValueNames();
            
            MQTTDataProvider.MQTTManager.LHConnector.myConnector.stopRecordingEvent += MyConnector_stopRecordingEvent;
            MQTTDataProvider.MQTTManager.LHConnector.myConnector.startRecordingEvent += MyConnector_startRecordingEvent;

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

            string SHT1X2_Temp_Value = Parsed_ReceivedMessage.shts[1].temp;
            client.Publish("wekit/vest/Sht1_Temp", Encoding.UTF8.GetBytes(SHT1X2_Temp_Value), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);

            string SHT1X2_Hum_Value = Parsed_ReceivedMessage.shts[1].hum;
            client.Publish("wekit/vest/Sht1_Hum", Encoding.UTF8.GetBytes(SHT1X2_Hum_Value), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
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
                             IMU1_AccY.Text = Parsed_ReceivedMessage.imus[0].ay;
                             IMU1_AccZ.Text = Parsed_ReceivedMessage.imus[0].az;
                             IMU1_GyroX.Text = Parsed_ReceivedMessage.imus[0].gx;
                             IMU1_GyroY.Text = Parsed_ReceivedMessage.imus[0].gy;
                             IMU1_GyroZ.Text = Parsed_ReceivedMessage.imus[0].gz;
                             IMU1_MagX.Text = Parsed_ReceivedMessage.imus[0].mx;
                             IMU1_MagY.Text = Parsed_ReceivedMessage.imus[0].my;
                             IMU1_MagZ.Text = Parsed_ReceivedMessage.imus[0].mz;
                             IMU1_Q0.Text = Parsed_ReceivedMessage.imus[0].q0;
                             IMU1_Q1.Text = Parsed_ReceivedMessage.imus[0].q1;
                             IMU1_Q2.Text = Parsed_ReceivedMessage.imus[0].q2;
                             IMU1_Q3.Text = Parsed_ReceivedMessage.imus[0].q3;

                         }));
        }
        public void UpdateIMU2()
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(
                         () =>
                         {
                             IMU2_AccX.Text = Parsed_ReceivedMessage.imus[1].ax;
                             IMU2_AccY.Text = Parsed_ReceivedMessage.imus[1].ay;
                             IMU2_AccZ.Text = Parsed_ReceivedMessage.imus[1].az;
                             IMU2_GyroX.Text = Parsed_ReceivedMessage.imus[1].gx;
                             IMU2_GyroY.Text = Parsed_ReceivedMessage.imus[1].gy;
                             IMU2_GyroZ.Text = Parsed_ReceivedMessage.imus[1].gz;
                             IMU2_MagX.Text = Parsed_ReceivedMessage.imus[1].mx;
                             IMU2_MagY.Text = Parsed_ReceivedMessage.imus[1].my;
                             IMU2_MagZ.Text = Parsed_ReceivedMessage.imus[1].mz;
                             IMU2_Q0.Text = Parsed_ReceivedMessage.imus[1].q0;
                             IMU2_Q1.Text = Parsed_ReceivedMessage.imus[1].q1;
                             IMU2_Q2.Text = Parsed_ReceivedMessage.imus[1].q2;
                             IMU2_Q3.Text = Parsed_ReceivedMessage.imus[1].q3;
                         }));
        }

        public void UpdateSHT1X1()
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(
                         () =>
                         {
                             SHT1X1_Temp.Text = Parsed_ReceivedMessage.shts[0].temp;
                             SHT1X1_Hum.Text = Parsed_ReceivedMessage.shts[0].hum;
                         }));
        }

        public void UpdateSHT1X2()
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(
                         () =>
                         {
                             SHT1X2_Temp.Text = Parsed_ReceivedMessage.shts[1].temp;
                             SHT1X2_Hum.Text = Parsed_ReceivedMessage.shts[1].hum;
                         }));
        }

        public void UpdatePulseTemplobe()
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(
                         () =>
                         {
                             Pulse_TempLobe.Text = Parsed_ReceivedMessage.pulse;
                         }));
        }

        public void UpdateGSR()
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(
                         () =>
                         {
                             GSR.Text = Parsed_ReceivedMessage.gsr;
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
                
        private void MultipleTopics_Checked(object sender, RoutedEventArgs e)
        {
            multiple_Topics = false;
        }

        private void Debug_Checked(object sender, RoutedEventArgs e)
        {
            debug = false;
        }
                
        private void BtnServer_Set(object sender, RoutedEventArgs e)
        {
            try
            {
                BrokerAddress = txtMQTTServer.Text;
                client = new MqttClient(BrokerAddress);
                MessageBox.Show(string.Format("The new server is: {0}", BrokerAddress));
            }
            catch (Exception)
            {
                MessageBox.Show("Please enter a valid server");
            }                    

        }
    }
}