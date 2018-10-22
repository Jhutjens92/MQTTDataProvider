using System;
using MQTTDataProvider.ViewModel;
using Newtonsoft.Json.Linq;


namespace MQTTDataProvider.Classes
{
    class JsonParser
    {

        #region Vars
        // JSON Parser MQTT message
        public static dynamic ParsedMqttMsg
        {
            get { return parsedMqttMsg; }
            set { parsedMqttMsg = value; }
        }
        private static dynamic parsedMqttMsg;
        #endregion

        #region Methods
        // Parse MQTT JSON String
        public static void JSONParseReceivedMessage(string receivedMessage)
        {
            Globals.jsonErrorMessage = false;
            try
            {
                parsedMqttMsg = JObject.Parse(receivedMessage);
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid JSON String");
            }
        }
        #endregion
    }
}