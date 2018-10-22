using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using MQTTDataProvider.Model;
using MQTTDataProvider.MQTTManager;
using static MQTTDataProvider.MQTTManager.MqttManager;


namespace MQTTDataProvider.ViewModel
{
    class MainWindowViewModel : BindableBase
    {
        #region Instance Declaration
        MqttManager mdmanager = new MqttManager();
        #endregion

        #region Vars

        private string _textReceived = "";
        public String TextReceived
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
        public String ButtonText
        {
            get { return _buttonText; }
            set
            {
                _buttonText = value;
                OnPropertyChanged("ButtonText");
            }
        }

        private Brush _buttonColor = new SolidColorBrush(Colors.White);
        public Brush ButtonColor
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
            CloseMqttConnection();
            CloseApp();
            Environment.Exit(Environment.ExitCode);
        }
                
        private void SendData(object sender, TextReceivedEventArgs e)
        {
            if (Globals.jsonErrorMessage == false)
            {
                try
                {
                    TextReceived = e.TextReceived;
                    var values = new List<string>
                {
                    e.ESPTimeStamp,
                    e.IMU1_AccX,
                    e.IMU1_AccY,
                    e.IMU1_AccZ,
                    e.IMU1_GyroX,
                    e.IMU1_GyroY,
                    e.IMU1_GyroZ,
                    e.IMU1_MagX,
                    e.IMU1_MagY,
                    e.IMU1_MagZ,
                    e.IMU1_Q0,
                    e.IMU1_Q1,
                    e.IMU1_Q2,
                    e.IMU1_Q3,
                    e.IMU2_AccX,
                    e.IMU2_AccY,
                    e.IMU2_AccZ,
                    e.IMU2_GyroX,
                    e.IMU2_GyroY,
                    e.IMU2_GyroZ,
                    e.IMU2_MagX,
                    e.IMU2_MagY,
                    e.IMU2_MagZ,
                    e.IMU2_Q0,
                    e.IMU2_Q1,
                    e.IMU2_Q2,
                    e.IMU2_Q3,
                    e.TempExternal,
                    e.HumExternal,
                    e.TempInternal,
                    e.HumInternal,
                    e.Pulse,
                    e.GSR
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
                TextReceived = e.TextReceived;
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
                ButtonText = "Stop Recording";
                ButtonColor = new SolidColorBrush(Colors.Green);
            }
            else if (Globals.isRecordingMqtt == true)
            {
                Globals.isRecordingMqtt = false;
                ButtonText = "Start Recording";
                ButtonColor = new SolidColorBrush(Colors.White);

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
        #endregion
    }
}