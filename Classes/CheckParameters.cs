using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTTDataProvider.Classes
{
    class CheckParameters
    {
        #region Variables
        // default BrokerAddress
        public string BrokerAddress
        {
            get { return brokerAddress; }
            set { brokerAddress = value; }
        }
        private string brokerAddress;
        #endregion

        #region Method
        public void CheckStartupParameters()
        {
            // check the startup parameters
            string[] StartupPar = Environment.GetCommandLineArgs();
            if (StartupPar.Any(s => s.Contains("-ba")))
            {
                int ParIndex = Array.IndexOf(StartupPar, "-ba");
                brokerAddress = StartupPar[ParIndex + 1];
            }
            // if no parameters are provided, set the default values accordingly.
            else
            {
                brokerAddress = "localhost";
                Console.WriteLine("Starting with default broker address (localhost).");
            }
        }
        #endregion  
    }
}
