using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using MQTTDataProvider.Model;
using MQTTDataProvider.Classes;
using uPLibrary.Networking.M2Mqtt.Messages;
using static MQTTDataProvider.Classes.MqttManager;


namespace MQTTDataProvider.ViewModel
{
    class MainWindowViewModel : BindableBase
    {
        #region Instance Declaration
        MqttManager mdmanager = new MqttManager();
        #endregion

        #region Vars

        private string _textReceived = "";
        public String textReceived
        {
            get { return _textReceived; }
            set
            {
                if (value == null)
                {
                    value = 0.ToString();
                }
                _textReceived = value;
                OnPropertyChanged("TextReceived");
            }
        }

        private string _buttonText = "Start Recording";
        public String buttonText
        {
            get { return _buttonText; }
            set
            {
                _buttonText = value;
                OnPropertyChanged("ButtonText");
            }
        }

        private Brush _buttonColor = new SolidColorBrush(Colors.White);
        public Brush buttonColor
        {
            get { return _buttonColor; }
            set
            {
                _buttonColor = value;
                OnPropertyChanged("ButtonColor");

            }
        }

        #endregion

        #region Events
        private void MyConnector_stopRecordingEvent(object sender)
        {
            Application.Current.Dispatcher.BeginInvoke(
                DispatcherPriority.Background,
                new Action(() => {
                    this.StartRecordingData();
                }));
        }

        private void MyConnector_startRecordingEvent(object sender)
        {
            Application.Current.Dispatcher.BeginInvoke(
                 DispatcherPriority.Background,
                 new Action(() => {
                     this.StartRecordingData();
                 }));
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            CloseConnection();
            CloseApp();
            Environment.Exit(Environment.ExitCode);
        }
                
        private void SendData(object sender, TextReceivedEventArgs e)
        {
            if (Globals.jsonErrorMessage == false)
            {
                try
                {
                    textReceived = e.textReceived;
                    var values = new List<string>
                {
                    e.espTimeStamp,
                    e.imu1_AccX,
                    e.imu1_AccY,
                    e.imu1_AccZ,
                    e.imu1_GyroX,
                    e.imu1_GyroY,
                    e.imu1_GyroZ,
                    e.imu1_MagX,
                    e.imu1_MagY,
                    e.imu1_MagZ,
                    e.imu1_Q0,
                    e.imu1_Q1,
                    e.imu1_Q2,
                    e.imu1_Q3,
                    e.imu2_AccX,
                    e.imu2_AccY,
                    e.imu2_AccZ,
                    e.imu2_GyroX,
                    e.imu2_GyroY,
                    e.imu2_GyroZ,
                    e.imu2_MagX,
                    e.imu2_MagY,
                    e.imu2_MagZ,
                    e.imu2_Q0,
                    e.imu2_Q1,
                    e.imu2_Q2,
                    e.imu2_Q3,
                    e.tempExternal,
                    e.humExternal,
                    e.tempInternal,
                    e.humInternal,
                    e.pulse,
                    e.gsr
                };
                    HubConnector.SendData(values);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.StackTrace);
                }
            }  
            else if (Globals.jsonErrorMessage == true)
            {
                textReceived = e.textReceived;
            }
        }

        private ICommand _buttonClicked;

        public ICommand OnButtonClicked
        {
            get
            {
                _buttonClicked = new RelayCommand(
                    param => this.StartRecordingData(), null
                    );
                return _buttonClicked;
            }
        }

        public void StartRecordingData()
        {
            if (Globals.isRecordingMqtt == false)
            {
                Globals.isRecordingMqtt = true;
                buttonText = "Stop Recording";
                buttonColor = new SolidColorBrush(Colors.Green);
            }
            else if (Globals.isRecordingMqtt == true)
            {
                Globals.isRecordingMqtt = false;
                buttonText = "Start Recording";
                buttonColor = new SolidColorBrush(Colors.White);

            }
        }
        #endregion

        #region Constructor
        public MainWindowViewModel()
        {
            mdmanager.NewMqttTextReceived += SendData;
            HubConnector.StartConnection();
            HubConnector.MyConnector.startRecordingEvent += MyConnector_startRecordingEvent;
            HubConnector.MyConnector.stopRecordingEvent += MyConnector_stopRecordingEvent;
            SetValueNames();
            Application.Current.MainWindow.Closing += MainWindow_Closing;
        }
        #endregion

        #region Methods
        public void CloseApp()
        {
            try
            {
                Process[] mqttDataProviderProcess = Process.GetProcessesByName("MQTTDataProvider");
                mqttDataProviderProcess[0].CloseMainWindow();
            }
            catch (Exception e)
            {
                Console.WriteLine("I got an exception after closing App" + e);
            }
        }
  
        public void SetValueNames()
        {
            var names = new List<string>
            {
                "ESP_TimeStap",
                "IMU1_AccX",
                "IMU1_AccY",
                "IMU1_AccZ",
                "IMU1_GyroX",
                "IMU1_GyroY",
                "IMU1_GyroZ",
                "IMU1_MagX",
                "IMU1_MagY",
                "IMU1_MagZ",
                "IMU1_Q0",
                "IMU1_Q1",
                "IMU1_Q2",
                "IMU1_Q3",
                "IMU2_AccX",
                "IMU2_AccY",
                "IMU2_AccZ",
                "IMU2_GyroX",
                "IMU2_GyroY",
                "IMU2_GyroZ",
                "IMU2_MagX",
                "IMU2_MagY",
                "IMU2_MagZ",
                "IMU2_Q0",
                "IMU2_Q1",
                "IMU2_Q2",
                "IMU2_Q3",
                "Temp_Ext",
                "Humidity_Ext",
                "Temp_Int",
                "Humidity_Int",
                "Pulse_TempLobe",
                "GSR"
            };
            HubConnector.SetValuesName(names);
        }
        #endregion
    }
}