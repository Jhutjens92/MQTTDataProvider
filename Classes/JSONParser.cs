using MQTTDataProvider.Classes;
using MQTTDataProvider.ViewModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTTDataProvider.Classes
{
    class JsonParser
    {
        #region Vars
        // JSON Parser MQTT message
        public static dynamic ParsedReceivedMessage;
        #endregion

        #region Methods
        // Parse MQTT JSON String
        public static void JSONParseReceivedMessage()
        {
            Globals.JSONErrorMessage = false;
            try
            {
                ParsedReceivedMessage = JObject.Parse(MqttManager.ReceivedMessage);
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid JSON String");
            }
        }
        #endregion

    }
}
