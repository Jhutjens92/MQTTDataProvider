using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Diagnostics;
using System.Windows.Media;
using System.Windows.Threading;

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

        //bool value for switching the record button text and the color
        public static bool isRecordingMQTT = false;

        //bool value for checking if multiple MQTT topics is enabled
        public static bool multiple_Topics = false;

        //debug value for enable debugging
        public static bool debug = false;

        //string list for assigning values
        string[] ReceivedMessage_List;

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

            // MQTT Functions//
            string BrokerAddress = "localhost";
            client = new MqttClient(BrokerAddress);
            // register a callback-function (we have to implement, see below) which is called by the library when a message was received
            client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;
            // use a unique id as client id, each time we start the application
            clientId = Guid.NewGuid().ToString();
            client.Connect(clientId);
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


        // this code runs when the button "Subscribe" is clicked
        private void BtnSubscribe_Click(object sender, RoutedEventArgs e)
        {
            if (txtTopicSubscribe.Text != "")
            {
                // whole topic
                string Topic = txtTopicSubscribe.Text;

                // subscribe to the topic with QoS 2
                client.Subscribe(new string[] { Topic }, new byte[] { 2 });   // we need arrays as parameters because we can subscribe to different topics with one call
                txtReceived.Text = "";
            }
            else
            {
                System.Windows.MessageBox.Show("You have to enter one or more topics to subscribe to!");
            }
        }

        // this code runs when the button "Publish" is clicked
        private void BtnPublish_Click(object sender, RoutedEventArgs e)
        {
            if (txtTopicPublish.Text != "")
            {
                // whole topic
                string Topic = txtTopicPublish.Text;

                // publish a message with QoS 2
                client.Publish(Topic, Encoding.UTF8.GetBytes(txtPublish.Text), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
            }
            else
            {
                System.Windows.MessageBox.Show("You have to enter a topic to publish!");
            }
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

                Format_ReceivedMessage();
            }
        }

        //clean up of the received MQTT string
        void Format_ReceivedMessage()
        {

            int letterIndex;
            string ReceivedMessageFiltered = "";
            bool quotesOpened = false;
            for (letterIndex = 0; letterIndex < ReceivedMessage.Length; letterIndex++)
            {
                if (ReceivedMessage[letterIndex] == '"')
                {
                    quotesOpened = !quotesOpened;

                    ReceivedMessageFiltered = ReceivedMessageFiltered + ReceivedMessage[letterIndex];
                }
                else
                {
                    if (!quotesOpened)
                        ReceivedMessageFiltered = ReceivedMessageFiltered + ReceivedMessage[letterIndex];
                }
            }

            Regex charsToDestroy = new Regex(@"[^\d|\.\-\,]");
            string ReceivedMessage_Formatted = charsToDestroy.Replace(ReceivedMessageFiltered, "");
            ReceivedMessage_List = ReceivedMessage_Formatted.Split(',');
            UpdateIMU1();
            UpdateIMU2();
            UpdateSHT1X1();
            UpdateSHT1X2();
            UpdatePulsePulse();
            UpdatePulseTemplobe();
            UpdateGSR();
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

        //code blocks to update the main window textboxes with the values they represent
        public void UpdateIMU1()
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(
                         () =>
                         {
                             IMU1_AccX.Text = ReceivedMessage_List[2].ToString();
                             IMU1_AccY.Text = ReceivedMessage_List[3].ToString();
                             IMU1_AccZ.Text = ReceivedMessage_List[4].ToString();
                             IMU1_GyroX.Text = ReceivedMessage_List[5].ToString();
                             IMU1_GyroY.Text = ReceivedMessage_List[6].ToString();
                             IMU1_GyroZ.Text = ReceivedMessage_List[7].ToString();
                             IMU1_MagX.Text = ReceivedMessage_List[8].ToString();
                             IMU1_MagY.Text = ReceivedMessage_List[9].ToString();
                             IMU1_MagZ.Text = ReceivedMessage_List[10].ToString();

                         }));
        }

        public void UpdateIMU2()
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(
                         () =>
                         {
                             IMU2_AccX.Text = ReceivedMessage_List[8].ToString();
                             IMU2_AccY.Text = ReceivedMessage_List[8].ToString();
                             IMU2_AccZ.Text = ReceivedMessage_List[8].ToString();
                             IMU2_GyroX.Text = ReceivedMessage_List[8].ToString();
                             IMU2_GyroY.Text = ReceivedMessage_List[8].ToString();
                             IMU2_GyroZ.Text = ReceivedMessage_List[8].ToString();
                             IMU2_MagX.Text = ReceivedMessage_List[8].ToString();
                             IMU2_MagY.Text = ReceivedMessage_List[8].ToString();
                             IMU2_MagZ.Text = ReceivedMessage_List[8].ToString();
                         }));
        }

        public void UpdateSHT1X1()
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(
                         () =>
                         {
                             SHT1X1_Temp.Text = ReceivedMessage_List[8].ToString();
                             SHT1X1_Hum.Text = ReceivedMessage_List[8].ToString();
                         }));
        }

        public void UpdateSHT1X2()
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(
                         () =>
                         {
                             SHT1X2_Temp.Text = ReceivedMessage_List[8].ToString();
                             SHT1X2_Hum.Text = ReceivedMessage_List[8].ToString();
                         }));
        }

        public void UpdatePulsePulse()
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(
                         () =>
                         {
                             Pulse_Pulse.Text = ReceivedMessage_List[8].ToString();
                         }));
        }

        public void UpdatePulseTemplobe()
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(
                         () =>
                         {
                             Pulse_TempLobe.Text = ReceivedMessage_List[8].ToString();
                         }));
        }

        public void UpdateGSR()
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(
                         () =>
                         {
                             GSR.Text = ReceivedMessage_List[11].ToString();
                         }));
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
            string BrokerAddress = txtMQTTServer.Text;
        }
    }
}