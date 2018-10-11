using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTTDataProvider.Classes
{
    class ParameterSet
    {
        // Checks the startup parameters
        public static void SetParameters()
        {
            string[] Parameters = Environment.GetCommandLineArgs();
            if (Parameters.Any(s => s.Contains("-ba")))
            {
                int parameterIndex = Array.IndexOf(Parameters, "-ba");
                MqttManager.BrokerAddress = Parameters[parameterIndex + 1];
            }
            else
            {
                Console.WriteLine("No valid paramater provided, starting with default values.");
            }
        }
    }
}
