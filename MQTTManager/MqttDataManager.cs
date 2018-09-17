using System.Text;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using ESPDataProvider.UDPManager;
using System;

namespace ESPDataProvider.MQTTManager
{
    class MqttDataManager
    {

        MqttClient client;
        string clientId;

        //default MQTT server value for WEKIT
        string BrokerAddress;


        public MqttDataManager()
        {
            //INIT Var Values//
            BrokerAddress = "localhost";
            //MQTT Functions//
            client = new MqttClient(BrokerAddress);
            // use a unique id as client id, each time we start the application
            clientId = Guid.NewGuid().ToString();
            client.Connect(clientId);
        }

        // this code runs when data is published to the subscribed topic
        public void Publish_Data()
        {
            // whole topic
            string GSR_Value = UDPDataManager.Parsed_ReceivedMessage.gsr;
            client.Publish("wekit/vest/GSR_Raw", Encoding.UTF8.GetBytes(GSR_Value), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);

            string Pulse_Value = UDPDataManager.Parsed_ReceivedMessage.pulse;
            client.Publish("wekit/vest/Pulse_Raw", Encoding.UTF8.GetBytes(Pulse_Value), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);

            string SHT1X1_Temp_Value = UDPDataManager.Parsed_ReceivedMessage.shts[0].temp;
            client.Publish("wekit/vest/Sht0_Temp", Encoding.UTF8.GetBytes(SHT1X1_Temp_Value), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);

            string SHT1X1_Hum_Value = UDPDataManager.Parsed_ReceivedMessage.shts[0].hum;
            client.Publish("wekit/vest/Sht0_Hum", Encoding.UTF8.GetBytes(SHT1X1_Hum_Value), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);

            string SHT2X2_Temp_Value = UDPDataManager.Parsed_ReceivedMessage.shts[1].temp;
            client.Publish("wekit/vest/Sht2_Temp", Encoding.UTF8.GetBytes(SHT2X2_Temp_Value), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);

            string SHT2X2_Hum_Value = UDPDataManager.Parsed_ReceivedMessage.shts[1].hum;
            client.Publish("wekit/vest/Sht2_Hum", Encoding.UTF8.GetBytes(SHT2X2_Hum_Value), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
        }
    }
}
