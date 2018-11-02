using System.Threading;
using System.IO;
using System.Diagnostics;
using System;

namespace MQTTDataProvider.Classes
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   Class to check and start the mosquitto broker if it isn't running. </summary>
    ///
    /// <remarks>   Jordi Hutjens, 2-11-2018. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    class MosquittoBroker
    {
        #region Variables

        /// <summary>   The exectuable. </summary>
        private static readonly string exectuable = "mosquitto.exe";

        /// <summary>   The first path. </summary>
        private static readonly string cPath1 = "C:/Program Files/mosquitto";

        /// <summary>   The second path. </summary>
        private static readonly string cPath2 = "C:/Program Files (x86)/mosquitto";

        /// <summary>   The first filename. </summary>
        private static readonly string filename1 = Path.Combine(cPath1, exectuable);

        /// <summary>   The second filename. </summary>
        private static readonly string filename2 = Path.Combine(cPath2, exectuable);

        #endregion

        #region Methods

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Starts mosquitto broker. </summary>
        ///
        /// <remarks>   Jordi Hutjens, 2-11-2018. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void StartMosquittoBroker()
        {
            try
            {
                if (File.Exists(filename1)) { Process.Start(filename1); }
                else if (File.Exists(filename2)) { Process.Start(filename2); }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine("Mosquitto Broker not started");
            }

        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Check mosquitto broker. </summary>
        ///
        /// <remarks>   Jordi Hutjens, 2-11-2018. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void CheckMosquittoBroker()
        {
            Process[] MosquittoBroker = Process.GetProcessesByName("mosquitto");
            if (MosquittoBroker.Length == 0)
            {
                StartMosquittoBroker();
                Thread.Sleep(500);
            }
        }
        #endregion
    }
}
