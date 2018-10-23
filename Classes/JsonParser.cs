using System;
using MQTTDataProvider.ViewModel;
using Newtonsoft.Json.Linq;


namespace MQTTDataProvider.Classes
{
    class JsonParser
    {

        #region Variables
        // JSON Parser MQTT message
        public static dynamic ParsedMqttMsg
        {
            get { return parsedMqttMsg; }
            set { parsedMqttMsg = value; }
        }
        private static dynamic parsedMqttMsg;
        #endregion

        #region Method
        // Parse MQTT JSON string
        public static void JSONParseReceivedMessage(string receivedMessage)
        {
            Globals.JsonErrorMessage = false;
            try
            {
                parsedMqttMsg = JObject.Parse(receivedMessage);
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid JSON string");
            }
        }
        #endregion
    }
}