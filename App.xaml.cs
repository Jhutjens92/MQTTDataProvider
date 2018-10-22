using MQTTDataProvider.MQTTManager;
using System;
using System.Linq;
using System.Windows;

namespace MQTTDataProvider
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //Checks the startup parameters
        void ParameterCheck(object sender, StartupEventArgs e)
        {
            var sba = new MqttManager.BrokerAddress();
            string[] parameters = Environment.GetCommandLineArgs();
            if (parameters.Any(s => s.Contains("-ba")))
            {
                int parameterIndex = Array.IndexOf(parameters, "-ba");
                sba._brokerAddress = parameters[parameterIndex + 1];
            }
            else
            {
                sba._brokerAddress = "localhost";
                Console.WriteLine("No valid paramater provided, starting with default values.");
            }

            // Create main application window, starting minimized if specified
            MainWindowView mainWindow = new MainWindowView();
            mainWindow.InitializeComponent();
            mainWindow.Show();
        }
    }
}
