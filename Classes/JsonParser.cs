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
        /// <value> A message describing the parsed udp. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public dynamic ParsedMqttMsg
        {
            get { return parsedMqttMsg; }
        }
        private dynamic parsedMqttMsg;

        #endregion

        #region Method

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Parse Mqtt JSON string. </summary>
        ///
        /// <remarks>   Jordi Hutjens, 26-10-2018. </remarks>
        ///
        /// <param name="receivedMessage">  String containing the receivedMessage from the Mqtt Receive
        ///                                 funtion. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public void JSONParseReceivedMessage(string receivedMessage)
        {
            try
            {
                parsedMqttMsg = JObject.Parse(receivedMessage);
            }
            catch (Newtonsoft.Json.JsonReaderException ex)
            {
                string errorMessage = "Invalid JSON structure received. Exception: " + ex.Message;
                parsedMqttMsg = errorMessage;
                Globals.JsonErrorThrown = true;
            }
        }
        #endregion
    }
}