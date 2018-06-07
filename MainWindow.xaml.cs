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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        MqttClient client;
        string clientId;

        //string containing the MQTT published message
        string ReceivedMessage;

        //bool value for switching the record button text and the color
        public static bool isRecordingMQTT = false;

        //string list for assigning values
        string[] ReceivedMessage_List;

        MQTTManager.MQTTManager MQTTManager = new MQTTManager.MQTTManager();

        // this code runs when the main window opens (start of the app)
        public MainWindow()
        {
            InitializeComponent();

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
                //string Topic = "/WEKIT/" + txtTopicSubscribe.Text + "/test";

                string Topic = txtTopicSubscribe.Text;

                // subscribe to the topic with QoS 2
                client.Subscribe(new string[] { Topic }, new byte[] { 2 });   // we need arrays as parameters because we can subscribe to different topics with one call
                txtReceived.Text = "";
            }
            else
            {
                System.Windows.MessageBox.Show("You have to enter a topic to subscribe!");
            }
        }


        // this code runs when the button "Publish" is clicked
        private void BtnPublish_Click(object sender, RoutedEventArgs e)
        {
            if (txtTopicPublish.Text != "")
            {
                // whole topic
                //string Topic = "/WEKIT/" + txtTopicPublish.Text + "/test";
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

        void Format_ReceivedMessage()
        {
            Regex charsToDestroy = new Regex(@"[^\d|\.\-\,]");
            string ReceivedMessage_Formatted = charsToDestroy.Replace(ReceivedMessage, "");
            ReceivedMessage_List = ReceivedMessage_Formatted.Split(',');
            UpdateIMU1();
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
                             IMU1_AccX.Text = ReceivedMessage_List[0].ToString();
                             IMU1_AccY.Text = ReceivedMessage_List[1].ToString();
                             IMU1_AccZ.Text = ReceivedMessage_List[2].ToString();
                             IMU1_GyroX.Text = ReceivedMessage_List[3].ToString();
                             IMU1_GyroY.Text = ReceivedMessage_List[4].ToString();
                             IMU1_GyroZ.Text = ReceivedMessage_List[5].ToString();
                             IMU1_MagX.Text = ReceivedMessage_List[6].ToString();
                             IMU1_MagY.Text = ReceivedMessage_List[7].ToString();
                             IMU1_MagZ.Text = ReceivedMessage_List[8].ToString();

                         }));
        }

        public void UpdateIMU2(float a, float b, float c, float d, float e, float f, float g, float h, float i)
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(
                         () =>
                         {
                             IMU2_AccX.Text = a.ToString();
                             IMU2_AccY.Text = b.ToString();
                             IMU2_AccZ.Text = c.ToString();
                             IMU2_GyroX.Text = d.ToString();
                             IMU2_GyroY.Text = e.ToString();
                             IMU2_GyroZ.Text = f.ToString();
                             IMU2_MagX.Text = g.ToString();
                             IMU2_MagY.Text = h.ToString();
                             IMU2_MagZ.Text = i.ToString();
                         }));
        }

        public void UpdateSHT1X1(float a, float b)
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(
                         () =>
                         {
                             SHT1X1_Temp.Text = a.ToString();
                             SHT1X1_Hum.Text = b.ToString();
                         }));
        }

        public void UpdateSHT1X2(float a, float b)
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(
                         () =>
                         {
                             SHT1X2_Temp.Text = a.ToString();
                             SHT1X2_Hum.Text = b.ToString();
                         }));
        }

        public void UpdatePulse1(float a)
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(
                         () =>
                         {
                             Pulse_Pulse.Text = a.ToString();
                         }));
        }

        public void UpdatePulse2(float a)
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(
                         () =>
                         {
                             Pulse_TempLobe.Text = a.ToString();
                         }));
        }

        public void UpdateGSR(float a)
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(
                         () =>
                         {
                             GSR.Text = a.ToString();
                         }));
        }





    }
}