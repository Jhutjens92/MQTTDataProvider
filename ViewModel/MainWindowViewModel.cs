using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using MQTTDataProvider.Classes;
using MQTTDataProvider.Model;
using static MQTTDataProvider.Classes.MqttManager;

namespace MQTTDataProvider.ViewModel
{
    class MainWindowViewModel : BindableBase
    {
        #region Instance declaration
        MqttManager mdmanager = new MqttManager();
        #endregion

        #region Variables

        private string textReceived = "";
        public string TextReceived
        {
            get { return textReceived; }
            set
            {
                if (value == null)
                {
                    value = 0.ToString();
                }
                textReceived = value;
                OnPropertyChanged("TextReceived");
            }
        }

        private string buttonText = "Start Recording";
        public string ButtonText
        {
            get { return buttonText; }
            set
            {
                buttonText = value;
                OnPropertyChanged("ButtonText");
            }
        }

        private Brush buttonColor = new SolidColorBrush(Colors.White);
        public Brush ButtonColor
        {
            get { return buttonColor; }
            set
            {
                buttonColor = value;
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
            CloseApp();
            Environment.Exit(Environment.ExitCode);
        }
                
        private void IUpdateTextBox(object sender, TextReceivedEventArgs e)
        {
            TextReceived = e.TextReceived;
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
            mdmanager.NewMqttTextReceived += IUpdateTextBox;
            HubConnector.StartConnection();
            HubConnector.MyConnector.startRecordingEvent += MyConnector_startRecordingEvent;
            HubConnector.MyConnector.stopRecordingEvent += MyConnector_stopRecordingEvent;
            SetLHDescriptions.SetDescriptions();
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