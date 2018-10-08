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
using uPLibrary.Networking.M2Mqtt.Messages;
using static MQTTDataProvider.MQTTManager.MqttDataManager;


namespace MQTTDataProvider.ViewModel
{
    class MainWindowViewModel : BindableBase
    {
        MqttDataManager mdmanager = new MqttDataManager();

        #region Vars
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
                OnPropertyChanged("IMU1_AccX");
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
                OnPropertyChanged("IMU1_AccY");
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
                OnPropertyChanged("IMU1_AccZ");
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
                OnPropertyChanged("IMU1_GyroX");
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
                OnPropertyChanged("IMU1_GyroY");
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
                OnPropertyChanged("IMU1_GyroZ");
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
                OnPropertyChanged("IMU1_MagX");
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
                OnPropertyChanged("IMU1_MagY");
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
                OnPropertyChanged("IMU1_MagZ");
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
                OnPropertyChanged("IMU1_Q0");
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
                OnPropertyChanged("IMU1_Q1");
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
                OnPropertyChanged("IMU1_Q2");
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
                OnPropertyChanged("IMU1_Q3");
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
                OnPropertyChanged("IMU2_AccX");
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
                OnPropertyChanged("IMU2_AccY");
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
                OnPropertyChanged("IMU2_AccZ");
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
                OnPropertyChanged("IMU2_GyroX");
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
                OnPropertyChanged("IMU2_GyroY");
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
                OnPropertyChanged("IMU2_GyroZ");
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
                OnPropertyChanged("IMU2_MagX");
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
                OnPropertyChanged("IMU2_MagY");
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
                OnPropertyChanged("IMU2_MagZ");
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
                OnPropertyChanged("IMU2_Q0");
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
                OnPropertyChanged("IMU2_Q1");
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
                OnPropertyChanged("IMU2_Q2");
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
                OnPropertyChanged("IMU2_Q3");
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
                OnPropertyChanged("Temp_External");
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
                OnPropertyChanged("Humidity_External");
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
                OnPropertyChanged("Temp_Internal");
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
                OnPropertyChanged("Humidity_Internal");
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
                OnPropertyChanged("Pulse_TempLobe");
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
                OnPropertyChanged("GSR");
            }
        }

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
      
        #region events
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
            CloseApp();
            Environment.Exit(Environment.ExitCode);
        }
                
        private void OnNewMqttReceived(object sender, TextReceivedEventArgs e)
        {
            TextReceived = e.TextReceived;
            IMU1_AccX = e.IMU1_AccX;
            IMU1_AccY = e.IMU1_AccY;
            IMU1_AccZ = e.IMU1_AccZ;
            IMU1_GyroX = e.IMU1_GyroX;
            IMU1_GyroY = e.IMU1_GyroY;
            IMU1_GyroZ = e.IMU1_GyroZ;
            IMU1_MagX = e.IMU1_MagX;
            IMU1_MagY = e.IMU1_MagY;
            IMU1_MagZ = e.IMU1_MagZ;
            IMU1_Q0 = e.IMU1_Q0;
            IMU1_Q1 = e.IMU1_Q1;
            IMU1_Q2 = e.IMU1_Q2;
            IMU1_Q3 = e.IMU1_Q3;
            IMU2_AccX = e.IMU2_AccX;
            IMU2_AccY = e.IMU2_AccY;
            IMU2_AccZ = e.IMU2_AccZ;
            IMU2_GyroX = e.IMU2_GyroX;
            IMU2_GyroY = e.IMU2_GyroY;
            IMU2_GyroZ = e.IMU2_GyroZ;
            IMU2_MagX = e.IMU2_MagX;
            IMU2_MagY = e.IMU2_MagY;
            IMU2_MagZ = e.IMU2_MagZ;
            IMU2_Q0 = e.IMU2_Q0;
            IMU2_Q1 = e.IMU2_Q1;
            IMU2_Q2 = e.IMU2_Q2;
            IMU2_Q3 = e.IMU2_Q3;
            Temp_External = e.Temp_Ext;
            Humidity_External = e.Humidity_Ext;
            Temp_Internal = e.Temp_Int;
            Humidity_Internal = e.Humidity_Int;
            Pulse_TempLobe = e.Pulse_TempLobe;
            GSR = e.GSR;
            SendData();
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
            if (Globals.IsRecordingMqtt == false)
            {
                Globals.IsRecordingMqtt = true;
                ButtonText = "Stop Recording";
                ButtonColor = new SolidColorBrush(Colors.Green);
            }
            else if (Globals.IsRecordingMqtt == true)
            {
                Globals.IsRecordingMqtt = false;
                ButtonText = "Start Recording";
                ButtonColor = new SolidColorBrush(Colors.White);

            }
        }
        #endregion

        #region Constructor
        public MainWindowViewModel()
        {
            Globals.IsRecordingMqtt = false;
            mdmanager.NewMqttTextReceived += OnNewMqttReceived;
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
                Process[] pp1 = Process.GetProcessesByName("MQTTDataProvider");
                pp1[0].CloseMainWindow();

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

        public void SendData()
        {
            try
            {
                var values = new List<string>
                {
                    IMU1_AccX,
                    IMU1_AccY,
                    IMU1_AccZ,
                    IMU1_GyroX,
                    IMU1_GyroY,
                    IMU1_GyroZ,
                    IMU1_MagX,
                    IMU1_MagY,
                    IMU1_MagZ,
                    IMU1_Q0,
                    IMU1_Q1,
                    IMU1_Q2,
                    IMU1_Q3,
                    IMU2_AccX,
                    IMU2_AccY,
                    IMU2_AccZ,
                    IMU2_GyroX,
                    IMU2_GyroY,
                    IMU2_GyroZ,
                    IMU2_MagX,
                    IMU2_MagY,
                    IMU2_MagZ,
                    IMU2_Q0,
                    IMU2_Q1,
                    IMU2_Q2,
                    IMU2_Q3,
                    Temp_External,
                    Humidity_External,
                    Temp_Internal,
                    Humidity_Internal,
                    Pulse_TempLobe,
                    GSR
                };
                HubConnector.SendData(values);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
        }
        #endregion
    }
}