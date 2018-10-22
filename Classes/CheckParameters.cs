using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTTDataProvider.Classes
{
    class CheckParameters
    {
        // Default brokeraddress
        public string BrokerAddress
        {
            get { return brokerAddress; }
            set { brokerAddress = value; }
        }
        private string brokerAddress;

        public void CheckStartupParameters()
        {
            string[] StartupPar = Environment.GetCommandLineArgs();
            if (StartupPar.Any(s => s.Contains("-ba")))
            {
                int ParIndex = Array.IndexOf(StartupPar, "-ba");
                brokerAddress = StartupPar[ParIndex + 1];
            }
            else
            {
                brokerAddress = "localhost";
                Console.WriteLine("No valid paramater provided, starting with default values.");
            }
        }
    }
}
