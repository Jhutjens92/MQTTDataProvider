using System;
using MQTTDataProvider.ViewModel;
using Newtonsoft.Json.Linq;


namespace MQTTDataProvider.Classes
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   Class containing the Jsonparser. </summary>
    ///
    /// <remarks>   Jordi Hutjens, 26-10-2018. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    class JsonParser
    {
        #region Variables

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Method for getting the ParsedMqttMsg variable. </summary>
        ///
        /// <value> A message describing the parsed mqtt. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public dynamic ParsedMqttMsg
        {
            get { return parsedMqttMsg; }
        }
        private static dynamic parsedMqttMsg;

        #endregion

        #region Method

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Parse MQTT JSON string. </summary>
        ///
        /// <remarks>   Jordi Hutjens, 26-10-2018. </remarks>
        ///
        /// <param name="receivedMessage">  String containing the receivedMessage from the Mqtt callback
        ///                                 funtion. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public void JSONParseReceivedMessage(string receivedMessage)
        {
            try
            {
                parsedMqttMsg = JObject.Parse(receivedMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        #endregion
    }
}