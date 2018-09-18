using System;
﻿using MQTTDataProvider.Model;
using MQTTDataProvider.MQTTManager;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using static MQTTDataProvider.MQTTManager.MqttDataManager;

namespace MQTTDataProvider.ViewModel
{
    class MainWindowViewModel: BindableBase
    {
        MqttDataManager mdmanager = new MqttDataManager();

        #region Vars & Properties
        private string _IMU1_AccX = "";
        public String IMU1_AccX
        {
            get { return _IMU1_AccX; }
            set
            {
                _IMU1_AccX = value;
                OnPropertyChanged("IMU1_AccX");
            }
        }

        private string _IMU1_AccY= "";
        public String IMU1_AccY
        {
            get { return _IMU1_AccY; }
            set
            {
                _IMU1_AccY = value;
                OnPropertyChanged("IMU1_AccY");
            }
        }

        private string _IMU1_AccZ= "";
        public String IMU1_AccZ
        {
            get { return _IMU1_AccZ; }
            set
            {
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
                _IMU1_GyroX = value;
                OnPropertyChanged("IMU1_GyroX");
            }
        }

        private string _IMU1_GyroY= "";
        public String IMU1_GyroY
        {
            get { return _IMU1_GyroY; }
            set
            {
                _IMU1_GyroY = value;
                OnPropertyChanged("IMU1_GyroY");
            }
        }

        private string _IMU1_GyroZ= "";
        public String IMU1_GyroZ
        {
            get { return _IMU1_GyroZ; }
            set
            {
                _IMU1_GyroZ = value;
                OnPropertyChanged("IMU1_GyroZ");
            }
        }

        private string _IMU1_MagX= "";
        public String IMU1_MagX
        {
            get { return _IMU1_MagX; }
            set
            {
                _IMU1_MagX = value;
                OnPropertyChanged("IMU1_MagX");
            }
        }

        private string _IMU1_MagY= "";
        public String IMU1_MagY
        {
            get { return _IMU1_MagY; }
            set
            {
                _IMU1_MagY = value;
                OnPropertyChanged("IMU1_MagY");
            }
        }

        private string _IMU1_MagZ= "";
        public String IMU1_MagZ
        {
            get { return _IMU1_MagZ; }
            set
            {
                _IMU1_MagZ = value;
                OnPropertyChanged("IMU1_MagZ");
            }
        }

        private string _IMU1_Q0= "";
        public String IMU1_Q0
        {
            get { return _IMU1_Q0; }
            set
            {
                _IMU1_Q0 = value;
                OnPropertyChanged("IMU1_Q0");
            }
        }

        private string _IMU1_Q1= "";
        public String IMU1_Q1
        {
            get { return _IMU1_Q1; }
            set
            {
                _IMU1_Q1 = value;
                OnPropertyChanged("IMU1_Q1");
            }
        }

        private string _IMU1_Q2= "";
        public String IMU1_Q2
        {
            get { return _IMU1_Q2; }
            set
            {
                _IMU1_Q2 = value;
                OnPropertyChanged("IMU1_Q2");
            }
        }

        private string _IMU1_Q3= "";
        public String IMU1_Q3
        {
            get { return _IMU1_Q3; }
            set
            {
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
                _IMU2_Q3 = value;
                OnPropertyChanged("IMU2_Q3");
            }
        }

        private string _Temp_External= "";
        public String Temp_External
        {
            get { return _Temp_External; }
            set
            {
                _Temp_External = value;
                OnPropertyChanged("Temp_External");
            }
        }

        private string _Humidity_External= "";
        public String Humidity_External
        {
            get { return _Humidity_External; }
            set
            {
                _Humidity_External = value;
                OnPropertyChanged("Humidity_External");
            }
        }

        private string _Temp_Internal= "";
        public String Temp_Internal
        {
            get { return _Temp_Internal; }
            set
            {
                _Temp_Internal = value;
                OnPropertyChanged("Temp_Internal");
            }
        }

        private string _Humidity_Internal= "";
        public String Humidity_Internal
        {
            get { return _Humidity_Internal; }
            set
            {
                _Humidity_Internal = value;
                OnPropertyChanged("Humidity_Internal");
            }
        }

        private string _Pulse_TempLobe= "";
        public String Pulse_TempLobe
        {
            get { return _Pulse_TempLobe; }
            set
            {
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

        public MainWindowViewModel()
        {
            mdmanager.NewMqttTextReceived += OnNewMqttReceived;
            HubConnector.StartConnection();
            HubConnector.MyConnector.startRecordingEvent += MyConnector_startRecordingEvent;
            HubConnector.MyConnector.stopRecordingEvent += MyConnector_stopRecordingEvent;
            SetValueNames();
        }

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

        private void OnNewMqttReceived(object sender, TextReceivedEventArgs e)
        {
            TextReceived = e.TextReceived;
        }

        #region events
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

        #region LearningHubMethods
        public void SetValueNames()
        {
            var names = new List<string>();
            names.Add("IMU1_AccX");
            names.Add("IMU1_AccY");
            names.Add("IMU1_AccZ");
            names.Add("IMU1_GyroX");
            names.Add("IMU1_GyroY");
            names.Add("IMU1_GyroZ");
            names.Add("IMU1_MagX");
            names.Add("IMU1_MagY");
            names.Add("IMU1_MagZ");
            names.Add("IMU1_Q0");
            names.Add("IMU1_Q1");
            names.Add("IMU1_Q2");
            names.Add("IMU1_Q3");
            names.Add("IMU2_AccX");
            names.Add("IMU2_AccY");
            names.Add("IMU2_AccZ");
            names.Add("IMU2_GyroX");
            names.Add("IMU2_GyroY");
            names.Add("IMU2_GyroZ");
            names.Add("IMU2_MagX");
            names.Add("IMU2_MagY");
            names.Add("IMU2_MagZ");
            names.Add("IMU2_Q0");
            names.Add("IMU2_Q1");
            names.Add("IMU2_Q2");
            names.Add("IMU2_Q3");
            names.Add("Temp_Ext");
            names.Add("Humidity_Ext");
            names.Add("Temp_Int");
            names.Add("Humidity_Int");
            names.Add("Pulse_TempLobe");
            names.Add("GSR");
            HubConnector.SetValuesName(names);

        }

        public void SendData()
        {
            try
            {
                var values = new List<string>();
                values.Add(IMU1_AccX);
                values.Add(IMU1_AccY);
                values.Add(IMU1_AccZ);
                values.Add(IMU1_GyroX);
                values.Add(IMU1_GyroY);
                values.Add(IMU1_GyroZ);
                values.Add(IMU1_MagX);
                values.Add(IMU1_MagY);
                values.Add(IMU1_MagZ);
                values.Add(IMU1_Q0);
                values.Add(IMU1_Q1);
                values.Add(IMU1_Q2);
                values.Add(IMU1_Q3);
                values.Add(IMU2_AccX);
                values.Add(IMU2_AccY);
                values.Add(IMU2_AccZ);
                values.Add(IMU2_GyroX);
                values.Add(IMU2_GyroY);
                values.Add(IMU2_GyroZ);
                values.Add(IMU2_MagX);
                values.Add(IMU2_MagY);
                values.Add(IMU2_MagZ);
                values.Add(IMU2_Q0);
                values.Add(IMU2_Q1);
                values.Add(IMU2_Q2);
                values.Add(IMU2_Q3);
                values.Add(Temp_External);
                values.Add(Humidity_External);
                values.Add(Temp_Internal);
                values.Add(Humidity_Internal);
                values.Add(Pulse_TempLobe);
                values.Add(GSR);
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
