using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTTDataProvider.Classes
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   This class checks the startup parameters. </summary>
    ///
    /// <remarks>   Jordi Hutjens, 26-10-2018. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    class CheckParameters
    {
        #region Variables

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   method for getting the BrokerAddress variable. </summary>
        ///
        /// <value> The broker address. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public string BrokerAddress
        {
            get { return brokerAddress; }
        }
        private string brokerAddress;

        #endregion

        #region Method

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Checks the startup parameters given (if any). If no arguments given, it sets default values.
        /// </summary>
        ///
        /// <remarks>   Jordi Hutjens, 26-10-2018. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public void CheckStartupParameters()
        {
            try
            {
                string[] StartupPar = Environment.GetCommandLineArgs();
                if (StartupPar.Any(s => s.Contains("-ba")))
                {
                    int ParIndex = Array.IndexOf(StartupPar, "-ba");
                    brokerAddress = StartupPar[ParIndex + 1];
                    Console.WriteLine("Starting with broker address: {0}", brokerAddress);
                }
                else
                {
                    brokerAddress = "localhost";
                    Console.WriteLine("Starting with default broker address (localhost).");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        #endregion  
    }
}
