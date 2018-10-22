using MQTTDataProvider.MQTTManager;
using System.Windows;


namespace MQTTDataProvider
{
    public partial class MainWindowView : Window
    {
        MqttManager mdmanager = new MqttManager();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mdmanager.StartMqttClient();
        }
    }
}